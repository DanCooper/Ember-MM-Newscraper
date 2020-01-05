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
Imports System.Windows.Forms

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

    Private Shared Function GetAddonEventType(ByRef dbelement As Database.DBElement, ByVal type As ScrapingType) As Enums.AddonEventType
        Select Case dbelement.ContentType
            Case Enums.ContentType.Movie
                Select Case type
                    Case ScrapingType.PreCheck
                        Return Enums.AddonEventType.Scrape_Movie_PreCheck
                    Case ScrapingType.Scrape
                        Return Enums.AddonEventType.Scrape_Movie
                    Case ScrapingType.Search
                        Return Enums.AddonEventType.Search_Movie
                End Select
            Case Enums.ContentType.Movieset
                Select Case type
                    Case ScrapingType.PreCheck
                        Return Enums.AddonEventType.Scrape_Movieset_PreCheck
                    Case ScrapingType.Scrape
                        Return Enums.AddonEventType.Scrape_Movieset
                    Case ScrapingType.Search
                        Return Enums.AddonEventType.Search_Movieset
                End Select
            Case Enums.ContentType.TVEpisode
                Select Case type
                    Case ScrapingType.PreCheck
                        Return Enums.AddonEventType.Scrape_TVEpisode_PreCheck
                    Case ScrapingType.Scrape
                        Return Enums.AddonEventType.Scrape_TVEpisode
                    Case ScrapingType.Search
                        Return Enums.AddonEventType.Search_TVEpisode
                End Select
            Case Enums.ContentType.TVSeason
                Select Case type
                    Case ScrapingType.PreCheck
                        Return Enums.AddonEventType.Scrape_TVSeason_PreCheck
                    Case ScrapingType.Scrape
                        Return Enums.AddonEventType.Scrape_TVSeason
                    Case ScrapingType.Search
                        Return Enums.AddonEventType.Search_TVSeason
                End Select
            Case Enums.ContentType.TVShow
                Select Case type
                    Case ScrapingType.PreCheck
                        Return Enums.AddonEventType.Scrape_TVShow_PreCheck
                    Case ScrapingType.Scrape
                        Return Enums.AddonEventType.Scrape_TVShow
                    Case ScrapingType.Search
                        Return Enums.AddonEventType.Search_TVShow
                End Select
        End Select
        Return Nothing
    End Function

    Public Shared Function Run(ByRef dbElement As Database.DBElement) As ScrapeResults
        'create a clone of DBMovie
        Dim cloneDBElement As Database.DBElement = CType(dbElement.CloneDeep, Database.DBElement)
        Dim nScrapeResults As New ScrapeResults
        Dim nSearchResults As New List(Of MediaContainers.MainDetails)
        Select Case cloneDBElement.ContentType
            Case Enums.ContentType.Movie
                If ScrapePreCheck(cloneDBElement) Then
                    nScrapeResults = Scrape(cloneDBElement)
                Else
                    nSearchResults = Search(cloneDBElement)
                    If nSearchResults.Count = 1 Then
                        cloneDBElement.MainDetails = nSearchResults(0)
                        nScrapeResults = Scrape(cloneDBElement)
                    End If
                End If
        End Select
        If nScrapeResults IsNot Nothing Then
            Select Case dbElement.ScrapeType
                Case Enums.ScrapeType.Auto, Enums.ScrapeType.Auto, Enums.ScrapeType.Skip
                    'Merge scraperresults considering global datascraper settings
                    dbElement = Information.MergeResults(dbElement, nScrapeResults.lstData, dbElement.ScrapeOptions, dbElement.ScrapeModifiers.withEpisodes)
                    dbElement.MainDetails.CreateCachePaths_ActorsThumbs()

                    'Images
                    nScrapeResults.lstImages.SortAndFilter(dbElement)
                    nScrapeResults.lstImages.CreateCachePaths(dbElement)
                    Images.SetPreferredImages(dbElement, nScrapeResults.lstImages, dbElement.ScrapeModifiers)

                    'MetaData
                    If Master.eSettings.Movie.DataSettings.MetadataScan.Enabled AndAlso dbElement.ScrapeModifiers.MainMeta Then
                        MetaData.UpdateFileInfo(dbElement)
                    End If

                    'Theme
                    Dim newPreferredTheme As New MediaContainers.Theme
                    If Themes.GetPreferred(nScrapeResults.lstThemes, newPreferredTheme, dbElement.ContentType) Then
                        dbElement.Theme = newPreferredTheme
                    End If

                    'Trailer
                    Dim newPreferredTrailer As New MediaContainers.Trailer
                    If Trailers.GetPreferred(nScrapeResults.lstTrailers, newPreferredTrailer, dbElement.ContentType) Then
                        dbElement.Trailer = newPreferredTrailer
                    End If

                    'Save to DB
                    dbElement.Save(False, dbElement.ScrapeModifiers.MainNFO OrElse dbElement.ScrapeModifiers.MainMeta, True, True, False)
            End Select
        End If
        Return nScrapeResults
    End Function

    Private Shared Function Scrape(ByRef dbElement As Database.DBElement) As ScrapeResults
        _Logger.Trace(String.Format("[Scraper] [Scrape] [Start] {0}", dbElement.FileItem.FirstPathFromStack))
        While Not AddonsManager.Instance.AllAddonsLoaded
            Application.DoEvents()
        End While
        Dim nAddonEventType = GetAddonEventType(dbElement, ScrapingType.Scrape)
        Dim nScrapeResults As New ScrapeResults
        If dbElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(dbElement, False) Then
            For Each cAddon In AddonsManager.Instance.Addons
                _Logger.Trace(String.Format("[Scraper] [Scrape] [Using] {0}", cAddon.AssemblyName))
                Dim nAddonResult = cAddon.ProcessorModule.Run(dbElement, nAddonEventType, Nothing)
                If nAddonResult IsNot Nothing Then
                    If nAddonResult.ScraperResult_Data IsNot Nothing Then
                        nAddonResult.ScraperResult_Data.Scrapersource = cAddon.AssemblyName
                        nScrapeResults.lstData.Add(nAddonResult.ScraperResult_Data)
                        'set new informations for following scrapers
                        If nAddonResult.ScraperResult_Data.UniqueIDs.IMDbIdSpecified Then
                            dbElement.MainDetails.UniqueIDs.IMDbId = nAddonResult.ScraperResult_Data.UniqueIDs.IMDbId
                        End If
                        If nAddonResult.ScraperResult_Data.OriginalTitleSpecified Then
                            dbElement.MainDetails.OriginalTitle = nAddonResult.ScraperResult_Data.OriginalTitle
                        End If
                        If nAddonResult.ScraperResult_Data.TitleSpecified Then
                            dbElement.MainDetails.Title = nAddonResult.ScraperResult_Data.Title
                        End If
                        If nAddonResult.ScraperResult_Data.UniqueIDs.TMDbIdSpecified Then
                            dbElement.MainDetails.UniqueIDs.TMDbId = nAddonResult.ScraperResult_Data.UniqueIDs.TMDbId
                        End If
                        If nAddonResult.ScraperResult_Data.UniqueIDs.TVDbIdSpecified Then
                            dbElement.MainDetails.UniqueIDs.TVDbId = nAddonResult.ScraperResult_Data.UniqueIDs.TVDbId
                        End If
                        If nAddonResult.ScraperResult_Data.YearSpecified Then
                            dbElement.MainDetails.Year = nAddonResult.ScraperResult_Data.Year
                        End If
                    End If

                    If nAddonResult.ScraperResult_ImageContainer IsNot Nothing Then
                        nScrapeResults.lstImages.EpisodeFanarts.AddRange(nAddonResult.ScraperResult_ImageContainer.EpisodeFanarts)
                        nScrapeResults.lstImages.EpisodePosters.AddRange(nAddonResult.ScraperResult_ImageContainer.EpisodePosters)
                        nScrapeResults.lstImages.MainBanners.AddRange(nAddonResult.ScraperResult_ImageContainer.MainBanners)
                        nScrapeResults.lstImages.MainCharacterArts.AddRange(nAddonResult.ScraperResult_ImageContainer.MainCharacterArts)
                        nScrapeResults.lstImages.MainClearArts.AddRange(nAddonResult.ScraperResult_ImageContainer.MainClearArts)
                        nScrapeResults.lstImages.MainClearLogos.AddRange(nAddonResult.ScraperResult_ImageContainer.MainClearLogos)
                        nScrapeResults.lstImages.MainDiscArts.AddRange(nAddonResult.ScraperResult_ImageContainer.MainDiscArts)
                        nScrapeResults.lstImages.MainFanarts.AddRange(nAddonResult.ScraperResult_ImageContainer.MainFanarts)
                        nScrapeResults.lstImages.MainLandscapes.AddRange(nAddonResult.ScraperResult_ImageContainer.MainLandscapes)
                        nScrapeResults.lstImages.MainPosters.AddRange(nAddonResult.ScraperResult_ImageContainer.MainPosters)
                        nScrapeResults.lstImages.SeasonBanners.AddRange(nAddonResult.ScraperResult_ImageContainer.SeasonBanners)
                        nScrapeResults.lstImages.SeasonFanarts.AddRange(nAddonResult.ScraperResult_ImageContainer.SeasonFanarts)
                        nScrapeResults.lstImages.SeasonLandscapes.AddRange(nAddonResult.ScraperResult_ImageContainer.SeasonLandscapes)
                        nScrapeResults.lstImages.SeasonPosters.AddRange(nAddonResult.ScraperResult_ImageContainer.SeasonPosters)
                    End If

                    If nAddonResult.ScraperResult_Themes IsNot Nothing Then
                        nScrapeResults.lstThemes.AddRange(nAddonResult.ScraperResult_Themes)
                    End If

                    If nAddonResult.ScraperResult_Trailers IsNot Nothing Then
                        nScrapeResults.lstTrailers.AddRange(nAddonResult.ScraperResult_Trailers)
                    End If
                End If
                _Logger.Trace(String.Format("[Scraper] [Scrape] [EndUsing] {0}", cAddon.AssemblyName))
            Next
            If nScrapeResults.bCancelled OrElse nScrapeResults.bError Then
                _Logger.Trace(String.Format("[Scraper] [Scrape] [Abort] [Error] {0}", dbElement.FileItem.FirstPathFromStack))
                Return Nothing
            End If
        Else
            _Logger.Trace(String.Format("[Scraper] [Scrape] [Abort] [Media Offline] {0}", dbElement.FileItem.FirstPathFromStack))
            Return Nothing
        End If

        _Logger.Trace(String.Format("[Scraper] [Scrape] [Done] {0}", dbElement.FileItem.FirstPathFromStack))
        Return nScrapeResults
    End Function

    Private Shared Function ScrapePreCheck(ByRef dbelement As Database.DBElement) As Boolean
        _Logger.Trace(String.Format("[Scraper] [ScrapePreCheck] [Start] {0}", dbelement.FileItem.FirstPathFromStack))
        Dim nAddonEventType = GetAddonEventType(dbelement, ScrapingType.PreCheck)
        For Each cAddon In AddonsManager.Instance.Addons
            _Logger.Trace(String.Format("[Scraper] [ScrapePreCheck] [Using] {0}", cAddon.AssemblyName))
            Dim nAddonResult = cAddon.ProcessorModule.Run(dbelement, nAddonEventType, Nothing)
            If nAddonResult IsNot Nothing AndAlso Not nAddonResult.bPreCheckSuccessful Then
                _Logger.Trace(String.Format("[Scraper] [ScrapePreCheck] [Abort] PreCheck not successful {0}", cAddon.AssemblyName))
                Return False
            End If
        Next
        _Logger.Trace(String.Format("[Scraper] [ScrapePreCheck] [Done] {0}", dbelement.FileItem.FirstPathFromStack))
        Return True
    End Function

    Private Shared Function Search(ByRef dbelement As Database.DBElement) As List(Of MediaContainers.MainDetails)
        _Logger.Trace(String.Format("[Scraper] [Search] [Start] {0}", dbelement.FileItem.FirstPathFromStack))
        While Not AddonsManager.Instance.AllAddonsLoaded
            Application.DoEvents()
        End While

        Dim nAddonEventType = GetAddonEventType(dbelement, ScrapingType.Search)
        Dim nSearchResluts As New List(Of MediaContainers.MainDetails)
        If AddonsManager.Instance.Addons.Count > 0 Then
            For Each cAddon In AddonsManager.Instance.Addons
                _Logger.Trace(String.Format("[Scraper] [Search] [Using] {0}", cAddon.AssemblyName))
                Dim nAddonResult = cAddon.ProcessorModule.Run(dbelement, nAddonEventType, Nothing)
                If nAddonResult IsNot Nothing Then
                    If nAddonResult.SearchResults IsNot Nothing Then nSearchResluts.AddRange(nAddonResult.SearchResults)
                End If
                _Logger.Trace(String.Format("[Scraper] [Search] [EndUsing] {0}", cAddon.AssemblyName))
            Next
        Else
            _Logger.Warn("[Scraper] [Search] [Abort] No addons found")
        End If

        _Logger.Trace(String.Format("[Scraper] [Search] [Done] {0}", dbelement.FileItem.FirstPathFromStack))
        Return nSearchResluts
    End Function


#End Region 'Methods

#Region "Nested Types"

    Public Class ScrapeResults

#Region "Fields"

        Public bCancelled As Boolean
        Public bError As Boolean
        Public lstData As New List(Of MediaContainers.MainDetails)
        Public lstImages As New MediaContainers.SearchResultsContainer
        Public lstThemes As New List(Of MediaContainers.Theme)
        Public lstTrailers As New List(Of MediaContainers.Trailer)

#End Region 'Fields

    End Class

    Private Enum ScrapingType
        PreCheck
        Scrape
        Search
    End Enum

#End Region 'Nested Types

End Class

Public Class ScraperProperties

#Region "Properties"

    Public Property AssemblyName() As String = String.Empty

    Public Property ScraperCapatibilities() As New List(Of Enums.ScraperCapatibility)

#End Region 'Properties

#Region "Constructors"

    Public Sub New(ByVal name As String, ByVal capatibilityList As List(Of Enums.ScraperCapatibility))
        ScraperCapatibilities = capatibilityList
        AssemblyName = name
    End Sub

#End Region 'Constructors

#Region "Methods"

#End Region 'Methods

End Class

Public Class SearchEngineProperties

#Region "Properties"

    Public Property AssemblyName() As String = String.Empty

    Public Property SearchMovie() As Boolean = False

    Public Property SearchMovieSet() As Boolean = False

    Public Property SearchTVEpisode() As Boolean = False

    Public Property SearchTVSeason() As Boolean = False

    Public Property SearchTVShow() As Boolean = False

#End Region 'Properties

#Region "Constructors"

    Public Sub New(ByVal name As String, ByVal capatibilityList As List(Of Enums.AddonEventType))
        AssemblyName = name
        If capatibilityList.Contains(Enums.AddonEventType.Search_Movie) Then SearchMovie = True
        If capatibilityList.Contains(Enums.AddonEventType.Search_Movieset) Then SearchMovieSet = True
        If capatibilityList.Contains(Enums.AddonEventType.Search_TVEpisode) Then SearchTVEpisode = True
        If capatibilityList.Contains(Enums.AddonEventType.Search_TVSeason) Then SearchTVSeason = True
        If capatibilityList.Contains(Enums.AddonEventType.Search_TVShow) Then SearchTVShow = True
    End Sub

#End Region 'Constructors

#Region "Methods"

#End Region 'Methods

End Class