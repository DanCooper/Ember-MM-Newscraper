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

    Public Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Aired As String, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As MediaContainers.EpisodeDetails
        Return sObject.GetSingleEpisodeByAired(New Structures.ScrapeInfo With {.ShowID = ShowID, .TVDBID = TVDBID, .iEpisode = -999, .iSeason = -999, .Aired = Aired, .showLang = Lang, .Ordering = Ordering, .Options = Options})
    End Function

    Public Sub GetSingleImage(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As Enums.ImageType_TV, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal CurrentImage As Images, ByRef RetImage As Images)
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

    Public Sub ScrapeEpisode(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iEpisode As Integer, ByVal iSeason As Integer, ByVal Aired As String, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions)
        sObject.ScrapeEpisode(New Structures.ScrapeInfo With {.ShowID = ShowID, .ShowTitle = ShowTitle, .TVDBID = TVDBID, .iEpisode = iEpisode, .iSeason = iSeason, .Aired = Aired, .ShowLang = Lang, .Ordering = Ordering, .Options = Options})
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

        Dim AllSeasonsBanner As MediaContainers.Image
        Dim AllSeasonsFanart As MediaContainers.Image
        Dim AllSeasonsLandscape As MediaContainers.Image
        Dim AllSeasonsPoster As MediaContainers.Image
        Dim SeasonImageList As List(Of TVDBSeasonImage)
        Dim ShowBanner As MediaContainers.Image
        Dim ShowCharacterArt As MediaContainers.Image
        Dim ShowClearArt As MediaContainers.Image
        Dim ShowClearLogo As MediaContainers.Image
        Dim ShowFanart As MediaContainers.Image
        Dim ShowLandscape As MediaContainers.Image
        Dim ShowPoster As MediaContainers.Image

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
                tmpTVDBShow.AllSeason = Master.DB.LoadTVAllSeasonsFromDB(_ID)

                Using SQLCount As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    If OnlySeason = 999 Then
                        SQLCount.CommandText = String.Concat("SELECT COUNT(idEpisode) AS eCount FROM episode WHERE idShow = ", _ID, " AND Missing = 0;")
                    Else
                        SQLCount.CommandText = String.Concat("SELECT COUNT(idEpisode) AS eCount FROM episode WHERE idShow = ", _ID, " AND Season = ", OnlySeason, " AND Missing = 0;")
                    End If
                    Using SQLRCount As SQLite.SQLiteDataReader = SQLCount.ExecuteReader
                        If SQLRCount.HasRows Then
                            SQLRCount.Read()
                            If Convert.ToInt32(SQLRCount("eCount")) > 0 Then
                                Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                    If OnlySeason = 999 Then
                                        SQLCommand.CommandText = String.Concat("SELECT idEpisode, Lock FROM episode WHERE idShow = ", _ID, " AND Missing = 0;")
                                    Else
                                        SQLCommand.CommandText = String.Concat("SELECT idEpisode, Lock FROM episode WHERE idShow = ", _ID, " AND Season = ", OnlySeason, " AND Missing = 0;")
                                    End If
                                    Using SQLReader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                                        While SQLReader.Read
                                            tmpTVDBShow.Episodes.Add(Master.DB.LoadTVEpFromDB(Convert.ToInt64(SQLReader("idEpisode")), True))
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
            Dim tEpisodes As List(Of MediaContainers.EpisodeDetails) = Me.GetListOfKnownEpisodes(sInfo)
            If tEpisodes.Count > 0 Then
                Using dChangeEp As New dlgTVChangeEp
                    Return dChangeEp.ShowDialog(tEpisodes)
                End Using
            Else
                MessageBox.Show(Master.eLang.GetString(943, "There are no known episodes for this show. Scrape the show, season, or episode and try again."), Master.eLang.GetString(944, "No Known Episodes"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Return Nothing
        End Function

        Public Sub DownloadSeries(ByVal sInfo As Structures.ScrapeInfo, Optional ByVal ImagesOnly As Boolean = False)
            Dim fPath As String = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sInfo.TVDBID, Path.DirectorySeparatorChar, sInfo.ShowLang, ".zip"))
            Dim fExists As Boolean = File.Exists(fPath)
            Dim doDownload As Boolean = False

            Select Case Master.eSettings.TVScraperUpdateTime
                Case Enums.TVScraperUpdateTime.Always
                    'Check how old the ZIP file is. If it's older than 10min -> try to get a fresh/updated file
                    'This is usefull for non english users because TVDB can has much "not yet translated" information if the file is to old
                    If File.Exists(fPath) Then
                        Dim fileInfo As New FileInfo(fPath)
                        If fileInfo.LastWriteTime < DateTime.Now.AddMinutes(-10) Then
                            doDownload = True
                        End If
                    Else
                        doDownload = True
                    End If
                Case Enums.TVScraperUpdateTime.Never
                    doDownload = False
                Case Enums.TVScraperUpdateTime.Week
                    If fExists AndAlso File.GetCreationTime(fPath).AddDays(7) < Date.Now Then doDownload = True
                Case Enums.TVScraperUpdateTime.BiWeekly
                    If fExists AndAlso File.GetCreationTime(fPath).AddDays(14) < Date.Now Then doDownload = True
                Case Enums.TVScraperUpdateTime.Month
                    If fExists AndAlso File.GetCreationTime(fPath).AddMonths(1) < Date.Now Then doDownload = True
            End Select

            If doDownload OrElse Not fExists Then
                Using sHTTP As New HTTP
                    Dim xZip As Byte() = sHTTP.DownloadZip(String.Format("http://{0}/api/{1}/series/{2}/all/{3}.zip", _TVDBMirror, APIKey, sInfo.TVDBID, sInfo.ShowLang))

                    If xZip IsNot Nothing AndAlso xZip.Length > 0 Then
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

                    Using fStream As FileStream = New FileStream(fPath, FileMode.Open, FileAccess.Read)
                        Dim fZip As Byte() = Functions.ReadStreamToEnd(fStream)
                        Me.ProcessTVDBZip(fZip, sInfo)

                        'get the actors first
                        Try
                            If Not String.IsNullOrEmpty(aXML) Then
                                Dim xdActors As XDocument = XDocument.Parse(aXML)
                                For Each Actor As XElement In xdActors.Descendants("Actor")
                                    If Actor.Element("Name") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Actor.Element("Name").Value) Then
                                        Actors.Add(New MediaContainers.Person With {.Name = Actor.Element("Name").Value, .Role = If(Actor.Element("Role") Is Nothing, String.Empty, Actor.Element("Role").Value), .ThumbURL = If(Actor.Element("Image") Is Nothing OrElse String.IsNullOrEmpty(Actor.Element("Image").Value), String.Empty, String.Format("http://{0}/banners/{1}", _TVDBMirror, Actor.Element("Image").Value))})
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name, ex)
                        End Try

                        If Not String.IsNullOrEmpty(sXML) Then
                            Dim xdEps As XDocument = XDocument.Parse(sXML)

                            For Each Episode As XElement In xdEps.Descendants("Episode")
                                If Episode.Element("EpisodeName").Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(Episode.Element("EpisodeName").Value) Then
                                    tEpisode = New MediaContainers.EpisodeDetails

                                    tOrdering = Enums.Ordering.Standard

                                    If sInfo.Ordering = Enums.Ordering.DVD Then
                                        If Episode.Element("SeasonNumber") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Episode.Element("SeasonNumber").Value.ToString) AndAlso _
                                        Episode.Element("DVD_season") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Episode.Element("DVD_season").Value.ToString) AndAlso _
                                        Episode.Element("DVD_episodenumber") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Episode.Element("DVD_episodenumber").Value.ToString) Then
                                            tSeas = Convert.ToInt32(Episode.Element("SeasonNumber").Value)
                                            If sInfo.iSeason >= 0 AndAlso Not tSeas = sInfo.iSeason Then Continue For
                                            If xdEps.Descendants("Episode").Where(Function(e) Convert.ToInt32(e.Element("SeasonNumber").Value) = tSeas AndAlso (e.Element("DVD_season") Is Nothing OrElse String.IsNullOrEmpty(e.Element("DVD_season").Value.ToString) OrElse e.Element("DVD_episodenumber") Is Nothing OrElse String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString))).Count = 0 Then
                                                tOrdering = Enums.Ordering.DVD
                                            End If
                                        ElseIf Episode.Element("DVD_season") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Episode.Element("DVD_season").Value.ToString) AndAlso _
                                        Episode.Element("DVD_episodenumber") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Episode.Element("DVD_episodenumber").Value.ToString) Then
                                            tSeas = Convert.ToInt32(Episode.Element("DVD_season").Value)
                                            If xdEps.Descendants("Episode").Where(Function(e) Convert.ToInt32(e.Element("DVD_season").Value) = tSeas AndAlso (e.Element("DVD_episodenumber") Is Nothing OrElse String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString))).Count = 0 Then
                                                tOrdering = Enums.Ordering.DVD
                                            End If
                                        End If
                                    ElseIf sInfo.Ordering = Enums.Ordering.Absolute Then
                                        If Episode.Element("absolute_number") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Episode.Element("absolute_number").Value.ToString) Then
                                            If xdEps.Descendants("Episode").Where(Function(e) Convert.ToInt32(e.Element("SeasonNumber").Value) > 0 AndAlso (e.Element("absolute_number") Is Nothing OrElse String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString))).Count = 0 Then
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
                                            .Season = If(Episode.Element("SeasonNumber") Is Nothing OrElse String.IsNullOrEmpty(Episode.Element("SeasonNumber").Value), 0, Convert.ToInt32(Episode.Element("SeasonNumber").Value))
                                            .Episode = If(Episode.Element("EpisodeNumber") Is Nothing OrElse String.IsNullOrEmpty(Episode.Element("EpisodeNumber").Value), 0, Convert.ToInt32(Episode.Element("EpisodeNumber").Value))
                                        End If
                                        If Episode.Element("airsafter_season") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Episode.Element("airsafter_season").Value) Then
                                            .DisplaySeason = Convert.ToInt32(Episode.Element("airsafter_season").Value)
                                            .DisplayEpisode = 4096
                                        End If
                                        If Episode.Element("airsbefore_season") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Episode.Element("airsbefore_season").Value) Then
                                            .DisplaySeason = Convert.ToInt32(Episode.Element("airsbefore_season").Value)
                                        End If
                                        If Episode.Element("airsbefore_episode") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Episode.Element("airsbefore_episode").Value) Then
                                            .DisplayEpisode = Convert.ToInt32(CLng(Episode.Element("airsbefore_episode").Value))
                                        End If

                                        .Aired = If(Episode.Element("FirstAired") Is Nothing, String.Empty, Episode.Element("FirstAired").Value)
                                        .Rating = If(Episode.Element("Rating") Is Nothing, String.Empty, Episode.Element("Rating").Value)
                                        .Votes = If(Episode.Element("RatingCount") Is Nothing, String.Empty, Episode.Element("RatingCount").Value)
                                        .Plot = If(Episode.Element("Overview") Is Nothing, String.Empty, Episode.Element("Overview").Value.ToString.Trim)
                                        .Director = If(Episode.Element("Director") Is Nothing, String.Empty, String.Join(" / ", Episode.Element("Director").Value.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|"))))
                                        .OldCredits = If(Episode.Element("Writer") Is Nothing, String.Empty, String.Join(" / ", Episode.Element("Writer").Value.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|"))))
                                        .Actors.Clear()
                                        .Actors.AddRange(Actors)
                                        If sInfo.Options.bEpGuestStars Then .Actors.AddRange(GuestStarsToActors(Episode.Element("GuestStars").Value))
                                        .PosterURL = If(Episode.Element("filename") Is Nothing, String.Empty, String.Format("http://{0}/banners/{1}", _TVDBMirror, Episode.Element("filename").Value))
                                        .LocalFile = If(Episode.Element("filename") Is Nothing, String.Empty, Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sInfo.TVDBID, Path.DirectorySeparatorChar, "episodeposters", Path.DirectorySeparatorChar, Episode.Element("filename").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))))
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

            DownloadSeries(sInfo)
            tEp = Me.GetListOfKnownEpisodes(sInfo).FirstOrDefault(Function(e) e.Season = sInfo.iSeason AndAlso e.Episode = sInfo.iEpisode)
            If tEp IsNot Nothing Then
                Return tEp
            End If

            Return New MediaContainers.EpisodeDetails
        End Function

        Public Function GetSingleEpisodeByAired(ByVal sInfo As Structures.ScrapeInfo) As MediaContainers.EpisodeDetails
            Dim tEp As New MediaContainers.EpisodeDetails

            DownloadSeries(sInfo)
            tEp = Me.GetListOfKnownEpisodes(sInfo).FirstOrDefault(Function(e) e.Aired = sInfo.Aired)
            If tEp IsNot Nothing Then
                Return tEp
            End If

            Return New MediaContainers.EpisodeDetails
        End Function

        Public Sub GetSingleImage(ByVal sInfo As Structures.ScrapeInfo, ByRef RetImage As Images)
            tmpTVDBShow = New TVDBShow

            If sInfo.ImageType = Enums.ImageType_TV.EpisodePoster Then

                If String.IsNullOrEmpty(sInfo.TVDBID) Then
                    Using dTVDBSearch As New dlgTVDBSearchResults
                        sInfo = dTVDBSearch.ShowDialog(sInfo, True)
                        If Not String.IsNullOrEmpty(sInfo.TVDBID) Then
                            Master.currShow.TVShow.ID = sInfo.TVDBID

                            Dim tmpEp As MediaContainers.EpisodeDetails = Me.GetListOfKnownEpisodes(sInfo).FirstOrDefault(Function(e) e.Episode = sInfo.iEpisode AndAlso e.Season = sInfo.iSeason)
                            If tmpEp IsNot Nothing Then

                                If File.Exists(tmpEp.LocalFile) Then
                                    RetImage.FromFile(tmpEp.LocalFile)
                                Else
                                    RetImage.FromWeb(tmpEp.PosterURL)
                                    If RetImage.Image IsNot Nothing Then
                                        Directory.CreateDirectory(Directory.GetParent(tmpEp.LocalFile).FullName)
                                        RetImage.Save(tmpEp.LocalFile)
                                    End If
                                End If

                                If RetImage.Image IsNot Nothing Then
                                    Using dPosterConfirm As New dlgTVEpisodePoster
                                        If Not (dPosterConfirm.ShowDialog(RetImage.Image) = DialogResult.OK) Then
                                            RetImage.Dispose()
                                        End If
                                    End Using
                                Else
                                    MessageBox.Show(Master.eLang.GetString(945, "There is no poster available for this episode."), Master.eLang.GetString(946, "No Posters Found"), MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                    If tmpEp IsNot Nothing Then
                        If File.Exists(tmpEp.LocalFile) Then
                            RetImage.FromFile(tmpEp.LocalFile)
                        Else
                            RetImage.FromWeb(tmpEp.PosterURL)
                            If RetImage.Image IsNot Nothing Then
                                Directory.CreateDirectory(Directory.GetParent(tmpEp.LocalFile).FullName)
                                RetImage.Save(tmpEp.LocalFile)
                            End If
                        End If

                        If RetImage.Image IsNot Nothing Then
                            Using dPosterConfirm As New dlgTVEpisodePoster
                                If Not (dPosterConfirm.ShowDialog(RetImage.Image) = DialogResult.OK) Then
                                    RetImage.Dispose()
                                End If
                            End Using
                        Else
                            MessageBox.Show(Master.eLang.GetString(945, "There is no poster available for this episode."), Master.eLang.GetString(946, "No Posters Found"), MessageBoxButtons.OK, MessageBoxIcon.Information)
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

                    While zEntry IsNot Nothing
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
                                If Master.currShow.TVEp.Poster.Image IsNot Nothing Then
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
                            If Master.currShow.TVEp.Poster.Image IsNot Nothing Then
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
                                    If Master.currShow.TVEp.Poster IsNot Nothing Then
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
            If String.IsNullOrEmpty(sInfo.TVDBID) AndAlso sInfo.ScrapeType = Enums.ScrapeType.FullAsk Then
                RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Searching, 0, Nothing)
                Using dTVDBSearch As New dlgTVDBSearchResults
                    If dTVDBSearch.ShowDialog(sInfo) = Windows.Forms.DialogResult.OK Then
                        Master.currShow = tmpTVDBShow.Show
                        RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.SelectImages, 0, Nothing)
                        Using dTVImageSel As New dlgTVImageSelect
                            If dTVImageSel.ShowDialog(sInfo.ShowID, Enums.ImageType_TV.All, sInfo.ScrapeType, sInfo.WithCurrent) = Windows.Forms.DialogResult.OK Then
                                If sInfo.iSeason >= 0 Then
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
                        If dTVImageSel.ShowDialog(sInfo.ShowID, Enums.ImageType_TV.All, sInfo.ScrapeType, sInfo.WithCurrent) = Windows.Forms.DialogResult.OK Then
                            If sInfo.iSeason >= 0 Then
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
                                If dTVImageSel.ShowDialog(sInfo.ShowID, Enums.ImageType_TV.All, sInfo.ScrapeType, sInfo.WithCurrent) = Windows.Forms.DialogResult.OK Then
                                    If sInfo.iSeason >= 0 Then
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
        End Sub

        Private Sub bwtvDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVDB.DoWork
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)

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
        End Sub

        Private Sub bwTVDB_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTVDB.ProgressChanged
            RaiseEvent ScraperEvent(Enums.ScraperEventType_TV.Progress, e.ProgressPercentage, e.UserState.ToString)
        End Sub

        Private Sub bwTVDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTVDB.RunWorkerCompleted
            Dim Res As Results = DirectCast(e.Result, Results)

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
        End Sub

        Private Sub SaveAllTVInfo()
            Dim iEp As Integer = -1
            Dim iSea As Integer = -1
            Dim iProgress As Integer = 1

            Dim tShow As New Structures.DBTV
            Dim tEpisode As New MediaContainers.EpisodeDetails

            Me.bwTVDB.ReportProgress(tmpTVDBShow.Episodes.Count, "max")

            Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                'clear old missing episode from db
                Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    If onlyScrapeSeason <> 999 Then
                        SQLCommand.CommandText = String.Concat("DELETE FROM episode WHERE Missing = 1 AND Season = ", onlyScrapeSeason, " AND idShow = ", Master.currShow.ShowID, ";")
                    Else
                        SQLCommand.CommandText = String.Concat("DELETE FROM episode WHERE Missing = 1 AND idShow = ", Master.currShow.ShowID, ";")
                    End If
                    SQLCommand.ExecuteNonQuery()
                End Using

                For i As Integer = 0 To tmpTVDBShow.Episodes.Count - 1
                    If Me.bwTVDB.CancellationPending Then Return

                    Dim Episode As Structures.DBTV = tmpTVDBShow.Episodes.Item(i)

                    Episode.ShowID = Master.currShow.ShowID

                    iEp = Episode.TVEp.Episode
                    iSea = Episode.TVEp.Season

                    'remove it from tepisodes since it's a real episode
                    tEpisode = tEpisodes.FirstOrDefault(Function(e) e.Episode = iEp AndAlso e.Season = iSea)
                    If tEpisode IsNot Nothing Then tEpisodes.Remove(tEpisode)
                    tShow = Episode

                    If Me.bwTVDB.CancellationPending Then Return

                    If Episode.TVEp.Season > -1 AndAlso Episode.TVEp.Episode > -1 AndAlso Not Episode.IsLockEp Then
                        If Episode.TVEp.Poster.Image IsNot Nothing Then Episode.EpPosterPath = Episode.TVEp.Poster.SaveAsTVEpisodePoster(Episode, Episode.TVEp.PosterURL)

                        If Me.bwTVDB.CancellationPending Then Return

                        If Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso Episode.TVEp.Fanart.Image IsNot Nothing Then Episode.EpFanartPath = Episode.TVEp.Fanart.SaveAsTVEpisodeFanart(Episode, )

                        If Me.bwTVDB.CancellationPending Then Return

                        Dim cSea = From cSeason As TVDBSeasonImage In TVDBImages.SeasonImageList Where cSeason.Season = iSea Take 1
                        If cSea.Count > 0 Then
                            If Not cSea(0).AlreadySaved Then
                                If Master.eSettings.TVSeasonBannerAnyEnabled Then
                                    If Not String.IsNullOrEmpty(cSea(0).Banner.LocalFile) AndAlso File.Exists(cSea(0).Banner.LocalFile) Then
                                        cSea(0).Banner.WebImage.FromFile(cSea(0).Banner.LocalFile)
                                        Episode.SeasonBannerPath = cSea(0).Banner.WebImage.SaveAsTVSeasonBanner(Episode)
                                    ElseIf Not String.IsNullOrEmpty(cSea(0).Banner.URL) AndAlso Not String.IsNullOrEmpty(cSea(0).Banner.LocalFile) Then
                                        cSea(0).Banner.WebImage.Clear()
                                        cSea(0).Banner.WebImage.FromWeb(cSea(0).Banner.URL)
                                        If cSea(0).Banner.WebImage.Image IsNot Nothing Then
                                            Directory.CreateDirectory(Directory.GetParent(cSea(0).Banner.LocalFile).FullName)
                                            cSea(0).Banner.WebImage.Save(cSea(0).Banner.LocalFile)
                                            Episode.SeasonBannerPath = cSea(0).Banner.WebImage.SaveAsTVSeasonBanner(Episode)
                                        End If
                                    End If
                                End If
                                If Master.eSettings.TVSeasonFanartAnyEnabled Then
                                    If Not String.IsNullOrEmpty(cSea(0).Fanart.LocalFile) AndAlso File.Exists(cSea(0).Fanart.LocalFile) Then
                                        cSea(0).Fanart.WebImage.FromFile(cSea(0).Fanart.LocalFile)
                                        Episode.SeasonFanartPath = cSea(0).Fanart.WebImage.SaveAsTVSeasonFanart(Episode)
                                    ElseIf Not String.IsNullOrEmpty(cSea(0).Fanart.URL) AndAlso Not String.IsNullOrEmpty(cSea(0).Fanart.LocalFile) Then
                                        cSea(0).Fanart.WebImage.Clear()
                                        cSea(0).Fanart.WebImage.FromWeb(cSea(0).Fanart.URL)
                                        If cSea(0).Fanart.WebImage.Image IsNot Nothing Then
                                            Directory.CreateDirectory(Directory.GetParent(cSea(0).Fanart.LocalFile).FullName)
                                            cSea(0).Fanart.WebImage.Save(cSea(0).Fanart.LocalFile)
                                            Episode.SeasonFanartPath = cSea(0).Fanart.WebImage.SaveAsTVSeasonFanart(Episode)
                                        End If
                                    End If
                                End If
                                If Master.eSettings.TVSeasonLandscapeAnyEnabled Then
                                    If Not String.IsNullOrEmpty(cSea(0).Landscape.LocalFile) AndAlso File.Exists(cSea(0).Landscape.LocalFile) Then
                                        cSea(0).Landscape.WebImage.FromFile(cSea(0).Landscape.LocalFile)
                                        Episode.SeasonLandscapePath = cSea(0).Landscape.WebImage.SaveAsTVSeasonLandscape(Episode)
                                    ElseIf Not String.IsNullOrEmpty(cSea(0).Landscape.URL) AndAlso Not String.IsNullOrEmpty(cSea(0).Landscape.LocalFile) Then
                                        cSea(0).Landscape.WebImage.Clear()
                                        cSea(0).Landscape.WebImage.FromWeb(cSea(0).Landscape.URL)
                                        If cSea(0).Landscape.WebImage.Image IsNot Nothing Then
                                            Directory.CreateDirectory(Directory.GetParent(cSea(0).Landscape.LocalFile).FullName)
                                            cSea(0).Landscape.WebImage.Save(cSea(0).Landscape.LocalFile)
                                            Episode.SeasonLandscapePath = cSea(0).Landscape.WebImage.SaveAsTVSeasonLandscape(Episode)
                                        End If
                                    End If
                                End If
                                If Master.eSettings.TVSeasonPosterAnyEnabled Then
                                    If Not String.IsNullOrEmpty(cSea(0).Poster.LocalFile) AndAlso File.Exists(cSea(0).Poster.LocalFile) Then
                                        cSea(0).Poster.WebImage.FromFile(cSea(0).Poster.LocalFile)
                                        Episode.SeasonPosterPath = cSea(0).Poster.WebImage.SaveAsTVSeasonPoster(Episode)
                                    ElseIf Not String.IsNullOrEmpty(cSea(0).Poster.URL) AndAlso Not String.IsNullOrEmpty(cSea(0).Poster.LocalFile) Then
                                        cSea(0).Poster.WebImage.Clear()
                                        cSea(0).Poster.WebImage.FromWeb(cSea(0).Poster.URL)
                                        If cSea(0).Poster.WebImage.Image IsNot Nothing Then
                                            Directory.CreateDirectory(Directory.GetParent(cSea(0).Poster.LocalFile).FullName)
                                            cSea(0).Poster.WebImage.Save(cSea(0).Poster.LocalFile)
                                            Episode.SeasonPosterPath = cSea(0).Poster.WebImage.SaveAsTVSeasonPoster(Episode)
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        If Me.bwTVDB.CancellationPending Then Return

                        If Master.eSettings.TVScraperMetaDataScan Then MediaInfo.UpdateTVMediaInfo(Episode)

                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.ScraperMulti_TVEpisode, Nothing, Nothing, False, , Episode)

                        Master.DB.SaveTVEpToDB(Episode, False, If(cSea(0).AlreadySaved = False, True, False), True, True)
                        cSea(0).AlreadySaved = True

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

                    If Master.eSettings.TVEpisodeActorThumbsAnyEnabled Then
                        For Each act As MediaContainers.Person In Episode.TVEp.Actors
                            Dim img As New Images
                            img.FromWeb(act.ThumbURL)
                            If img.Image IsNot Nothing Then
                                act.ThumbPath = img.SaveAsTVEpisodeActorThumb(act, Episode)
                            End If
                        Next
                    End If

                    iProgress += 1
                Next

                'now save all missing episodes
                tShow.Filename = String.Empty
                tShow.EpFanartPath = String.Empty
                tShow.EpNfoPath = String.Empty
                tShow.EpPosterPath = String.Empty
                tShow.SeasonBannerPath = String.Empty
                tShow.SeasonFanartPath = String.Empty
                tShow.SeasonLandscapePath = String.Empty
                tShow.SeasonPosterPath = String.Empty
                tShow.IsLockEp = False
                tShow.IsMarkEp = False
                tShow.EpID = -1
                If tEpisodes.Count > 0 Then
                    For Each Episode As MediaContainers.EpisodeDetails In tEpisodes
                        tShow.TVEp = Episode
                        Master.DB.SaveTVEpToDB(tShow, True, True, True)
                    Next
                End If

                If Me.bwTVDB.CancellationPending Then Return

                SQLTrans.Commit()
            End Using
        End Sub

        Private Sub SaveToCache(ByVal sID As String, ByVal sURL As String, ByVal sPath As String)
            Dim sImage As New Images

            sImage.FromWeb(sURL)

            If sImage.Image IsNot Nothing Then
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
                                    cResult.Name = If(tSer.Element("SeriesName") IsNot Nothing, tSer.Element("SeriesName").Value, String.Empty)
                                    If tSer.Element("Language") IsNot Nothing AndAlso Master.eSettings.TVGeneralLanguages.Language.Count > 0 Then
                                        sLang = tSer.Element("Language").Value
                                        cResult.Language = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(s) s.abbreviation = sLang)
                                    ElseIf tSer.Element("Language") IsNot Nothing Then
                                        sLang = tSer.Element("Language").Value
                                        cResult.Language = New TVDBLanguagesLanguage With {.name = String.Format("Unknown ({0})", sLang), .abbreviation = sLang, .id = 0}
                                    Else
                                        'no language info available... don't bother adding it
                                        Continue For
                                    End If
                                    cResult.Aired = If(tSer.Element("FirstAired") IsNot Nothing, tSer.Element("FirstAired").Value, String.Empty)
                                    cResult.Overview = If(tSer.Element("Overview") IsNot Nothing, tSer.Element("Overview").Value.ToString.Trim, String.Empty)
                                    cResult.Banner = If(tSer.Element("banner") IsNot Nothing, tSer.Element("banner").Value, String.Empty)
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
                        cResult.Name = If(xS.Element("SeriesName") IsNot Nothing, xS.Element("SeriesName").Value, String.Empty)
                        If xS.Element("language") IsNot Nothing AndAlso Master.eSettings.TVGeneralLanguages.Language.Count > 0 Then
                            sLang = xS.Element("language").Value
                            cResult.Language = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(s) s.abbreviation = sLang)
                        ElseIf xS.Element("language") IsNot Nothing Then
                            sLang = xS.Element("language").Value
                            cResult.Language = New TVDBLanguagesLanguage With {.name = String.Format("Unknown ({0})", sLang), .abbreviation = sLang, .id = 0}
                        Else
                            'no language info available... don't bother adding it
                            Continue For
                        End If
                        cResult.Aired = If(xS.Element("FirstAired") IsNot Nothing, xS.Element("FirstAired").Value, String.Empty)
                        cResult.Overview = If(xS.Element("Overview") IsNot Nothing, xS.Element("Overview").Value.ToString.Trim, String.Empty)
                        cResult.Banner = If(xS.Element("banner") IsNot Nothing, xS.Element("banner").Value, String.Empty)
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
            Dim eAired As String = String.Empty
            Dim sID As String = String.Empty
            Dim iEp As Integer = -1
            Dim iSeas As Integer = -1
            Dim sTitle As String = String.Empty
            Dim byTitle As Boolean = False
            Dim xE As XElement = Nothing
            Dim tShow As Structures.DBTV = tmpTVDBShow.Show
            Dim tOrdering As Enums.Ordering = Enums.Ordering.Standard

            If Not ImagesOnly Then
                tEpisodes = Me.GetListOfKnownEpisodes(sInfo)

                'get the actors first
                Try
                    If sInfo.Options.bShowActors OrElse sInfo.Options.bEpActors Then
                        If Not String.IsNullOrEmpty(aXML) Then
                            Dim xdActors As XDocument = XDocument.Parse(aXML)
                            For Each Actor As XElement In xdActors.Descendants("Actor")
                                If Actor.Element("Name") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Actor.Element("Name").Value) Then
                                    Actors.Add(New MediaContainers.Person With {.Name = Actor.Element("Name").Value, .Role = Actor.Element("Role").Value, .ThumbURL = If(Actor.Element("Image") Is Nothing OrElse String.IsNullOrEmpty(Actor.Element("Image").Value), String.Empty, String.Format("http://{0}/banners/{1}", _TVDBMirror, Actor.Element("Image").Value))})
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
                            If tShow.TVShow IsNot Nothing Then
                                With tShow.TVShow
                                    sID = xS(0).Element("id").Value
                                    .ID = sID
                                    If sInfo.Options.bShowTitle AndAlso (String.IsNullOrEmpty(.Title) OrElse Not Master.eSettings.TVLockShowTitle) Then .Title = If(xS(0).Element("SeriesName") Is Nothing, .Title, xS(0).Element("SeriesName").Value)
                                    If sInfo.Options.bShowEpisodeGuide Then .EpisodeGuide.URL = If(Not String.IsNullOrEmpty(clsAdvancedSettings.GetSetting("TVDBAPIKey", "")), String.Format("http://{0}/api/{1}/series/{2}/all/{3}.zip", _TVDBMirror, clsAdvancedSettings.GetSetting("TVDBAPIKey", ""), sID, clsAdvancedSettings.GetSetting("TVDBLanguage", "en")), String.Empty)
                                    If sInfo.Options.bShowGenre AndAlso (String.IsNullOrEmpty(.Genre) OrElse Not Master.eSettings.TVLockShowGenre) Then .Genre = If(xS(0).Element("Genre") Is Nothing, .Genre, String.Join(" / ", xS(0).Element("Genre").Value.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|"))))
                                    If sInfo.Options.bShowMPAA Then .MPAA = If(xS(0).Element("ContentRating") Is Nothing, .MPAA, xS(0).Element("ContentRating").Value)
                                    If sInfo.Options.bShowPlot AndAlso (String.IsNullOrEmpty(.Plot) OrElse Not Master.eSettings.TVLockShowPlot) Then .Plot = If(xS(0).Element("Overview") Is Nothing, .Plot, xS(0).Element("Overview").Value.ToString.Trim)
                                    If sInfo.Options.bShowPremiered Then .Premiered = If(xS(0).Element("FirstAired") Is Nothing, .Premiered, xS(0).Element("FirstAired").Value)
                                    If sInfo.Options.bShowRating AndAlso (String.IsNullOrEmpty(.Rating) OrElse Not Master.eSettings.TVLockShowRating) Then .Rating = If(xS(0).Element("Rating") Is Nothing, .Rating, xS(0).Element("Rating").Value)
                                    If sInfo.Options.bShowRuntime AndAlso (String.IsNullOrEmpty(.Runtime) OrElse Not Master.eSettings.TVLockShowRuntime) Then .Runtime = If(xS(0).Element("Runtime") Is Nothing, .Runtime, xS(0).Element("Runtime").Value)
                                    If sInfo.Options.bShowStatus AndAlso (String.IsNullOrEmpty(.Status) OrElse Not Master.eSettings.TVLockShowStatus) Then .Status = If(xS(0).Element("Status") Is Nothing, .Status, xS(0).Element("Status").Value)
                                    If sInfo.Options.bShowStudio AndAlso (String.IsNullOrEmpty(.Studio) OrElse Not Master.eSettings.TVLockShowStudio) Then .Studio = If(xS(0).Element("Network") Is Nothing, .Studio, xS(0).Element("Network").Value)
                                    If sInfo.Options.bShowVotes AndAlso (String.IsNullOrEmpty(.Votes) OrElse Not Master.eSettings.TVLockShowVotes) Then .Votes = If(xS(0).Element("RatingCount") Is Nothing, .Votes, xS(0).Element("RatingCount").Value)
                                    If sInfo.Options.bShowActors Then .Actors = Actors
                                    If String.IsNullOrEmpty(.MPAA) AndAlso Not String.IsNullOrEmpty(Master.eSettings.TVScraperShowMPAANotRated) Then .MPAA = Master.eSettings.TVScraperShowMPAANotRated
                                End With
                            End If

                            'set it back
                            tmpTVDBShow.Show = tShow

                            For Each Episode As Structures.DBTV In tmpTVDBShow.Episodes

                                Episode.ShowLanguage = sInfo.ShowLang

                                eAired = Episode.TVEp.Aired
                                iEp = Episode.TVEp.Episode
                                iSeas = Episode.TVEp.Season
                                sTitle = Episode.TVEp.Title
                                byTitle = False
                                tOrdering = Enums.Ordering.Standard

                                If iEp = -1 AndAlso Not String.IsNullOrEmpty(eAired) Then iEp = -999

                                If tShow.TVShow IsNot Nothing Then Episode.TVShow = tShow.TVShow

                                If sInfo.Ordering = Enums.Ordering.DVD Then
                                    'first we need to check if dvd order is specified for every episode in the season
                                    'we'll use the regular season number as an indicator even though there are some cases
                                    'where this will not work (season 1 episode 1 = dvd_season 2 dvd_episode 1) but it
                                    'should work in most cases and is the best solution I could come up with

                                    If xdShow.Descendants("Episode").Where(Function(e) e.Element("SeasonNumber") IsNot Nothing AndAlso Convert.ToInt32(e.Element("SeasonNumber").Value) = iSeas AndAlso (e.Element("DVD_season") Is Nothing OrElse String.IsNullOrEmpty(e.Element("DVD_season").Value.ToString) OrElse e.Element("DVD_episodenumber") Is Nothing OrElse String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString))).Count = 0 Then
                                        tOrdering = Enums.Ordering.DVD
                                    End If
                                ElseIf sInfo.Ordering = Enums.Ordering.Absolute Then
                                    If xdShow.Descendants("Episode").Where(Function(e) Convert.ToInt32(e.Element("SeasonNumber").Value) > 0 AndAlso (e.Element("absolute_number") Is Nothing OrElse String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString))).Count = 0 Then
                                        tOrdering = Enums.Ordering.Absolute
                                    End If
                                End If

                                If tOrdering = Enums.Ordering.DVD Then
                                    xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) e.Element("DVD_episodenumber") IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.Element("DVD_episodenumber").Value.ToString) AndAlso Convert.ToInt32(CLng(e.Element("DVD_episodenumber").Value.ToString)) = iEp AndAlso e.Element("DVD_season") IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.Element("DVD_season").Value.ToString) AndAlso Convert.ToInt32(e.Element("DVD_season").Value) = iSeas)
                                ElseIf tOrdering = Enums.Ordering.Absolute Then
                                    If iSeas = 1 Then
                                        If Not iEp = -999 Then
                                            xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) e.Element("absolute_number") IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString) AndAlso Convert.ToInt32(e.Element("absolute_number").Value.ToString) = iEp)
                                        Else
                                            xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) e.Element("absolute_number") IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString) AndAlso Convert.ToString(e.Element("FirstAired").Value.ToString) = eAired)
                                        End If
                                    Else
                                        If Not iEp = -999 Then
                                            xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) e.Element("absolute_number") IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString) AndAlso Convert.ToInt32(e.Element("EpisodeNumber").Value.ToString) = iEp AndAlso Convert.ToInt32(e.Element("SeasonNumber").Value) = iSeas)
                                        Else
                                            xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) e.Element("absolute_number") IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.Element("absolute_number").Value.ToString) AndAlso Convert.ToString(e.Element("FirstAired").Value.ToString) = eAired)
                                        End If
                                    End If
                                Else
                                    If Not iEp = -999 Then
                                        xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) Convert.ToInt32(e.Element("EpisodeNumber").Value) = iEp AndAlso Convert.ToInt32(e.Element("SeasonNumber").Value) = iSeas)
                                    Else
                                        xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) Convert.ToString(e.Element("FirstAired").Value) = eAired)
                                    End If
                                End If

                                If xE Is Nothing Then
                                    xE = xdShow.Descendants("Episode").FirstOrDefault(Function(e) StringUtils.ComputeLevenshtein(e.Element("EpisodeName").Value, sTitle) < 5)
                                    byTitle = True
                                End If

                                If xE IsNot Nothing Then
                                    With Episode.TVEp
                                        If sInfo.Options.bEpTitle AndAlso (String.IsNullOrEmpty(.Title) OrElse Not Master.eSettings.TVLockEpisodeTitle) AndAlso Not String.IsNullOrEmpty(xE.Element("EpisodeName").Value) Then .Title = xE.Element("EpisodeName").Value
                                        If byTitle OrElse iEp = -999 Then
                                            If tOrdering = Enums.Ordering.DVD Then
                                                If sInfo.Options.bEpSeason Then .Season = If(xE.Element("DVD_season") Is Nothing OrElse String.IsNullOrEmpty(xE.Element("DVD_season").Value), 0, Convert.ToInt32(xE.Element("DVD_season").Value))
                                                If sInfo.Options.bEpEpisode Then .Episode = If(xE.Element("DVD_episodenumber") Is Nothing OrElse String.IsNullOrEmpty(xE.Element("DVD_episodenumber").Value), 0, Convert.ToInt32(xE.Element("DVD_episodenumber").Value))
                                            ElseIf tOrdering = Enums.Ordering.Absolute Then
                                                If sInfo.Options.bEpSeason Then .Season = 1
                                                If sInfo.Options.bEpEpisode Then .Episode = If(xE.Element("absolute_number") Is Nothing OrElse String.IsNullOrEmpty(xE.Element("absolute_number").Value), 0, Convert.ToInt32(xE.Element("absolute_number").Value))
                                            Else
                                                If sInfo.Options.bEpSeason Then .Season = If(xE.Element("SeasonNumber") Is Nothing OrElse String.IsNullOrEmpty(xE.Element("SeasonNumber").Value), 0, Convert.ToInt32(xE.Element("SeasonNumber").Value))
                                                If sInfo.Options.bEpEpisode Then .Episode = If(xE.Element("EpisodeNumber") Is Nothing OrElse String.IsNullOrEmpty(xE.Element("EpisodeNumber").Value), 0, Convert.ToInt32(xE.Element("EpisodeNumber").Value))
                                            End If
                                        End If
                                        If xE.Element("airsafter_season") IsNot Nothing AndAlso Not String.IsNullOrEmpty(xE.Element("airsafter_season").Value) Then
                                            .DisplaySeason = Convert.ToInt32(xE.Element("airsafter_season").Value)
                                            .DisplayEpisode = 4096
                                        End If
                                        If xE.Element("airsbefore_season") IsNot Nothing AndAlso Not String.IsNullOrEmpty(xE.Element("airsbefore_season").Value) Then
                                            .DisplaySeason = Convert.ToInt32(xE.Element("airsbefore_season").Value)
                                        End If
                                        If xE.Element("airsbefore_episode") IsNot Nothing AndAlso Not String.IsNullOrEmpty(xE.Element("airsbefore_episode").Value) Then
                                            .DisplayEpisode = Convert.ToInt32(CLng(xE.Element("airsbefore_episode").Value))
                                        End If
                                        If sInfo.Options.bEpAired Then .Aired = If(xE.Element("FirstAired") Is Nothing, .Aired, xE.Element("FirstAired").Value)
                                        If sInfo.Options.bEpRating AndAlso (String.IsNullOrEmpty(.Rating) OrElse Not Master.eSettings.TVLockEpisodeRating) Then .Rating = If(xE.Element("Rating") Is Nothing, .Rating, xE.Element("Rating").Value)
                                        If sInfo.Options.bEpVotes AndAlso (String.IsNullOrEmpty(.Votes) OrElse Not Master.eSettings.TVLockEpisodeVotes) Then .Votes = If(xE.Element("RatingCount") Is Nothing, .Votes, xE.Element("RatingCount").Value)
                                        If sInfo.Options.bEpPlot AndAlso (String.IsNullOrEmpty(.Plot) OrElse Not Master.eSettings.TVLockEpisodePlot) Then .Plot = If(xE.Element("Overview") Is Nothing, .Plot, xE.Element("Overview").Value.ToString.Trim)
                                        If sInfo.Options.bEpDirector Then .Director = If(xE.Element("Director") Is Nothing, .Director, String.Join(" / ", xE.Element("Director").Value.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|"))))
                                        If sInfo.Options.bEpCredits Then .OldCredits = If(xE.Element("Writer") Is Nothing, .OldCredits, String.Join(" / ", xE.Element("Writer").Value.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|"))))
                                        If sInfo.Options.bEpActors Then
                                            .Actors.Clear()
                                            .Actors.AddRange(Actors)
                                        End If
                                        If sInfo.Options.bEpGuestStars Then .Actors.AddRange(GuestStarsToActors(xE.Element("GuestStars").Value))
                                        .PosterURL = If(xE.Element("filename") Is Nothing OrElse String.IsNullOrEmpty(xE.Element("filename").Value), String.Empty, String.Format("http://{0}/banners/{1}", _TVDBMirror, xE.Element("filename").Value))
                                        .LocalFile = If(xE.Element("filename") Is Nothing OrElse String.IsNullOrEmpty(xE.Element("filename").Value), String.Empty, Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "episodeposters", Path.DirectorySeparatorChar, xE.Element("filename").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))))
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
                If ImagesOnly OrElse tShow.TVShow IsNot Nothing Then
                    If Not String.IsNullOrEmpty(bXML) Then
                        Dim xdImage As XDocument = XDocument.Parse(bXML)
                        For Each tImage As XElement In xdImage.Descendants("Banner")
                            If (tImage.Element("BannerPath") IsNot Nothing AndAlso Not String.IsNullOrEmpty(tImage.Element("BannerPath").Value)) AndAlso _
                               (Not CBool(clsAdvancedSettings.GetSetting("OnlyGetTVImagesForSelectedLanguage", "True")) OrElse ((tImage.Element("Language") IsNot Nothing AndAlso tImage.Element("Language").Value = clsAdvancedSettings.GetSetting("TVDBLanguage", "en")) OrElse _
                               ((tImage.Element("Language") Is Nothing OrElse tImage.Element("Language").Value = "en") AndAlso CBool(clsAdvancedSettings.GetSetting("AlwaysGetEnglishTVImages", "True"))))) Then
                                Select Case tImage.Element("BannerType").Value
                                    Case "fanart"
                                        tmpTVDBShow.Fanarts.Add(New MediaContainers.Image With { _
                                                             .URL = String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("BannerPath").Value), _
                                                             .ThumbURL = If(tImage.Element("ThumbnailPath") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("ThumbnailPath").Value), String.Empty, String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("ThumbnailPath").Value)), _
                                                             .Height = If(tImage.Element("BannerType2") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("BannerType2").Value), New Size With {.Width = 0, .Height = 0}, StringUtils.StringToSize(tImage.Element("BannerType2").Value)).Height.ToString, _
                                                             .Width = If(tImage.Element("BannerType2") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("BannerType2").Value), New Size With {.Width = 0, .Height = 0}, StringUtils.StringToSize(tImage.Element("BannerType2").Value)).Width.ToString, _
                                                             .VoteAverage = If(tImage.Element("Rating") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Rating").Value), "0", tImage.Element("Rating").Value), _
                                                             .VoteCount = If(tImage.Element("RatingCount") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("RatingCount").Value), 0, CInt(tImage.Element("RatingCount").Value)), _
                                                             .LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "fanart", Path.DirectorySeparatorChar, tImage.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                             .LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "fanart", Path.DirectorySeparatorChar, tImage.Element("ThumbnailPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                             .ShortLang = If(tImage.Element("Language") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, tImage.Element("Language").Value), _
                                                             .LongLang = If(tImage.Element("Language") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, Localization.ISOGetLangByCode2(tImage.Element("Language").Value))})
                                    Case "poster"
                                        tmpTVDBShow.Posters.Add(New MediaContainers.Image With { _
                                                              .URL = String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("BannerPath").Value), _
                                                              .Height = If(tImage.Element("BannerType2") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("BannerType2").Value), New Size With {.Width = 0, .Height = 0}, StringUtils.StringToSize(tImage.Element("BannerType2").Value)).Height.ToString, _
                                                              .Width = If(tImage.Element("BannerType2") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("BannerType2").Value), New Size With {.Width = 0, .Height = 0}, StringUtils.StringToSize(tImage.Element("BannerType2").Value)).Width.ToString, _
                                                              .VoteAverage = If(tImage.Element("Rating") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Rating").Value), "0", tImage.Element("Rating").Value), _
                                                              .VoteCount = If(tImage.Element("RatingCount") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("RatingCount").Value), 0, CInt(tImage.Element("RatingCount").Value)), _
                                                              .LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "posters", Path.DirectorySeparatorChar, tImage.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                              .ShortLang = If(tImage.Element("Language") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, tImage.Element("Language").Value), _
                                                              .LongLang = If(tImage.Element("Language") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, Localization.ISOGetLangByCode2(tImage.Element("Language").Value))})
                                    Case "season"
                                        If tImage.Element("BannerType2").Value.ToLower = "season" Then
                                            tmpTVDBShow.SeasonPosters.Add(New MediaContainers.Image With { _
                                                                    .URL = String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("BannerPath").Value), _
                                                                    .Season = If(tImage.Element("Season") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Season").Value), 0, Convert.ToInt32(tImage.Element("Season").Value)), _
                                                                    .VoteAverage = If(tImage.Element("Rating") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Rating").Value), "0", tImage.Element("Rating").Value), _
                                                                    .VoteCount = If(tImage.Element("RatingCount") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("RatingCount").Value), 0, CInt(tImage.Element("RatingCount").Value)), _
                                                                    .LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seasonposters", Path.DirectorySeparatorChar, tImage.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                                    .ShortLang = If(tImage.Element("Language") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, tImage.Element("Language").Value), _
                                                                    .LongLang = If(tImage.Element("Language") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, Localization.ISOGetLangByCode2(tImage.Element("Language").Value))})
                                        Else
                                            tmpTVDBShow.SeasonBanners.Add(New MediaContainers.Image With { _
                                                                    .URL = String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("BannerPath").Value), _
                                                                    .Season = If(tImage.Element("Season") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Season").Value), 0, Convert.ToInt32(tImage.Element("Season").Value)), _
                                                                    .VoteAverage = If(tImage.Element("Rating") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Rating").Value), "0", tImage.Element("Rating").Value), _
                                                                    .VoteCount = If(tImage.Element("RatingCount") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("RatingCount").Value), 0, CInt(tImage.Element("RatingCount").Value)), _
                                                                    .LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seasonposters", Path.DirectorySeparatorChar, tImage.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                                    .ShortLang = If(tImage.Element("Language") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, tImage.Element("Language").Value), _
                                                                    .LongLang = If(tImage.Element("Language") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, Localization.ISOGetLangByCode2(tImage.Element("Language").Value))})
                                        End If
                                    Case "series"
                                        tmpTVDBShow.ShowBanners.Add(New MediaContainers.Image With { _
                                                              .URL = String.Format("http://{0}/banners/{1}", _TVDBMirror, tImage.Element("BannerPath").Value), _
                                                              .TVBannerType = If(tImage.Element("BannerType2") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("BannerType2").Value), Enums.TVBannerType.Any, StringToShowPosterType(tImage.Element("BannerType2").Value)), _
                                                              .VoteAverage = If(tImage.Element("Rating") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Rating").Value), "0", tImage.Element("Rating").Value), _
                                                              .VoteCount = If(tImage.Element("RatingCount") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("RatingCount").Value), 0, CInt(tImage.Element("RatingCount").Value)), _
                                                              .LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seriesposters", Path.DirectorySeparatorChar, tImage.Element("BannerPath").Value.Replace(Convert.ToChar("/"), Path.DirectorySeparatorChar))), _
                                                              .ShortLang = If(tImage.Element("Language") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, tImage.Element("Language").Value), _
                                                              .LongLang = If(tImage.Element("Language") Is Nothing OrElse String.IsNullOrEmpty(tImage.Element("Language").Value), String.Empty, Localization.ISOGetLangByCode2(tImage.Element("Language").Value))})
                                End Select
                            End If
                        Next
                    End If

                    'get external scraper images

                    If tShow.TVShow Is Nothing OrElse String.IsNullOrEmpty(tShow.TVShow.TVDBID) Then
                        tShow.TVShow = New MediaContainers.TVShow With {.TVDBID = sInfo.TVDBID}
                    End If

                    'Banner AllSeasons/Show
                    Dim aList As New List(Of MediaContainers.Image)
                    If Not ModulesManager.Instance.ScrapeImage_TV(tShow, Enums.ScraperCapabilities_TV.ShowBanner, aList) Then
                        If aList.Count > 0 Then
                            For Each img In aList.Where(Function(f) f.Description = "original")
                                img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seriesposters\graphical", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seriesposters\_cache\graphical", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                tmpTVDBShow.ShowBanners.Add(img)
                            Next
                        End If
                    End If

                    'Banner Season
                    aList = New List(Of MediaContainers.Image)
                    If Not ModulesManager.Instance.ScrapeImage_TV(tShow, Enums.ScraperCapabilities_TV.SeasonBanner, aList) Then
                        If aList.Count > 0 Then
                            For Each img In aList.Where(Function(f) f.Description = "original")
                                img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seasonposters\seasons", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seasonposters\_cache\seasons", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                tmpTVDBShow.SeasonBanners.Add(img)
                            Next
                        End If
                    End If

                    'CharacterArt Show
                    aList = New List(Of MediaContainers.Image)
                    If Not ModulesManager.Instance.ScrapeImage_TV(tShow, Enums.ScraperCapabilities_TV.ShowCharacterArt, aList) Then
                        If aList.Count > 0 Then
                            For Each img In aList.Where(Function(f) f.Description = "original")
                                img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seriescharacterarts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seriescharacterarts\_cache", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                tmpTVDBShow.ShowCharacterArts.Add(img)
                            Next
                        End If
                    End If

                    'ClearArt Show
                    aList = New List(Of MediaContainers.Image)
                    If Not ModulesManager.Instance.ScrapeImage_TV(tShow, Enums.ScraperCapabilities_TV.ShowClearArt, aList) Then
                        If aList.Count > 0 Then
                            For Each img In aList.Where(Function(f) f.Description = "original")
                                img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seriescleararts", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seriescleararts\_cache", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                tmpTVDBShow.ShowClearArts.Add(img)
                            Next
                        End If
                    End If

                    'ClearLogo Show
                    aList = New List(Of MediaContainers.Image)
                    If Not ModulesManager.Instance.ScrapeImage_TV(tShow, Enums.ScraperCapabilities_TV.ShowClearLogo, aList) Then
                        If aList.Count > 0 Then
                            For Each img In aList.Where(Function(f) f.Description = "original")
                                img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seriesclearlogos", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seriesclearlogos\_cache", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                tmpTVDBShow.ShowClearLogos.Add(img)
                            Next
                        End If
                    End If

                    'Fanart AllSeasons/Season/Show
                    aList = New List(Of MediaContainers.Image)
                    If Not ModulesManager.Instance.ScrapeImage_TV(tShow, Enums.ScraperCapabilities_TV.ShowFanart, aList) Then
                        If aList.Count > 0 Then
                            For Each img In aList.Where(Function(f) f.Description = "original")
                                img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "fanart\fanart\original", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "fanart\_cache\fanart\original", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                tmpTVDBShow.Fanarts.Add(img)
                            Next
                        End If
                    End If

                    'Landscape AllSeasons/Show
                    aList = New List(Of MediaContainers.Image)
                    If Not ModulesManager.Instance.ScrapeImage_TV(tShow, Enums.ScraperCapabilities_TV.ShowLandscape, aList) Then
                        If aList.Count > 0 Then
                            For Each img In aList.Where(Function(f) f.Description = "original")
                                img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "serieslandscapes", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "serieslandscapes\_cache", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                tmpTVDBShow.ShowLandscapes.Add(img)
                            Next
                        End If
                    End If

                    'Landscape Season
                    aList = New List(Of MediaContainers.Image)
                    If Not ModulesManager.Instance.ScrapeImage_TV(tShow, Enums.ScraperCapabilities_TV.SeasonLandscape, aList) Then
                        If aList.Count > 0 Then
                            For Each img In aList.Where(Function(f) f.Description = "original")
                                img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seasonlandscapes", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seasonlandscapes\_cache", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                tmpTVDBShow.SeasonLandscapes.Add(img)
                            Next
                        End If
                    End If

                    'Poster AllSeasons/Show
                    aList = New List(Of MediaContainers.Image)
                    If Not ModulesManager.Instance.ScrapeImage_TV(tShow, Enums.ScraperCapabilities_TV.ShowPoster, aList) Then
                        If aList.Count > 0 Then
                            For Each img In aList.Where(Function(f) f.Description = "original")
                                img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "posters\posters", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "posters\_cache\posters", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                tmpTVDBShow.Posters.Add(img)
                            Next
                        End If
                    End If

                    'Poster Season
                    aList = New List(Of MediaContainers.Image)
                    If Not ModulesManager.Instance.ScrapeImage_TV(tShow, Enums.ScraperCapabilities_TV.SeasonPoster, aList) Then
                        If aList.Count > 0 Then
                            For Each img In aList.Where(Function(f) f.Description = "original")
                                img.LocalFile = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seasonposters\seasons", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                img.LocalThumb = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID, Path.DirectorySeparatorChar, "seasonposters\_cache\seasons", Path.DirectorySeparatorChar, Path.GetFileName(img.URL)))
                                tmpTVDBShow.SeasonPosters.Add(img)
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Private Function GuestStarsToActors(ByVal sGStars As String) As List(Of MediaContainers.Person)
            Dim gActors As New List(Of MediaContainers.Person)
            Dim gRole As String = Master.eLang.GetString(947, "Guest Star")

            If Not String.IsNullOrEmpty(sGStars) Then
                If sGStars.Contains("|") Then
                    For Each gStar In sGStars.Trim(Convert.ToChar("|")).Split(Convert.ToChar("|"))
                        If Not String.IsNullOrEmpty(gStar) Then
                            gActors.Add(New MediaContainers.Person With {.Name = gStar, .Role = gRole})
                        End If
                    Next
                ElseIf sGStars.Contains(",") Then
                    For Each gStar In sGStars.Trim(Convert.ToChar(",")).Split(Convert.ToChar(","))
                        If Not String.IsNullOrEmpty(gStar) Then
                            gActors.Add(New MediaContainers.Person With {.Name = gStar, .Role = gRole})
                        End If
                    Next
                End If
            End If
            Return gActors
        End Function

        Private Function StringToShowPosterType(ByVal sType As String) As Enums.TVBannerType
            Select Case sType.ToLower
                Case "blank"
                    Return Enums.TVBannerType.Blank
                Case "graphical"
                    Return Enums.TVBannerType.Graphical
                Case "text"
                    Return Enums.TVBannerType.Text
                Case Else
                    Return Enums.TVBannerType.Any
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
    Public Class TVDBSeasonImage

#Region "Fields"

        Private _alreadysaved As Boolean
        Private _banner As MediaContainers.Image
        Private _fanart As MediaContainers.Image
        Private _landscape As MediaContainers.Image
        Private _poster As MediaContainers.Image
        Private _season As Integer

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property AlreadySaved() As Boolean
            Get
                Return Me._alreadysaved
            End Get
            Set(ByVal value As Boolean)
                Me._alreadysaved = value
            End Set
        End Property

        Public Property Banner() As MediaContainers.Image
            Get
                Return Me._banner
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._banner = value
            End Set
        End Property

        Public Property Fanart() As MediaContainers.Image
            Get
                Return Me._fanart
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._fanart = value
            End Set
        End Property

        Public Property Landscape() As MediaContainers.Image
            Get
                Return Me._landscape
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._landscape = value
            End Set
        End Property

        Public Property Poster() As MediaContainers.Image
            Get
                Return Me._poster
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._poster = value
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
            Me._alreadysaved = False
            Me._banner = New MediaContainers.Image
            Me._fanart = New MediaContainers.Image
            Me._landscape = New MediaContainers.Image
            Me._poster = New MediaContainers.Image
            Me._season = -1
        End Sub

#End Region 'Methods

    End Class

    Public Class TVDBShow

#Region "Fields"

        Private _allseason As Structures.DBTV
        Private _episodes As New List(Of Structures.DBTV)
        Private _fanarts As New List(Of MediaContainers.Image)
        Private _posters As New List(Of MediaContainers.Image)
        Private _seasonposters As New List(Of MediaContainers.Image)
        Private _seasonbanners As New List(Of MediaContainers.Image)
        Private _seasonlandscapes As New List(Of MediaContainers.Image)
        Private _show As Structures.DBTV
        Private _showbanners As New List(Of MediaContainers.Image)
        Private _showcharacterarts As New List(Of MediaContainers.Image)
        Private _showcleararts As New List(Of MediaContainers.Image)
        Private _showclearlogos As New List(Of MediaContainers.Image)
        Private _showlandscapes As New List(Of MediaContainers.Image)

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

        Public Property Fanarts() As List(Of MediaContainers.Image)
            Get
                Return Me._fanarts
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._fanarts = value
            End Set
        End Property

        Public Property Posters() As List(Of MediaContainers.Image)
            Get
                Return Me._posters
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._posters = value
            End Set
        End Property

        Public Property SeasonPosters() As List(Of MediaContainers.Image)
            Get
                Return Me._seasonposters
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._seasonposters = value
            End Set
        End Property

        Public Property SeasonBanners() As List(Of MediaContainers.Image)
            Get
                Return Me._seasonbanners
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._seasonbanners = value
            End Set
        End Property

        Public Property SeasonLandscapes() As List(Of MediaContainers.Image)
            Get
                Return Me._seasonlandscapes
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._seasonlandscapes = value
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

        Public Property ShowBanners() As List(Of MediaContainers.Image)
            Get
                Return Me._showbanners
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._showbanners = value
            End Set
        End Property

        Public Property ShowCharacterArts() As List(Of MediaContainers.Image)
            Get
                Return Me._showcharacterarts
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._showcharacterarts = value
            End Set
        End Property

        Public Property ShowClearArts() As List(Of MediaContainers.Image)
            Get
                Return Me._showcleararts
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._showcleararts = value
            End Set
        End Property

        Public Property ShowClearLogos() As List(Of MediaContainers.Image)
            Get
                Return Me._showclearlogos
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._showclearlogos = value
            End Set
        End Property

        Public Property ShowLandscapes() As List(Of MediaContainers.Image)
            Get
                Return Me._showlandscapes
            End Get
            Set(ByVal value As List(Of MediaContainers.Image))
                Me._showlandscapes = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._show = New Structures.DBTV
            Me._allseason = New Structures.DBTV
            Me._episodes = New List(Of Structures.DBTV)
            Me._fanarts = New List(Of MediaContainers.Image)
            Me._showbanners = New List(Of MediaContainers.Image)
            Me._showcharacterarts = New List(Of MediaContainers.Image)
            Me._showcleararts = New List(Of MediaContainers.Image)
            Me._showclearlogos = New List(Of MediaContainers.Image)
            Me._showlandscapes = New List(Of MediaContainers.Image)
            Me._seasonposters = New List(Of MediaContainers.Image)
            Me._seasonbanners = New List(Of MediaContainers.Image)
            Me._seasonlandscapes = New List(Of MediaContainers.Image)
            Me._posters = New List(Of MediaContainers.Image)
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
