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
Imports System.Drawing
Imports System.IO
Imports System.Xml.Serialization

Public Class Addons

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared AssemblyList As New List(Of AssemblyListItem)
    Public Shared VersionList As New List(Of VersionItem)

    Public Addons As New List(Of AddonClass)
    Public RuntimeObjects As New EmberRuntimeObjects

    'Singleton Instace for module manager .. allways use this one
    Private Shared Singleton As Addons = Nothing

    Friend WithEvents BwLoadAddons As New ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal eventType As Enums.AddonEventType, ByRef parameters As List(Of Object))

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property AllAddonsLoaded() As Boolean
        Get
            Return Not BwLoadAddons.IsBusy
        End Get
    End Property

    Public Shared ReadOnly Property Instance() As Addons
        Get
            If (Singleton Is Nothing) Then
                Singleton = New Addons()
            End If
            Return Singleton
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Private Sub BuildVersionList()
        VersionList.Clear()
        VersionList.Add(New VersionItem With {
                        .AssemblyFileName = "*Ember Media Manager",
                        .Version = My.Application.Info.Version.ToString()
                        })
        VersionList.Add(New VersionItem With {
                        .AssemblyFileName = "*Ember API",
                        .Version = Functions.EmberAPIVersion()
                        })
        For Each addon In Addons
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = addon.AssemblyFileName,
                            .Version = addon.AssemblyVersion
                            })
        Next
        VersionList = VersionList.Distinct.ToList
        VersionList = VersionList.OrderBy(Function(f) f.AssemblyFileName).ToList
    End Sub

    Private Sub BwLoadAddons_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BwLoadAddons.DoWork
        LoadAddons()
    End Sub

    Private Sub BwLoadAddons_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BwLoadAddons.RunWorkerCompleted
        BuildVersionList()
    End Sub

    Public Function GetMovieCollectionId(ByVal imdbOrTmdbId As String) As Integer
        Dim TMDbCollectionId As Integer = -1

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        'If Not String.IsNullOrEmpty(imdbOrTmdbId) Then
        '    Dim ret As Interfaces.AddonResult_Generic
        '    For Each addon As Data_Scraper_Movieset In Data_Scrapers_Movieset.Where(Function(e) e.AddonInterface.Name = "scraper.data.themoviedb.org")
        '        ret = addon.AddonInterface.GetTMDbCollectionId(imdbOrTmdbId, TMDbCollectionId)
        '        If ret.Status = Interfaces.ResultStatus.BreakChain Then Exit For
        '    Next
        'End If
        Return TMDbCollectionId
    End Function

    Public Function GetMovieTMDbIdByIMDbId(ByRef imdbId As String) As Integer
        Dim iTMDbId As Integer = -1

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        'If Not String.IsNullOrEmpty(imdbId) Then
        '    Dim ret As Interfaces.AddonResult_Generic
        '    For Each addon As Data_Scraper_Movie In Data_Scrapers_Movie.Where(Function(e) e.AddonInterface.Name = "scraper.data.themoviedb.org")
        '        ret = addon.AddonInterface.GetTMDbIdByIMDbId(imdbId, iTMDbId)
        '        If ret.Status = Interfaces.ResultStatus.BreakChain Then Exit For
        '    Next
        'End If
        Return iTMDbId
    End Function

    Public Sub GetVersions()
        Dim dlgVersions As New dlgVersions
        Dim li As ListViewItem
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each v As VersionItem In VersionList
            li = dlgVersions.lstVersions.Items.Add(v.AssemblyFileName)
            li.SubItems.Add(v.Version)
        Next
        dlgVersions.ShowDialog()
    End Sub

    Public Sub LoadAddons()
        _Logger.Trace("[Addons] [LoadAddons] [Start]")

        If Directory.Exists(Master.AddonsPath) Then
            'add each .dll file to AssemblyList
            For Each inDir In Directory.GetDirectories(Master.AddonsPath)
                For Each inFile As String In Directory.GetFiles(inDir, "*.dll")
                    Dim nAssembly As Reflection.Assembly = Reflection.Assembly.LoadFile(inFile)
                    AssemblyList.Add(New AssemblyListItem With {
                                     .Assembly = nAssembly,
                                     .AssemblyName = nAssembly.GetName.Name,
                                     .AssemblyVersion = nAssembly.GetName().Version
                                     })
                Next
            Next

            For Each tAssemblyItem As AssemblyListItem In AssemblyList
                'Loop through each of the assemeblies type
                For Each fileType As Type In tAssemblyItem.Assembly.GetTypes
                    Dim fType As Type = fileType.GetInterface("IAddon")
                    If Not fType Is Nothing Then
                        Dim nAddonInterface As Interfaces.IAddon
                        nAddonInterface = CType(Activator.CreateInstance(fileType), Interfaces.IAddon)

                        Dim nAddon As New AddonClass With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .AddonInterface = nAddonInterface
                        }
                        Addons.Add(nAddon)
                        nAddon.AddonInterface.Init()

                        For Each i In Master.eSettings.Addons.Where(Function(f) f.AssemblyFileName = nAddon.AssemblyFileName)
                            nAddon.AddonInterface.IsEnabled_Generic = i.Enabled
                            nAddon.AddonInterface.IsEnabled_Data_Movie = i.Enabled_Data_Movie
                            nAddon.AddonInterface.IsEnabled_Data_Movieset = i.Enabled_Data_Movieset
                            nAddon.AddonInterface.IsEnabled_Data_TV = i.Enabled_Data_TV
                            nAddon.AddonInterface.IsEnabled_Image_Movie = i.Enabled_Image_Movie
                            nAddon.AddonInterface.IsEnabled_Image_Movieset = i.Enabled_Image_Movieset
                            nAddon.AddonInterface.IsEnabled_Image_TV = i.Enabled_Image_TV
                            nAddon.AddonInterface.IsEnabled_Theme_Movie = i.Enabled_Theme_Movie
                            nAddon.AddonInterface.IsEnabled_Theme_TV = i.Enabled_Theme_TV
                            nAddon.AddonInterface.IsEnabled_Trailer_Movie = i.Enabled_Trailer_Movie
                            nAddon.Order = i.Order
                            nAddon.Order_Data_Movie = i.Order_Data_Movie
                            nAddon.Order_Data_Movieset = i.Order_Data_Movieset
                            nAddon.Order_Data_TV = i.Order_Data_TV
                            nAddon.Order_Image_Movie = i.Order_Image_Movie
                            nAddon.Order_Image_Movieset = i.Order_Image_Movieset
                            nAddon.Order_Image_TV = i.Order_Image_TV
                            nAddon.Order_Theme_Movie = i.Order_Theme_Movie
                            nAddon.Order_Theme_TV = i.Order_Theme_TV
                            nAddon.Order_Trailer_Movie = i.Order_Trailer_Movie
                        Next
                        _Logger.Trace(String.Concat("[Addons] [LoadAddons] Addon loaded: ", nAddon.AssemblyName))
                    End If
                Next
            Next

            'Addons ordering
            Dim c As Integer = 0
            For Each ext In Addons.OrderBy(Function(f) f.Order)
                ext.Order = c
                c += 1
            Next
            c = 0
            For Each ext In Addons.OrderBy(Function(f) f.Order_Data_Movie)
                ext.Order_Data_Movie = c
                c += 1
            Next
            c = 0
            For Each ext In Addons.OrderBy(Function(f) f.Order_Data_Movieset)
                ext.Order_Data_Movieset = c
                c += 1
            Next
            c = 0
            For Each ext In Addons.OrderBy(Function(f) f.Order_Data_TV)
                ext.Order_Data_TV = c
                c += 1
            Next
            c = 0
            For Each ext In Addons.OrderBy(Function(f) f.Order_Image_Movie)
                ext.Order_Image_Movie = c
                c += 1
            Next
            c = 0
            For Each ext In Addons.OrderBy(Function(f) f.Order_Image_Movieset)
                ext.Order_Image_Movieset = c
                c += 1
            Next
            c = 0
            For Each ext In Addons.OrderBy(Function(f) f.Order_Image_TV)
                ext.Order_Image_TV = c
                c += 1
            Next
            c = 0
            For Each ext In Addons.OrderBy(Function(f) f.Order_Theme_Movie)
                ext.Order_Theme_Movie = c
                c += 1
            Next
            c = 0
            For Each ext In Addons.OrderBy(Function(f) f.Order_Theme_TV)
                ext.Order_Theme_TV = c
                c += 1
            Next
            c = 0
            For Each ext In Addons.OrderBy(Function(f) f.Order_Trailer_Movie)
                ext.Order_Trailer_Movie = c
                c += 1
            Next
        End If

        _Logger.Trace("[AddonsManager] [LoadAddons] [Done]")
    End Sub

    Public Sub LoadAddons_All()
        BwLoadAddons.RunWorkerAsync()
    End Sub

    Public Function QueryAnyAddonIsBusy() As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Dim lstBusyAddons = Addons.Where(Function(e) e.AddonInterface.IsBusy)
        If lstBusyAddons.Count() > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function QueryScraperCapabilities_Image_Movie(ByVal addon As AddonClass, ByVal scrapeModifiers As Structures.ScrapeModifiers) As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        If scrapeModifiers.Banner AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Banner) Then Return True
        If scrapeModifiers.Clearart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Clearart) Then Return True
        If scrapeModifiers.Clearlogo AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Clearlogo) Then Return True
        If scrapeModifiers.Discart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Discart) Then Return True
        If scrapeModifiers.Extrafanarts AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Fanart) Then Return True
        If scrapeModifiers.Extrathumbs AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Fanart) Then Return True
        If scrapeModifiers.Fanart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Fanart) Then Return True
        If scrapeModifiers.Keyart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Poster) Then Return True
        If scrapeModifiers.Landscape AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Landscape) Then Return True
        If scrapeModifiers.Poster AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Poster) Then Return True

        Return False
    End Function

    Private Function QueryScraperCapabilities_Image_Movie(ByVal addon As AddonClass, ByVal imageType As Enums.ModifierType) As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Select Case imageType
            Case Enums.ModifierType.MainBanner
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Banner)
            Case Enums.ModifierType.MainClearArt
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Clearart)
            Case Enums.ModifierType.MainClearLogo
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Clearlogo)
            Case Enums.ModifierType.MainDiscArt
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Discart)
            Case Enums.ModifierType.MainExtrafanarts, Enums.ModifierType.MainExtrathumbs, Enums.ModifierType.MainFanart
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Fanart)
            Case Enums.ModifierType.MainKeyart, Enums.ModifierType.MainPoster
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Poster)
            Case Enums.ModifierType.MainLandscape
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Landscape)
        End Select

        Return False
    End Function

    Function QueryScraperCapabilities_Image_Movieset(ByVal addon As AddonClass, ByVal scrapeModifiers As Structures.ScrapeModifiers) As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        If scrapeModifiers.Banner AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Banner) Then Return True
        If scrapeModifiers.Clearart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Clearart) Then Return True
        If scrapeModifiers.Clearlogo AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Clearlogo) Then Return True
        If scrapeModifiers.Discart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Discart) Then Return True
        If scrapeModifiers.Fanart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Fanart) Then Return True
        If scrapeModifiers.Keyart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Poster) Then Return True
        If scrapeModifiers.Landscape AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Landscape) Then Return True
        If scrapeModifiers.Poster AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Poster) Then Return True

        Return False
    End Function

    Function QueryScraperCapabilities_Image_MovieSet(ByVal addon As AddonClass, ByVal imageType As Enums.ModifierType) As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Select Case imageType
            Case Enums.ModifierType.MainBanner
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Banner)
            Case Enums.ModifierType.MainClearArt
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Clearart)
            Case Enums.ModifierType.MainClearLogo
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Clearlogo)
            Case Enums.ModifierType.MainDiscArt
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Discart)
            Case Enums.ModifierType.MainFanart
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Fanart)
            Case Enums.ModifierType.MainKeyart, Enums.ModifierType.MainPoster
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Poster)
            Case Enums.ModifierType.MainLandscape
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movieset_Image_Landscape)
        End Select

        Return False
    End Function

    Function QueryScraperCapabilities_Image_TV(ByVal addon As AddonClass, ByVal scrapeModifiers As Structures.ScrapeModifiers) As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        If scrapeModifiers.Episodes.Fanart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVEpisode_Image_Fanart) Then Return True
        If scrapeModifiers.Episodes.Poster AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVEpisode_Image_Poster) Then Return True
        If scrapeModifiers.Banner AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Banner) Then Return True
        If scrapeModifiers.Characterart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_CharacteraArt) Then Return True
        If scrapeModifiers.Clearart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Clearart) Then Return True
        If scrapeModifiers.Clearlogo AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Clearlogo) Then Return True
        If scrapeModifiers.Fanart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Fanart) Then Return True
        If scrapeModifiers.Keyart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Poster) Then Return True
        If scrapeModifiers.Landscape AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Landscape) Then Return True
        If scrapeModifiers.Poster AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Poster) Then Return True
        If scrapeModifiers.Seasons.Banner AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Banner) Then Return True
        If scrapeModifiers.Seasons.Fanart AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Fanart) Then Return True
        If scrapeModifiers.Seasons.Landscape AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Landscape) Then Return True
        If scrapeModifiers.Seasons.Poster AndAlso addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Poster) Then Return True

        Return False
    End Function

    Function QueryScraperCapabilities_Image_TV(ByVal addon As AddonClass, ByVal imageType As Enums.ModifierType) As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Select Case imageType
            Case Enums.ModifierType.AllSeasonsBanner
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Banner) OrElse
                    addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Banner)
            Case Enums.ModifierType.AllSeasonsFanart
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Fanart) OrElse
                    addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Fanart)
            Case Enums.ModifierType.AllSeasonsLandscape
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Landscape) OrElse
                    addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Landscape)
            Case Enums.ModifierType.AllSeasonsPoster
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Poster) OrElse
                    addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Poster)
            Case Enums.ModifierType.EpisodeFanart
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Fanart) OrElse
                    addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVEpisode_Image_Fanart)
            Case Enums.ModifierType.EpisodePoster
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVEpisode_Image_Poster)
            Case Enums.ModifierType.MainBanner
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Banner)
            Case Enums.ModifierType.MainCharacterArt
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_CharacteraArt)
            Case Enums.ModifierType.MainClearArt
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Clearart)
            Case Enums.ModifierType.MainClearLogo
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Clearlogo)
            Case Enums.ModifierType.MainExtrafanarts, Enums.ModifierType.MainFanart
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Fanart)
            Case Enums.ModifierType.MainKeyart, Enums.ModifierType.MainPoster
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Poster)
            Case Enums.ModifierType.MainLandscape
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Landscape)
            Case Enums.ModifierType.SeasonBanner
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Banner)
            Case Enums.ModifierType.SeasonFanart
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVShow_Image_Fanart) OrElse
                    addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Fanart)
            Case Enums.ModifierType.SeasonLandscape
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Landscape)
            Case Enums.ModifierType.SeasonPoster
                Return addon.AddonInterface.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.TVSeason_Image_Poster)
        End Select

        Return False
    End Function
    ''' <summary>
    ''' Calls all the generic addons of the supplied type (if one is defined), passing the supplied _params.
    ''' The module will do its task and return any expected results in the _refparams.
    ''' </summary>
    ''' <param name="eventType">The <c>Enums.ModuleEventType</c> of module to execute.</param>
    ''' <param name="parameters">Parameters to pass to the module</param>
    ''' <param name="singleObjekt"><c>Object</c> representing the module's result (if relevant)</param>
    ''' <param name="runOnlyOne">If <c>True</c>, allow only one module to perform the required task.</param>
    ''' <returns></returns>
    ''' <remarks>Note that if any module returns a result of breakChain, no further addons are processed</remarks>
    Public Function RunGeneric(ByVal eventType As Enums.AddonEventType, ByRef parameters As List(Of Object), Optional ByVal singleObjekt As Object = Nothing, Optional ByVal runOnlyOne As Boolean = False, Optional ByRef dbElement As Database.DBElement = Nothing) As Boolean
        _Logger.Trace(String.Format("[Addons] [RunGeneric] [Start] <{0}>", eventType.ToString))
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Dim Result As New Interfaces.AddonResult
        Try
            Dim lstAddons = Addons.Where(Function(e) e.AddonInterface.Capabilities_AddonEventTypes.Contains(eventType) AndAlso e.AddonInterface.IsEnabled_Generic)
            If (lstAddons.Count() <= 0) Then
                _Logger.Trace("[AddonsManager] [RunGeneric] No generic addons defined <{0}>", eventType.ToString)
            Else
                For Each Addon In lstAddons
                    Try
                        _Logger.Trace("[AddonsManager] [RunGeneric] Run generic module <{0}>", Addon.AssemblyFileName)
                        Result = Addon.AddonInterface.Run(dbElement, eventType, parameters)
                    Catch ex As Exception
                        _Logger.Error("[AddonsManager] [RunGeneric] Run generic module <{0}>", Addon.AssemblyFileName)
                        _Logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                    If Result.Status = Interfaces.ResultStatus.BreakChain OrElse runOnlyOne Then Exit For
                Next
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return Result.Status = Interfaces.ResultStatus.Cancelled
    End Function

    'Public Function ScrapeData_Movie(ByRef dbElement As Database.DBElement,
    '                                 ByRef scrapeModifiers As Structures.ScrapeModifiers,
    '                                 ByVal scrapeType As Enums.ScrapeType,
    '                                 ByVal scrapeOptions As Structures.ScrapeOptions,
    '                                 ByVal showMessage As Boolean
    '                                 ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Start] {0}", dbElement.Filename))
    '    If dbElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(dbElement, showMessage) Then
    '        Dim Addons As IEnumerable(Of Data_Scraper_Movie) = Data_Scrapers_Movie.Where(Function(e) e.AddonInterface.Enabled).OrderBy(Function(e) e.Order)
    '        Dim Result As Interfaces.AddonResult_Data_Scraper_Movie
    '        Dim ScrapedList As New List(Of MediaContainers.MainDetails)

    '        While Not AllAddonsLoaded
    '            Application.DoEvents()
    '        End While

    '        If Not Addons.Count > 0 Then
    '            _Logger.Warn("[AddonsManager] [ScrapeData_Movie] [Abort] No scrapers enabled")
    '        Else
    '            'clean DBMovie if the movie is to be changed. For this, all existing (incorrect) information must be deleted and the images triggers set to remove.
    '            If (scrapeType = Enums.ScrapeType.SingleScrape OrElse scrapeType = Enums.ScrapeType.SingleAuto) AndAlso scrapeModifiers.DoSearch Then
    '                dbElement.ImagesContainer = New MediaContainers.ImagesContainer
    '                dbElement.MainDetails = New MediaContainers.MainDetails With {
    '                    .Edition = dbElement.Edition,
    '                    .Title = StringUtils.FilterTitleFromPath_Movie(dbElement.Filename, dbElement.IsSingle, dbElement.Source.UseFolderName),
    '                    .VideoSource = dbElement.VideoSource,
    '                    .Year = StringUtils.FilterYearFromPath_Movie(dbElement.Filename, dbElement.IsSingle, dbElement.Source.UseFolderName)
    '                }
    '            End If

    '            'create a clone of DBMovie
    '            Dim clonedDBElement As Database.DBElement = CType(dbElement.CloneDeep, Database.DBElement)

    '            For Each _externalScraperModule As Data_Scraper_Movie In Addons
    '                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Using] {0}", _externalScraperModule.AddonInterface.Name))
    '                Result = _externalScraperModule.AddonInterface.Scraper_Movie(clonedDBElement, scrapeModifiers, scrapeType, scrapeOptions)

    '                Select Case Result.Status
    '                    Case Interfaces.ResultStatus.BreakChain
    '                        Exit For
    '                    Case Interfaces.ResultStatus.Cancelled
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Cancelled] {0}", _externalScraperModule.AddonInterface.Name))
    '                        Return False
    '                    Case Interfaces.ResultStatus.NoResult
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [NoResult] {0}", _externalScraperModule.AddonInterface.Name))
    '                        'do Next
    '                    Case Interfaces.ResultStatus.Skipped
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Skipped] by User {0}", _externalScraperModule.AddonInterface.Name))
    '                        'do Next
    '                    Case Else
    '                        If Result.Result IsNot Nothing Then
    '                            'if a movie is found, set "DoSearch" back to "False" for following scrapers
    '                            scrapeModifiers.DoSearch = False
    '                            ScrapedList.Add(Result.Result)

    '                            'set new informations for following scrapers
    '                            If Result.Result.UniqueIDsSpecified Then
    '                                clonedDBElement.MainDetails.UniqueIDs.AddRange(Result.Result.UniqueIDs)
    '                            End If
    '                            If Result.Result.OriginalTitleSpecified Then
    '                                clonedDBElement.MainDetails.OriginalTitle = Result.Result.OriginalTitle
    '                            End If
    '                            If Result.Result.TitleSpecified Then
    '                                clonedDBElement.MainDetails.Title = Result.Result.Title
    '                            End If
    '                            If Result.Result.YearSpecified Then
    '                                clonedDBElement.MainDetails.Year = Result.Result.Year
    '                            End If
    '                        End If
    '                End Select
    '            Next

    '            'workaround to get trailer links from trailer scrapers
    '            If scrapeOptions.Trailer AndAlso Master.eSettings.MovieScraperTrailerFromTrailerScrapers Then
    '                Dim nTrailerList As New List(Of MediaContainers.MediaFile)
    '                Instance.ScrapeTrailer_Movie(clonedDBElement, Enums.ModifierType.MainTrailer, nTrailerList)
    '                If nTrailerList IsNot Nothing Then
    '                    Dim newPreferredTrailer As New MediaContainers.MediaFile
    '                    If MediaFiles.GetPreferredMovieTrailer(nTrailerList, newPreferredTrailer) Then
    '                        If newPreferredTrailer IsNot Nothing AndAlso newPreferredTrailer.UrlForNfoSpecified Then
    '                            ScrapedList.Add(New MediaContainers.MainDetails With {
    '                                            .Scrapersource = newPreferredTrailer.Scraper,
    '                                            .Trailer = newPreferredTrailer.UrlForNfo
    '                                            })
    '                        End If
    '                    End If
    '                End If
    '            End If

    '            'Merge scraperresults considering global datascraper settings
    '            dbElement = NFO.MergeDataScraperResults_Movie(dbElement, ScrapedList, scrapeType, scrapeOptions)

    '            'create cache paths for Actor Thumbs
    '            dbElement.MainDetails.CreateCachePaths_ActorsThumbs()
    '        End If

    '        If ScrapedList.Count > 0 Then
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Done] {0}", dbElement.Filename))
    '            Return True
    '        Else
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Done] [No Scraper Results] {0}", dbElement.Filename))
    '            Return False
    '        End If
    '    Else
    '        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Abort] [Offline] {0}", dbElement.Filename))
    '        Return False
    '    End If
    'End Function

    'Public Function ScrapeData_Movieset(ByRef dbElement As Database.DBElement,
    '                                    ByRef scrapeModifiers As Structures.ScrapeModifiers,
    '                                    ByVal scrapeType As Enums.ScrapeType,
    '                                    ByVal scrapeOptions As Structures.ScrapeOptions,
    '                                    ByVal showMessage As Boolean
    '                                    ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_MovieSet] [Start] {0}", dbElement.MainDetails.Title))
    '    'If DBMovieSet.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_MovieSet(DBMovieSet, showMessage) Then
    '    Dim Addons As IEnumerable(Of Data_Scraper_Movieset) = Data_Scrapers_Movieset.Where(Function(e) e.AddonInterface.ScraperEnabled).OrderBy(Function(e) e.Order)
    '    Dim Result As Interfaces.AddonResult_Data_Scraper_Movieset
    '    Dim ScrapedList As New List(Of MediaContainers.MainDetails)

    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    If Not Addons.Count > 0 Then
    '        _Logger.Warn("[AddonsManager] [ScrapeData_MovieSet] [Abort] No scrapers enabled")
    '    Else
    '        'clean DBMovie if the movie is to be changed. For this, all existing (incorrect) information must be deleted and the images triggers set to remove.
    '        If (scrapeType = Enums.ScrapeType.SingleScrape OrElse scrapeType = Enums.ScrapeType.SingleAuto) AndAlso scrapeModifiers.DoSearch Then
    '            Dim tmpTitle As String = dbElement.MainDetails.Title

    '            dbElement.ImagesContainer = New MediaContainers.ImagesContainer
    '            dbElement.MainDetails = New MediaContainers.MainDetails With {
    '                .Title = tmpTitle
    '            }
    '        End If

    '        'create a clone of DBMovieset
    '        Dim clonedDBElement As Database.DBElement = CType(dbElement.CloneDeep, Database.DBElement)

    '        For Each _externalScraperModule As Data_Scraper_Movieset In Addons
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_MovieSet] [Using] {0}", _externalScraperModule.AddonInterface.Name))
    '            Result = _externalScraperModule.AddonInterface.Scraper(clonedDBElement, scrapeModifiers, scrapeType, scrapeOptions)

    '            Select Case Result.Status
    '                Case Interfaces.ResultStatus.BreakChain
    '                    Exit For
    '                Case Interfaces.ResultStatus.Cancelled
    '                    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movieset] [Cancelled] {0}", _externalScraperModule.AddonInterface.Name))
    '                    Return False
    '                Case Interfaces.ResultStatus.NoResult
    '                    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movieset] [NoResult] {0}", _externalScraperModule.AddonInterface.Name))
    '                    'do Next
    '                Case Interfaces.ResultStatus.Skipped
    '                    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movieset] [Skipped] by User {0}", _externalScraperModule.AddonInterface.Name))
    '                    'do Next
    '                Case Else
    '                    If Result.Result IsNot Nothing Then
    '                        'if a movieset is found, set "DoSearch" back to "False" for following scrapers
    '                        scrapeModifiers.DoSearch = False
    '                        ScrapedList.Add(Result.Result)

    '                        'set new informations for following scrapers
    '                        If Result.Result.UniqueIDsSpecified Then
    '                            clonedDBElement.MainDetails.UniqueIDs.AddRange(Result.Result.UniqueIDs)
    '                        End If
    '                        If Result.Result.TitleSpecified Then
    '                            clonedDBElement.MainDetails.Title = Result.Result.Title
    '                        End If
    '                    End If
    '            End Select
    '        Next

    '        'Merge scraperresults considering global datascraper settings
    '        dbElement = NFO.MergeDataScraperResults_MovieSet(dbElement, ScrapedList, scrapeType, scrapeOptions)
    '    End If

    '    If ScrapedList.Count > 0 Then
    '        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movieset] [Done] {0}", dbElement.MainDetails.Title))
    '        Return True
    '    Else
    '        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movieset] [Done] [No Scraper Results] {0}", dbElement.MainDetails.Title))
    '        Return False
    '    End If
    '    'Else
    '    'logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movieset] [Abort] [Offline] {0}", dbElement.MovieSet.Title))
    '    'Return False
    '    'End If
    'End Function

    'Public Function ScrapeData_TVEpisode(ByRef dbElement As Database.DBElement,
    '                                     ByVal scrapeOptions As Structures.ScrapeOptions,
    '                                     ByVal showMessage As Boolean
    '                                     ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Start] {0}", dbElement.Filename))
    '    If dbElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(dbElement, showMessage) Then
    '        Dim Addons As IEnumerable(Of Data_Scraper_TV) = Data_Scrapers_TV.Where(Function(e) e.AddonInterface.ScraperEnabled).OrderBy(Function(e) e.Order)
    '        Dim Result As Interfaces.AddonResult_Data_Scraper_TVEpisode
    '        Dim ScrapedList As New List(Of MediaContainers.MainDetails)

    '        While Not AllAddonsLoaded
    '            Application.DoEvents()
    '        End While

    '        If Not Addons.Count > 0 Then
    '            _Logger.Warn("[AddonsManager] [ScrapeData_TVEpisode] [Abort] No scrapers enabled")
    '        Else
    '            'create a clone of DBTV
    '            Dim clonedDBElement As Database.DBElement = CType(dbElement.CloneDeep, Database.DBElement)

    '            For Each _externalScraperModule As Data_Scraper_TV In Addons
    '                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Using] {0}", _externalScraperModule.AddonInterface.Name))
    '                Result = _externalScraperModule.AddonInterface.Scraper_TVEpisode(clonedDBElement, scrapeOptions)

    '                Select Case Result.Status
    '                    Case Interfaces.ResultStatus.BreakChain
    '                        Exit For
    '                    Case Interfaces.ResultStatus.Cancelled
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Cancelled] {0}", _externalScraperModule.AddonInterface.Name))
    '                        Return False
    '                    Case Interfaces.ResultStatus.NoResult
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [NoResult] {0}", _externalScraperModule.AddonInterface.Name))
    '                        'do Next
    '                    Case Interfaces.ResultStatus.Skipped
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Skipped] by User {0}", _externalScraperModule.AddonInterface.Name))
    '                        'do Next
    '                    Case Else
    '                        If Result.Result IsNot Nothing Then
    '                            ScrapedList.Add(Result.Result)

    '                            'set new informations for following scrapers
    '                            If Result.Result.UniqueIDsSpecified Then
    '                                clonedDBElement.MainDetails.UniqueIDs.AddRange(Result.Result.UniqueIDs)
    '                            End If
    '                            If Result.Result.AiredSpecified Then
    '                                clonedDBElement.MainDetails.Aired = Result.Result.Aired
    '                            End If
    '                            If Result.Result.EpisodeSpecified Then
    '                                clonedDBElement.MainDetails.Episode = Result.Result.Episode
    '                            End If
    '                            If Result.Result.SeasonSpecified Then
    '                                clonedDBElement.MainDetails.Season = Result.Result.Season
    '                            End If
    '                            If Result.Result.TitleSpecified Then
    '                                clonedDBElement.MainDetails.Title = Result.Result.Title
    '                            End If
    '                        End If
    '                End Select
    '            Next

    '            'Merge scraperresults considering global datascraper settings
    '            dbElement = NFO.MergeDataScraperResults_TVEpisode_Single(dbElement, ScrapedList, scrapeOptions)

    '            'create cache paths for Actor Thumbs
    '            dbElement.MainDetails.CreateCachePaths_ActorsThumbs()
    '        End If

    '        If ScrapedList.Count > 0 Then
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Done] {0}", dbElement.Filename))
    '            Return True
    '        Else
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Done] [No Scraper Results] {0}", dbElement.Filename))
    '            Return False
    '        End If
    '    Else
    '        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Abort] [Offline] {0}", dbElement.Filename))
    '        Return False
    '    End If
    'End Function

    'Public Function ScrapeData_TVSeason(ByRef dbElement As Database.DBElement,
    '                                    ByVal scrapeOptions As Structures.ScrapeOptions,
    '                                    ByVal showMessage As Boolean
    '                                    ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Start] {0}: Season {1}", dbElement.TVShowDetails.Title, dbElement.MainDetails.Season))
    '    If dbElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(dbElement, showMessage) Then
    '        Dim Addons As IEnumerable(Of Data_Scraper_TV) = Data_Scrapers_TV.Where(Function(e) e.AddonInterface.ScraperEnabled).OrderBy(Function(e) e.Order)
    '        Dim Result As Interfaces.AddonResult_Data_Scraper_TVSeason
    '        Dim ScrapedList As New List(Of MediaContainers.MainDetails)

    '        While Not AllAddonsLoaded
    '            Application.DoEvents()
    '        End While

    '        If Not Addons.Count > 0 Then
    '            _Logger.Warn("[AddonsManager] [ScrapeData_TVSeason] [Abort] No scrapers enabled")
    '        Else
    '            'create a clone of DBTV
    '            Dim cloneDBElement As Database.DBElement = CType(dbElement.CloneDeep, Database.DBElement)
    '            For Each _externalScraperModule As Data_Scraper_TV In Addons
    '                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Using] {0}", _externalScraperModule.AddonInterface.Name))
    '                Result = _externalScraperModule.AddonInterface.Scraper_TVSeason(cloneDBElement, scrapeOptions)

    '                Select Case Result.Status
    '                    Case Interfaces.ResultStatus.BreakChain
    '                        Exit For
    '                    Case Interfaces.ResultStatus.Cancelled
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Cancelled] {0}", _externalScraperModule.AddonInterface.Name))
    '                        Return False
    '                    Case Interfaces.ResultStatus.NoResult
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [NoResult] {0}", _externalScraperModule.AddonInterface.Name))
    '                        'do Next
    '                    Case Interfaces.ResultStatus.Skipped
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Skipped] by User {0}", _externalScraperModule.AddonInterface.Name))
    '                        'do Next
    '                    Case Else
    '                        If Result.Result IsNot Nothing Then
    '                            ScrapedList.Add(Result.Result)

    '                            'set new informations for following scrapers
    '                            If Result.Result.UniqueIDsSpecified Then
    '                                cloneDBElement.MainDetails.UniqueIDs.AddRange(Result.Result.UniqueIDs)
    '                            End If
    '                        End If
    '                End Select
    '            Next

    '            'Merge scraperresults considering global datascraper settings
    '            dbElement = NFO.MergeDataScraperResults_TVSeason(dbElement, ScrapedList, scrapeOptions)
    '        End If

    '        If ScrapedList.Count > 0 Then
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Done] {0}: Season {1}", dbElement.TVShowDetails.Title, dbElement.MainDetails.Season))
    '            Return True
    '        Else
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Done] [No Scraper Results] {0}: Season {1}", dbElement.TVShowDetails.Title, dbElement.MainDetails.Season))
    '            Return False
    '        End If
    '    Else
    '        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Abort] [Offline] {0}: Season {1}", dbElement.TVShowDetails.Title, dbElement.MainDetails.Season))
    '        Return False
    '    End If
    'End Function

    'Public Function ScrapeData_TVShow(ByRef dbElement As Database.DBElement,
    '                                  ByRef scrapeModifiers As Structures.ScrapeModifiers,
    '                                  ByVal scrapeType As Enums.ScrapeType,
    '                                  ByVal scrapeOptions As Structures.ScrapeOptions,
    '                                  ByVal showMessage As Boolean
    '                                  ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Start] {0}", dbElement.MainDetails.Title))
    '    If dbElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(dbElement, showMessage) Then
    '        Dim Addons As IEnumerable(Of Data_Scraper_TV) = Data_Scrapers_TV.Where(Function(e) e.AddonInterface.ScraperEnabled).OrderBy(Function(e) e.Order)
    '        Dim Result As Interfaces.AddonResult_Data_Scraper_TVShow
    '        Dim ScrapedList As New List(Of MediaContainers.MainDetails)

    '        While Not AllAddonsLoaded
    '            Application.DoEvents()
    '        End While

    '        If Not Addons.Count > 0 Then
    '            _Logger.Warn("[AddonsManager] [ScrapeData_TVShow] [Abort] No scrapers enabled")
    '        Else
    '            'clean DBTV if the tv show is to be changed. For this, all existing (incorrect) information must be deleted and the images triggers set to remove.
    '            If (scrapeType = Enums.ScrapeType.SingleScrape OrElse scrapeType = Enums.ScrapeType.SingleAuto) AndAlso scrapeModifiers.DoSearch Then
    '                dbElement.ExtrafanartsPath = String.Empty
    '                dbElement.ImagesContainer = New MediaContainers.ImagesContainer
    '                dbElement.NfoPath = String.Empty
    '                dbElement.Seasons.Clear()
    '                dbElement.Theme = New MediaContainers.MediaFile
    '                dbElement.MainDetails = New MediaContainers.MainDetails With {
    '                    .Title = StringUtils.FilterTitleFromPath_TVShow(dbElement.ShowPath)
    '                }

    '                For Each sEpisode As Database.DBElement In dbElement.Episodes
    '                    Dim iEpisode As Integer = sEpisode.MainDetails.Episode
    '                    Dim iSeason As Integer = sEpisode.MainDetails.Season
    '                    Dim strAired As String = sEpisode.MainDetails.Aired
    '                    sEpisode.ImagesContainer = New MediaContainers.ImagesContainer
    '                    sEpisode.NfoPath = String.Empty
    '                    sEpisode.MainDetails = New MediaContainers.MainDetails With {.Aired = strAired, .Episode = iEpisode, .Season = iSeason}
    '                    sEpisode.MainDetails.VideoSource = sEpisode.VideoSource
    '                Next
    '            End If

    '            'create a clone of DBTV
    '            Dim clonedDBElement As Database.DBElement = CType(dbElement.CloneDeep, Database.DBElement)
    '            For Each _externalScraperModule As Data_Scraper_TV In Addons
    '                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Using] {0}", _externalScraperModule.AddonInterface.Name))
    '                Result = _externalScraperModule.AddonInterface.Scraper_TVShow(clonedDBElement, scrapeModifiers, scrapeType, scrapeOptions)

    '                Select Case Result.Status
    '                    Case Interfaces.ResultStatus.BreakChain
    '                        Exit For
    '                    Case Interfaces.ResultStatus.Cancelled
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Cancelled] {0}", _externalScraperModule.AddonInterface.Name))
    '                        Return False
    '                    Case Interfaces.ResultStatus.NoResult
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [NoResult] {0}", _externalScraperModule.AddonInterface.Name))
    '                    'do Next
    '                    Case Interfaces.ResultStatus.Skipped
    '                        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Skipped] by User {0}", _externalScraperModule.AddonInterface.Name))
    '                        'do Next
    '                    Case Else
    '                        If Result.Result IsNot Nothing Then
    '                            'if a tvshow is found, set "DoSearch" back to "False" for following scrapers
    '                            scrapeModifiers.DoSearch = False
    '                            ScrapedList.Add(Result.Result)

    '                            'set new informations for following scrapers
    '                            If Result.Result.UniqueIDsSpecified Then
    '                                clonedDBElement.MainDetails.UniqueIDs.AddRange(Result.Result.UniqueIDs)
    '                            End If
    '                            If Result.Result.OriginalTitleSpecified Then
    '                                clonedDBElement.MainDetails.OriginalTitle = Result.Result.OriginalTitle
    '                            End If
    '                            If Result.Result.TitleSpecified Then
    '                                clonedDBElement.MainDetails.Title = Result.Result.Title
    '                            End If
    '                        End If
    '                End Select
    '            Next

    '            'Merge scraperresults considering global datascraper settings
    '            dbElement = NFO.MergeDataScraperResults_TV(dbElement, ScrapedList, scrapeType, scrapeOptions, scrapeModifiers.withEpisodes)

    '            'create cache paths for Actor Thumbs
    '            dbElement.MainDetails.CreateCachePaths_ActorsThumbs()
    '            If scrapeModifiers.withEpisodes Then
    '                For Each tEpisode As Database.DBElement In dbElement.Episodes
    '                    tEpisode.MainDetails.CreateCachePaths_ActorsThumbs()
    '                Next
    '            End If
    '        End If

    '        If ScrapedList.Count > 0 Then
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Done] {0}", dbElement.MainDetails.Title))
    '            Return True
    '        Else
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Done] [No Scraper Results] {0}", dbElement.MainDetails.Title))
    '            Return False
    '        End If
    '    Else
    '        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Abort] [Offline] {0}", dbElement.MainDetails.Title))
    '        Return False
    '    End If
    'End Function
    '''' <summary>
    '''' Request that enabled movie image scrapers perform their functions on the supplied movie
    '''' </summary>
    '''' <param name="dbElement">Movie to be scraped. Scraper will directly manipulate this structure</param>
    '''' <param name="imagesContainer">Container of images that the scraper should add to</param>
    '''' <returns><c>True</c> if one of the scrapers was successful</returns>
    '''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    'Public Function ScrapeImage_Movie(ByRef dbElement As Database.DBElement,
    '                                  ByRef imagesContainer As MediaContainers.SearchResultsContainer,
    '                                  ByVal scrapeModifiers As Structures.ScrapeModifiers,
    '                                  ByVal showMessage As Boolean
    '                                  ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_Movie] [Start] {0}", dbElement.Filename))
    '    If dbElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(dbElement, showMessage) Then
    '        Dim Addons As IEnumerable(Of Image_Scraper_Movie) = Image_Scrapers_Movie.Where(Function(e) e.AddonInterface.ScraperEnabled).OrderBy(Function(e) e.Order)
    '        Dim Result As Interfaces.AddonResult_Generic

    '        While Not AllAddonsLoaded
    '            Application.DoEvents()
    '        End While

    '        If Not Addons.Count > 0 Then
    '            _Logger.Warn("[AddonsManager] [ScrapeImage_Movie] [Abort] No scrapers enabled")
    '        Else
    '            For Each _externalScraperModule As Image_Scraper_Movie In Addons
    '                _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_Movie] [Using] {0}", _externalScraperModule.AddonInterface.ModuleName))
    '                If QueryScraperCapabilities_Image_Movie(_externalScraperModule, scrapeModifiers) Then
    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Result = _externalScraperModule.AddonInterface.Scraper(dbElement, aContainer, scrapeModifiers)
    '                    If aContainer IsNot Nothing Then
    '                        imagesContainer.MainBanners.AddRange(aContainer.MainBanners)
    '                        imagesContainer.MainCharacterArts.AddRange(aContainer.MainCharacterArts)
    '                        imagesContainer.MainClearArts.AddRange(aContainer.MainClearArts)
    '                        imagesContainer.MainClearLogos.AddRange(aContainer.MainClearLogos)
    '                        imagesContainer.MainDiscArts.AddRange(aContainer.MainDiscArts)
    '                        imagesContainer.MainFanarts.AddRange(aContainer.MainFanarts)
    '                        imagesContainer.MainLandscapes.AddRange(aContainer.MainLandscapes)
    '                        imagesContainer.MainPosters.AddRange(aContainer.MainPosters)
    '                    End If
    '                End If
    '            Next

    '            'sorting
    '            imagesContainer.SortAndFilter(dbElement)

    '            'create cache paths
    '            imagesContainer.CreateCachePaths(dbElement)
    '        End If

    '        _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_Movie] [Done] {0}", dbElement.Filename))
    '        Return True
    '    Else
    '        _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_Movie] [Abort] [Offline] {0}", dbElement.Filename))
    '        Return False
    '    End If
    'End Function
    '''' <summary>
    '''' Request that enabled movieset image scrapers perform their functions on the supplied movie
    '''' </summary>
    '''' <param name="dbElement">Movieset to be scraped. Scraper will directly manipulate this structure</param>
    '''' <param name="imagesContainer">Container of images that the scraper should add to</param>
    '''' <returns><c>True</c> if one of the scrapers was successful</returns>
    '''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    'Public Function ScrapeImage_Movieset(ByRef dbElement As Database.DBElement,
    '                                     ByRef imagesContainer As MediaContainers.SearchResultsContainer,
    '                                     ByVal scrapeModifiers As Structures.ScrapeModifiers
    '                                     ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_MovieSet] [Start] {0}", dbElement.MainDetails.Title))
    '    Dim Addons As IEnumerable(Of Image_Scraper_Movieset) = Image_Scrapers_Movieset.Where(Function(e) e.AddonInterface.ScraperEnabled).OrderBy(Function(e) e.Order)
    '    Dim Result As Interfaces.AddonResult_Generic

    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    If Not Addons.Count > 0 Then
    '        _Logger.Warn("[AddonsManager] [ScrapeImage_MovieSet] [Abort] No scrapers enabled")
    '    Else
    '        For Each _externalScraperModule As Image_Scraper_Movieset In Addons
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_MovieSet] [Using] {0}", _externalScraperModule.AddonInterface.ModuleName))
    '            If QueryScraperCapabilities_Image_MovieSet(_externalScraperModule, scrapeModifiers) Then
    '                Dim aContainer As New MediaContainers.SearchResultsContainer
    '                Result = _externalScraperModule.AddonInterface.Scraper(dbElement, aContainer, scrapeModifiers)
    '                If aContainer IsNot Nothing Then
    '                    imagesContainer.MainBanners.AddRange(aContainer.MainBanners)
    '                    imagesContainer.MainCharacterArts.AddRange(aContainer.MainCharacterArts)
    '                    imagesContainer.MainClearArts.AddRange(aContainer.MainClearArts)
    '                    imagesContainer.MainClearLogos.AddRange(aContainer.MainClearLogos)
    '                    imagesContainer.MainDiscArts.AddRange(aContainer.MainDiscArts)
    '                    imagesContainer.MainFanarts.AddRange(aContainer.MainFanarts)
    '                    imagesContainer.MainKeyarts.AddRange(aContainer.MainKeyarts)
    '                    imagesContainer.MainLandscapes.AddRange(aContainer.MainLandscapes)
    '                    imagesContainer.MainPosters.AddRange(aContainer.MainPosters)
    '                End If
    '            End If
    '        Next

    '        'sorting
    '        imagesContainer.SortAndFilter(dbElement)

    '        'create cache paths
    '        imagesContainer.CreateCachePaths(dbElement)
    '    End If

    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_MovieSet] [Done] {0}", dbElement.MainDetails.Title))
    '    Return True
    'End Function
    ''' <summary>
    ''' Request that enabled tv image scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="dbElement">TV Show to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="scrapeModifiers">What kind of image is being scraped (poster, fanart, etc)</param>
    ''' <param name="imagesContainer">Container of images that the scraper should add to</param>
    ''' <returns><c>True</c> if one of the scrapers was successful</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    'Public Function ScrapeImage_TV(ByRef dbElement As Database.DBElement,
    '                               ByRef imagesContainer As MediaContainers.SearchResultsContainer,
    '                               ByVal scrapeModifiers As Structures.ScrapeModifiers,
    '                               ByVal showMessage As Boolean
    '                               ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_TV] [Start] {0}", dbElement.MainDetails.Title))
    '    If dbElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(dbElement, showMessage) Then
    '        Dim Addons As IEnumerable(Of Image_Scraper_TV) = Image_Scrapers_TV.Where(Function(e) e.AddonInterface.ScraperEnabled).OrderBy(Function(e) e.Order)
    '        Dim Result As Interfaces.AddonResult_Generic

    '        While Not AllAddonsLoaded
    '            Application.DoEvents()
    '        End While

    '        'workaround to get MainFanarts for AllSeasonsFanarts, EpisodeFanarts and SeasonFanarts,
    '        'also get MainBanners, MainLandscapes and MainPosters for AllSeasonsBanners, AllSeasonsLandscapes and AllSeasonsPosters
    '        If scrapeModifiers.AllSeasonsBanner Then
    '            scrapeModifiers.MainBanner = True
    '            scrapeModifiers.SeasonBanner = True
    '        End If
    '        If scrapeModifiers.AllSeasonsFanart Then
    '            scrapeModifiers.MainFanart = True
    '            scrapeModifiers.SeasonFanart = True
    '        End If
    '        If scrapeModifiers.AllSeasonsLandscape Then
    '            scrapeModifiers.MainLandscape = True
    '            scrapeModifiers.SeasonLandscape = True
    '        End If
    '        If scrapeModifiers.AllSeasonsPoster Then
    '            scrapeModifiers.MainPoster = True
    '            scrapeModifiers.SeasonPoster = True
    '        End If
    '        If scrapeModifiers.EpisodeFanart Then
    '            scrapeModifiers.MainFanart = True
    '        End If
    '        If scrapeModifiers.MainExtrafanarts Then
    '            scrapeModifiers.MainFanart = True
    '        End If
    '        If scrapeModifiers.MainExtrathumbs Then
    '            scrapeModifiers.MainFanart = True
    '        End If
    '        If scrapeModifiers.SeasonFanart Then
    '            scrapeModifiers.MainFanart = True
    '        End If

    '        If Not Addons.Count > 0 Then
    '            _Logger.Warn("[AddonsManager] [ScrapeImage_TV] [Abort] No scrapers enabled")
    '        Else
    '            For Each _externalScraperModule As Image_Scraper_TV In Addons
    '                _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_TV] [Using] {0}", _externalScraperModule.AddonInterface.ModuleName))
    '                If QueryScraperCapabilities_Image_TV(_externalScraperModule, scrapeModifiers) Then
    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Result = _externalScraperModule.AddonInterface.Scraper(dbElement, aContainer, scrapeModifiers)
    '                    If aContainer IsNot Nothing Then
    '                        imagesContainer.EpisodeFanarts.AddRange(aContainer.EpisodeFanarts)
    '                        imagesContainer.EpisodePosters.AddRange(aContainer.EpisodePosters)
    '                        imagesContainer.SeasonBanners.AddRange(aContainer.SeasonBanners)
    '                        imagesContainer.SeasonFanarts.AddRange(aContainer.SeasonFanarts)
    '                        imagesContainer.SeasonLandscapes.AddRange(aContainer.SeasonLandscapes)
    '                        imagesContainer.SeasonPosters.AddRange(aContainer.SeasonPosters)
    '                        imagesContainer.MainBanners.AddRange(aContainer.MainBanners)
    '                        imagesContainer.MainCharacterArts.AddRange(aContainer.MainCharacterArts)
    '                        imagesContainer.MainClearArts.AddRange(aContainer.MainClearArts)
    '                        imagesContainer.MainClearLogos.AddRange(aContainer.MainClearLogos)
    '                        imagesContainer.MainFanarts.AddRange(aContainer.MainFanarts)
    '                        imagesContainer.MainKeyarts.AddRange(aContainer.MainKeyarts)
    '                        imagesContainer.MainLandscapes.AddRange(aContainer.MainLandscapes)
    '                        imagesContainer.MainPosters.AddRange(aContainer.MainPosters)
    '                    End If
    '                End If
    '            Next

    '            'sorting
    '            imagesContainer.SortAndFilter(dbElement)

    '            'create cache paths
    '            imagesContainer.CreateCachePaths(dbElement)
    '        End If

    '        _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_TV] [Done] {0}", dbElement.MainDetails.Title))
    '        Return True
    '    Else
    '        _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_TV] [Abort] [Offline] {0}", dbElement.Filename))
    '        Return False
    '    End If
    'End Function
    ''' <summary>
    ''' Request that enabled movie theme scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="dbElement">Movie to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="type">NOT ACTUALLY USED!</param>
    ''' <param name="themeList">List of Theme objects that the scraper will append to.</param>
    ''' <returns><c>True</c> if one of the scrapers was successful</returns>
    ''' <remarks></remarks>
    'Public Function ScrapeTheme_Movie(ByRef dbElement As Database.DBElement,
    '                                  ByVal type As Enums.ModifierType,
    '                                  ByRef themeList As List(Of MediaContainers.MediaFile)
    '                                  ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_Movie] [Start] {0}", dbElement.Filename))
    '    Dim Addons As IEnumerable(Of Theme_Scraper_Movie) = Theme_Scrapers_Movie.Where(Function(e) e.AddonInterface.ScraperEnabled).OrderBy(Function(e) e.Order)
    '    Dim Result As Interfaces.AddonResult_Generic

    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    If Not Addons.Count > 0 Then
    '        _Logger.Warn("[AddonsManager] [ScrapeTheme_Movie] [Abort] No scrapers enabled")
    '    Else
    '        For Each _externalScraperModule As Theme_Scraper_Movie In Addons
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_Movie] [Using] {0}", _externalScraperModule.AddonInterface.ModuleName))
    '            Dim aList As New List(Of MediaContainers.MediaFile)
    '            Result = _externalScraperModule.AddonInterface.Scraper(dbElement, type, aList)
    '            If aList IsNot Nothing Then
    '                For Each tItem In aList
    '                    tItem.Streams.BuildStreamVariants(True)
    '                Next
    '                themeList.AddRange(aList)
    '            End If
    '        Next
    '    End If
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_Movie] [Done] {0}", dbElement.Filename))
    '    Return True
    'End Function
    '''' <summary>
    '''' Request that enabled tvshow theme scrapers perform their functions on the supplied tv show
    '''' </summary>
    '''' <param name="dbElement">TV Show to be scraped. Scraper will directly manipulate this structure</param>
    '''' <param name="type">NOT ACTUALLY USED!</param>
    '''' <param name="themeList">List of Theme objects that the scraper will append to.</param>
    '''' <returns><c>True</c> if one of the scrapers was successful</returns>
    '''' <remarks></remarks>
    'Public Function ScrapeTheme_TVShow(ByRef dbElement As Database.DBElement,
    '                                   ByVal type As Enums.ModifierType,
    '                                   ByRef themeList As List(Of MediaContainers.MediaFile)
    '                                   ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_TVShow] [Start] {0}", dbElement.MainDetails.Title))
    '    Dim Addons As IEnumerable(Of Theme_Scraper_TV) = Theme_Scrapers_TV.Where(Function(e) e.AddonInterface.ScraperEnabled).OrderBy(Function(e) e.Order)
    '    Dim Result As Interfaces.AddonResult_Generic

    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    If Not Addons.Count > 0 Then
    '        _Logger.Warn("[AddonsManager] [ScrapeTheme_TVShow] [Abort] No scrapers enabled")
    '    Else
    '        For Each _externalScraperModule As Theme_Scraper_TV In Addons
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_TVShow] [Using] {0}", _externalScraperModule.AddonInterface.ModuleName))
    '            Dim aList As New List(Of MediaContainers.MediaFile)
    '            Result = _externalScraperModule.AddonInterface.Scraper(dbElement, type, aList)
    '            If aList IsNot Nothing Then
    '                For Each tItem In aList
    '                    tItem.Streams.BuildStreamVariants(True)
    '                Next
    '                themeList.AddRange(aList)
    '            End If
    '        Next
    '    End If
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_TVShow] [Done] {0}", dbElement.MainDetails.Title))
    '    Return True
    'End Function
    ''' <summary>
    ''' Request that enabled movie trailer scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="dbElement">Movie to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="type">NOT ACTUALLY USED!</param>
    ''' <param name="trailerList">List of Trailer objects that the scraper will append to. Note that only the URL is returned, 
    ''' not the full content of the trailer</param>
    ''' <returns><c>True</c> if one of the scrapers was successful</returns>
    ''' <remarks></remarks>
    'Public Function ScrapeTrailer_Movie(ByRef dbElement As Database.DBElement,
    '                                    ByVal type As Enums.ModifierType,
    '                                    ByRef trailerList As List(Of MediaContainers.MediaFile)
    '                                    ) As Boolean
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeTrailer_Movie] [Start] {0}", dbElement.Filename))
    '    Dim Addons As IEnumerable(Of Trailer_Scraper_Movie) = Trailer_Scrapers_Movie.Where(Function(e) e.AddonInterface.ScraperEnabled).OrderBy(Function(e) e.Order)
    '    Dim Result As Interfaces.AddonResult_Generic

    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    If Not Addons.Count > 0 Then
    '        _Logger.Warn("[AddonsManager] [ScrapeTrailer_Movie] [Abort] No scrapers enabled")
    '    Else
    '        For Each _externalScraperModule As Trailer_Scraper_Movie In Addons
    '            _Logger.Trace(String.Format("[AddonsManager] [ScrapeTrailer_Movie] [Using] {0}", _externalScraperModule.AddonInterface.ModuleName))
    '            Dim aList As New List(Of MediaContainers.MediaFile)
    '            Result = _externalScraperModule.AddonInterface.Scraper(dbElement, type, aList)
    '            If aList IsNot Nothing Then
    '                For Each tItem In aList
    '                    tItem.Streams.BuildStreamVariants()
    '                Next
    '                trailerList.AddRange(aList)
    '            End If
    '            If Result.Status = Interfaces.ResultStatus.BreakChain Then Exit For
    '        Next
    '    End If
    '    _Logger.Trace(String.Format("[AddonsManager] [ScrapeTrailer_Movie] [Done] {0}", dbElement.Filename))
    '    Return True
    'End Function

    Function ScraperWithCapabilityAnyEnabled_Image_Movie(ByVal imageType As Enums.ModifierType) As Boolean
        Dim Result As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each Addon In Addons.Where(Function(e) e.AddonInterface.IsEnabled_Image_Movie).OrderBy(Function(e) e.Order)
            Result = QueryScraperCapabilities_Image_Movie(Addon, imageType)
            If Result Then Exit For
        Next
        Return Result
    End Function

    Function ScraperWithCapabilityAnyEnabled_Image_Movieset(ByVal imageType As Enums.ModifierType) As Boolean
        Dim Result As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each Addon In Addons.Where(Function(e) e.AddonInterface.IsEnabled_Image_Movieset).OrderBy(Function(e) e.Order)
            Result = QueryScraperCapabilities_Image_MovieSet(Addon, imageType)
            If Result Then Exit For
        Next
        Return Result
    End Function

    Function ScraperWithCapabilityAnyEnabled_Image_TV(ByVal imageType As Enums.ModifierType) As Boolean
        Dim Result As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each Addon In Addons.Where(Function(e) e.AddonInterface.IsEnabled_Image_TV).OrderBy(Function(e) e.Order)
            Result = QueryScraperCapabilities_Image_TV(Addon, imageType)
            If Result Then Exit For
        Next
        Return Result
    End Function

    Function ScraperWithCapabilityAnyEnabled_Theme_Movie() As Boolean
        Dim Result As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each Addon In Addons.Where(Function(e) e.AddonInterface.IsEnabled_Theme_Movie).OrderBy(Function(e) e.Order)
            Result = True 'if a theme scraper is enabled we can exit.
            Exit For
        Next
        Return Result
    End Function

    Function ScraperWithCapabilityAnyEnabled_Theme_TV() As Boolean
        Dim Result As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each Addon In Addons.Where(Function(e) e.AddonInterface.IsEnabled_Theme_TV).OrderBy(Function(e) e.Order)
            Result = True 'if a theme scraper is enabled we can exit.
            Exit For
        Next
        Return Result
    End Function

    Function ScraperWithCapabilityAnyEnabled_Trailer_Movie() As Boolean
        Dim Result As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each Addon In Addons.Where(Function(e) e.AddonInterface.IsEnabled_Trailer_Movie).OrderBy(Function(e) e.Order)
            Result = True 'if a trailer scraper is enabled we can exit.
            Exit For
        Next
        Return Result
    End Function

    Public Sub Settings_Save()
        Dim tmpForXML As New List(Of AddonClass)

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        For Each i In Addons
            Dim t As New AddonClass With {
                .AssemblyFileName = i.AssemblyFileName,
                .Enabled = i.AddonInterface.IsEnabled_Generic,
                .Enabled_Data_Movie = i.AddonInterface.IsEnabled_Data_Movie,
                .Enabled_Data_Movieset = i.AddonInterface.IsEnabled_Data_Movieset,
                .Enabled_Data_TV = i.AddonInterface.IsEnabled_Data_TV,
                .Enabled_Image_Movie = i.AddonInterface.IsEnabled_Image_Movie,
                .Enabled_Image_Movieset = i.AddonInterface.IsEnabled_Image_Movieset,
                .Enabled_Image_TV = i.AddonInterface.IsEnabled_Image_TV,
                .Enabled_Theme_Movie = i.AddonInterface.IsEnabled_Theme_Movie,
                .Enabled_Theme_TV = i.AddonInterface.IsEnabled_Theme_TV,
                .Enabled_Trailer_Movie = i.AddonInterface.IsEnabled_Trailer_Movie,
                .Order = i.Order,
                .Order_Data_Movie = i.Order_Data_Movie,
                .Order_Data_Movieset = i.Order_Data_Movieset,
                .Order_Data_TV = i.Order_Data_TV,
                .Order_Image_Movie = i.Order_Image_Movie,
                .Order_Image_Movieset = i.Order_Image_Movieset,
                .Order_Image_TV = i.Order_Image_TV,
                .Order_Theme_Movie = i.Order_Theme_Movie,
                .Order_Theme_TV = i.Order_Theme_TV,
                .Order_Trailer_Movie = i.Order_Trailer_Movie
            }
            tmpForXML.Add(t)
        Next
        Master.eSettings.Addons = tmpForXML
        Master.eSettings.Save()
    End Sub

    Private Sub GenericRunCallBack(ByVal eventType As Enums.AddonEventType, ByRef parameters As List(Of Object))
        RaiseEvent GenericEvent(eventType, parameters)
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure AssemblyListItem

#Region "Fields"

        Public Assembly As Reflection.Assembly
        Public AssemblyName As String
        Public AssemblyVersion As Version

#End Region 'Fields

    End Structure

    Structure VersionItem

#Region "Fields"

        Public AssemblyFileName As String
        Public NeedUpdate As Boolean
        Public Version As String

#End Region 'Fields

    End Structure

    Class EmberRuntimeObjects

#Region "Fields"

        Private _ListMovies As String
        Private _ListMoviesets As String
        Private _ListTVShows As String
        Private _LoadMedia As LoadMedia
        Private _OpenImageViewer As OpenImageViewer

#End Region 'Fields

#Region "Delegates"

        Delegate Sub LoadMedia(ByVal scan As Scanner.ScanOrCleanOptions, ByVal sourceId As Long)

        'all runtime object including Function (delegate) that need to be exposed to Addons
        Delegate Sub OpenImageViewer(ByVal _Image As Image)

#End Region 'Delegates

#Region "Properties"

        Public Property ListMovies() As String
            Get
                Return If(_ListMovies IsNot Nothing, _ListMovies, "movielist")
            End Get
            Set(ByVal value As String)
                _ListMovies = value
            End Set
        End Property

        Public Property ListMoviesets() As String
            Get
                Return If(_ListMoviesets IsNot Nothing, _ListMoviesets, "setslist")
            End Get
            Set(ByVal value As String)
                _ListMoviesets = value
            End Set
        End Property

        Public Property ListTVShows() As String
            Get
                Return If(_ListTVShows IsNot Nothing, _ListTVShows, "tvshowlist")
            End Get
            Set(ByVal value As String)
                _ListTVShows = value
            End Set
        End Property

        Public Property FilterMovies() As String = String.Empty

        Public Property FilterMoviesSearch() As String = String.Empty

        Public Property FilterMoviesType() As String = String.Empty

        Public Property FilterTVShows() As String = String.Empty

        Public Property FilterTVShowsSearch() As String = String.Empty

        Public Property FilterTVShowsType() As String = String.Empty

        Public Property MediaTabSelected() As Settings.MainTabSorting = New Settings.MainTabSorting

        Public Property MainToolStrip() As ToolStrip = New ToolStrip

        Public Property MediaListMovies() As DataGridView = New DataGridView

        Public Property MediaListMovieSets() As DataGridView = New DataGridView

        Public Property MediaListTVEpisodes() As DataGridView = New DataGridView

        Public Property MediaListTVSeasons() As DataGridView = New DataGridView

        Public Property MediaListTVShows() As DataGridView = New DataGridView

        Public Property ContextMenuMovieList() As ContextMenuStrip = New ContextMenuStrip

        Public Property ContextMenuMovieSetList() As ContextMenuStrip = New ContextMenuStrip

        Public Property ContextMenuTVEpisodeList() As ContextMenuStrip = New ContextMenuStrip

        Public Property ContextMenuTVSeasonList() As ContextMenuStrip = New ContextMenuStrip

        Public Property ContextMenuTVShowList() As ContextMenuStrip = New ContextMenuStrip

        Public Property MainMenu() As MenuStrip = New MenuStrip

        Public Property TrayMenu() As ContextMenuStrip = New ContextMenuStrip

        Public Property MainTabControl() As TabControl = New TabControl

#End Region 'Properties

#Region "Methods"

        Public Sub DelegateLoadMedia(ByRef lm As LoadMedia)
            'Setup from EmberAPP
            _LoadMedia = lm
        End Sub

        Public Sub DelegateOpenImageViewer(ByRef IV As OpenImageViewer)
            _OpenImageViewer = IV
        End Sub

        Public Sub InvokeLoadMedia(ByVal scan As Scanner.ScanOrCleanOptions, Optional ByVal sourceId As Long = -1)
            'Invoked from Addons
            _LoadMedia.Invoke(scan, sourceId)
        End Sub

        Public Sub InvokeOpenImageViewer(ByRef image As Image)
            _OpenImageViewer.Invoke(image)
        End Sub

#End Region 'Methods

    End Class

    <XmlRoot("addons")>
    Class AddonClass

#Region "Fields"

        <XmlIgnore>
        Public AddonInterface As Interfaces.IAddon
        Public AssemblyFileName As String
        <XmlIgnore>
        Public AssemblyName As String
        <XmlIgnore>
        Public AssemblyVersion As String
        Public Enabled As Boolean
        Public Enabled_Data_Movie As Boolean
        Public Enabled_Data_Movieset As Boolean
        Public Enabled_Data_TV As Boolean
        Public Enabled_Image_Movie As Boolean
        Public Enabled_Image_Movieset As Boolean
        Public Enabled_Image_TV As Boolean
        Public Enabled_Theme_Movie As Boolean
        Public Enabled_Theme_TV As Boolean
        Public Enabled_Trailer_Movie As Boolean
        Public Order As Integer
        Public Order_Data_Movie As Integer
        Public Order_Data_Movieset As Integer
        Public Order_Data_TV As Integer
        Public Order_Image_Movie As Integer
        Public Order_Image_Movieset As Integer
        Public Order_Image_TV As Integer
        Public Order_Theme_Movie As Integer
        Public Order_Theme_TV As Integer
        Public Order_Trailer_Movie As Integer

#End Region 'Fields

    End Class

#End Region 'Nested Types

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class 'AddonsManager

Public Class AddonSettings
    Inherits XmlAdvancedSettings

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private PreviousAssemblyName As String = String.Empty

#End Region 'Fields

#Region "Constructors"

    Private Sub New()
        Return
    End Sub

    Public Sub New(ByVal type As Enums.ContentType)
        MyBase.New(Path.Combine(Master.SettingsPath,
                                Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location),
                                String.Format("settings_{0}.xml", type.ToString.ToLower)
                                ))
        Load(type)
    End Sub

    Public Sub New(ByVal type As Enums.ContentType, oldAssemblyName As String)
        MyBase.New(Path.Combine(Master.SettingsPath,
                                Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location),
                                String.Format("settings_{0}.xml", type.ToString.ToLower)
                                ))
        PreviousAssemblyName = oldAssemblyName
        Load(type)
    End Sub

    Public Shadows Sub Load(ByVal type As Enums.ContentType)
        If File.Exists(FullName) Then
            Try
                Dim objStreamReader = New StreamReader(FullName)
                Dim nAdvancedSettings = CType(New XmlSerializer([GetType]).Deserialize(objStreamReader), AddonSettings)
                ComplexSettings = nAdvancedSettings.ComplexSettings
                Settings = nAdvancedSettings.Settings
                objStreamReader.Close()
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                FileUtils.Common.CreateFileBackup(FullName)
                Clear()
            End Try
        ElseIf Not String.IsNullOrEmpty(PreviousAssemblyName) Then
            'Try to get settings from "advancedsettings.xml"
            ComplexSettings = Master.eAdvancedSettings.ComplexSettings.Where(Function(f) f.Table.Section = PreviousAssemblyName).ToList
            Settings = Master.eAdvancedSettings.Settings.Where(Function(f) f.Section = PreviousAssemblyName AndAlso f.Content = type).ToList
            Save()
            'remove transferred settings from "advancedsettings.xml" 
            Master.eAdvancedSettings.ComplexSettings.RemoveAll(Function(f) f.Table.Section = PreviousAssemblyName)
            Master.eAdvancedSettings.Settings.RemoveAll(Function(f) f.Section = PreviousAssemblyName AndAlso f.Content = type)
            Master.eAdvancedSettings.Save()
        End If
    End Sub

#End Region 'Constructors

#Region "Methods"

    'Public Function GetComplexSetting(ByVal strKey As String) As List(Of TableItem)
    '    Dim v = _XmlAddonSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = strKey)
    '    Return If(v Is Nothing, Nothing, v.Table.Item)
    'End Function

    'Public Function GetBooleanSetting(ByVal key As String, ByVal defValue As Boolean, Optional ByVal type As Enums.ContentType = Enums.ContentType.None) As Boolean
    '    If type = Enums.ContentType.None Then
    '        Dim v = From e In _XmlAddonSettings.Settings.Where(Function(f) f.Name = key)
    '        Return If(v(0) Is Nothing, defValue, Convert.ToBoolean(v(0).Value))
    '    Else
    '        Dim v = From e In _XmlAddonSettings.Settings.Where(Function(f) f.Name = key AndAlso f.Content = type)
    '        Return If(v(0) Is Nothing, defValue, Convert.ToBoolean(v(0).Value))
    '    End If
    '    Return True
    'End Function

    'Public Function GetIntegerSetting(ByVal key As String, ByVal defValue As Integer, Optional ByVal type As Enums.ContentType = Enums.ContentType.None) As Integer
    '    If type = Enums.ContentType.None Then
    '        Dim v = From e In _XmlAddonSettings.Settings.Where(Function(f) f.Name = key)
    '        Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing OrElse Not Integer.TryParse(v(0).Value, 0), defValue, Convert.ToInt32(v(0).Value))
    '    Else
    '        Dim v = From e In _XmlAddonSettings.Settings.Where(Function(f) f.Name = key AndAlso f.Content = type)
    '        Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing OrElse Not Integer.TryParse(v(0).Value, 0), defValue, Convert.ToInt32(v(0).Value))
    '    End If
    'End Function

    'Public Function GetSizeSetting(ByVal key As String, ByVal defValue As Size, Optional ByVal type As Enums.ContentType = Enums.ContentType.None) As Size
    '    If type = Enums.ContentType.None Then
    '        Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = key)
    '        Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing, defValue, DirectCast(v(0).Value, Size))
    '    Else
    '        Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = key AndAlso f.Content = type)
    '        Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing, defValue, v(0).Value)
    '    End If
    'End Function

    'Public Function GetStringSetting(ByVal key As String, ByVal defValue As String, Optional ByVal type As Enums.ContentType = Enums.ContentType.None) As String
    '    If type = Enums.ContentType.None Then
    '        Dim v = From e In _XmlAddonSettings.Settings.Where(Function(f) f.Name = key)
    '        Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing, defValue, v(0).Value)
    '    Else
    '        Dim v = From e In _XmlAddonSettings.Settings.Where(Function(f) f.Name = key AndAlso f.Content = type)
    '        Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing, defValue, v(0).Value)
    '    End If
    'End Function

    'Private Sub Load()
    '    Try
    '        If File.Exists(_AddonSettingsPath) Then
    '            Dim objStreamReader As New StreamReader(_AddonSettingsPath)
    '            _XmlAddonSettings = CType(New XmlSerializer([GetType]).Deserialize(objStreamReader), clsXMLAdvancedSettings)
    '            objStreamReader.Close()
    '        End If
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '    End Try
    'End Sub

    'Public Sub Save()
    '    '_XMLAddonSettings.Setting.Sort()
    '    Try
    '        If Not Directory.Exists(Directory.GetParent(_AddonSettingsPath).FullName) Then
    '            Directory.CreateDirectory(Directory.GetParent(_AddonSettingsPath).FullName)
    '        End If
    '        Dim objWriter As New FileStream(_AddonSettingsPath, FileMode.Create)
    '        Dim xAddonSettings As New XmlSerializer(_XmlAddonSettings.GetType)
    '        xAddonSettings.Serialize(objWriter, _XmlAddonSettings)
    '        objWriter.Close()
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '    End Try
    'End Sub

    'Public Sub SetComplexSetting(ByVal key As String, ByVal value As List(Of TableItem))
    '    Dim v = _XmlAddonSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key)
    '    If v Is Nothing Then
    '        _XmlAddonSettings.ComplexSettings.Add(New ComplexSettings With {
    '                                              .Table = New Table With {
    '                                              .Name = key,
    '                                              .Item = value
    '                                              }
    '                                              })
    '    Else
    '        _XmlAddonSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key).Table.Item = value
    '    End If
    'End Sub

    'Public Sub SetBooleanSetting(ByVal key As String, ByVal value As Boolean, Optional ByVal isDefault As Boolean = False, Optional ByVal type As Enums.ContentType = Enums.ContentType.None)
    '    If type = Enums.ContentType.None Then
    '        Dim v = _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key)
    '        If v Is Nothing Then
    '            _XmlAddonSettings.Settings.Add(New SingleSetting With {
    '                                          .DefaultValue = If(isDefault, Convert.ToString(value), String.Empty),
    '                                          .Name = key,
    '                                          .Value = Convert.ToString(value)
    '                                          })
    '        Else
    '            _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key).Value = Convert.ToString(value)
    '        End If
    '    Else
    '        Dim v = _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Content = type)
    '        If v Is Nothing Then
    '            _XmlAddonSettings.Settings.Add(New SingleSetting With {
    '                                          .Content = type,
    '                                          .DefaultValue = If(isDefault, Convert.ToString(value), String.Empty),
    '                                          .Name = key,
    '                                          .Value = Convert.ToString(value)
    '                                          })
    '        Else
    '            _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Content = type).Value = Convert.ToString(value)
    '        End If
    '    End If
    'End Sub

    'Public Sub SetIntegerSetting(ByVal key As String, ByVal value As Integer, Optional ByVal isDefault As Boolean = False, Optional ByVal type As Enums.ContentType = Enums.ContentType.None)
    '    If type = Enums.ContentType.None Then
    '        Dim v = _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key)
    '        If v Is Nothing Then
    '            _XmlAddonSettings.Settings.Add(New SingleSetting With {
    '                                          .DefaultValue = If(isDefault, Convert.ToString(value), String.Empty),
    '                                          .Name = key,
    '                                          .Value = Convert.ToString(value)
    '                                          })
    '        Else
    '            _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key).Value = Convert.ToString(value)
    '        End If
    '    Else
    '        Dim v = _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Content = type)
    '        If v Is Nothing Then
    '            _XmlAddonSettings.Settings.Add(New SingleSetting With {
    '                                          .Content = type,
    '                                          .DefaultValue = If(isDefault, Convert.ToString(value), String.Empty),
    '                                          .Name = key,
    '                                          .Value = Convert.ToString(value)
    '                                          })
    '        Else
    '            _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Content = type).Value = Convert.ToString(value)
    '        End If
    '    End If
    'End Sub

    'Public Sub SetStringSetting(ByVal key As String, ByVal value As String, Optional ByVal isDefault As Boolean = False, Optional ByVal type As Enums.ContentType = Enums.ContentType.None)
    '    If type = Enums.ContentType.None Then
    '        Dim v = _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key)
    '        If v Is Nothing Then
    '            _XmlAddonSettings.Settings.Add(New SingleSetting With {
    '                                          .DefaultValue = If(isDefault, value, String.Empty),
    '                                          .Name = key,
    '                                          .Value = value
    '                                          })
    '        Else
    '            _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key).Value = value
    '        End If
    '    Else
    '        Dim v = _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Content = type)
    '        If v Is Nothing Then
    '            _XmlAddonSettings.Settings.Add(New SingleSetting With {
    '                                          .Content = type,
    '                                          .DefaultValue = If(isDefault, value, String.Empty),
    '                                          .Name = key,
    '                                          .Value = value
    '                                          })
    '        Else
    '            _XmlAddonSettings.Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Content = type).Value = value
    '        End If
    '    End If
    'End Sub

#End Region 'Methods

End Class 'XMLAddonSettings