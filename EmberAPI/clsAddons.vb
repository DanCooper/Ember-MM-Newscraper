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
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml.Serialization

Public Class AddonsManager

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared AssemblyList As New List(Of AssemblyListItem)
    Public Shared VersionList As New List(Of VersionItem)

    Public Addons As New List(Of AddonClass)
    Public RuntimeObjects As New EmberRuntimeObjects

    'Singleton Instace for module manager .. allways use this one
    Private Shared Singleton As AddonsManager = Nothing

    Friend WithEvents bwLoadAddons As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object))

#End Region 'Events

#Region "Properties"

    Public Shared ReadOnly Property Instance() As AddonsManager
        Get
            If Singleton Is Nothing Then
                Singleton = New AddonsManager()
            End If
            Return Singleton
        End Get
    End Property

    Public ReadOnly Property AllAddonsLoaded() As Boolean
        Get
            Return Not bwLoadAddons.IsBusy
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Private Sub BuildVersionList()
        VersionList.Clear()
        VersionList.Add(New VersionItem With {
                        .AssemblyFileName = "Ember Media Manager",
                        .Version = My.Application.Info.Version.ToString()
                        })
        VersionList.Add(New VersionItem With {
                        .AssemblyFileName = "EmberAPI",
                        .Version = Functions.EmberAPIVersion()
                        })
        Dim tmpAddonsList As New List(Of VersionItem)
        For Each addon In Addons
            tmpAddonsList.Add(New VersionItem With {
                            .AssemblyFileName = addon.AssemblyFileName,
                            .Version = addon.AssemblyVersion
                            })
        Next
        tmpAddonsList = tmpAddonsList.Distinct.ToList
        VersionList.AddRange(tmpAddonsList.OrderBy(Function(f) f.AssemblyFileName))
    End Sub

    Private Sub bwLoadAddons_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadAddons.DoWork
        LoadAddons()
    End Sub

    Private Sub bwLoadAddons_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadAddons.RunWorkerCompleted
        BuildVersionList()
    End Sub

    Public Function GetMovieCollectionID(ByVal IMDbID As String) As Integer
        Dim CollectionID As Integer = -1

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        'If Not String.IsNullOrEmpty(IMDbID) Then
        '    Dim ret As Interfaces.ModuleResult
        '    For Each addon In Addons.Where(Function(e) e.AssemblyName = "addon.themoviedb.org") 'TODO: create a proper call via Enums
        '        ret = addon.ProcessorModule.Run.GetCollectionID(IMDbID, CollectionID)
        '        If ret.breakChain Then Exit For
        '    Next
        'End If
        Return CollectionID
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

    Public Sub LoadAllAddons()
        bwLoadAddons.RunWorkerAsync()
    End Sub

    Public Sub LoadAddons()
        _Logger.Trace("[AddonsManager] [LoadAddons] [Start]")

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
                            .ProcessorModule = nAddonInterface
                        }
                        Addons.Add(nAddon)
                        nAddon.ProcessorModule.Init()

                        For Each i In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = nAddon.AssemblyName)
                            nAddon.ProcessorModule.IsEnabled = i.AddonEnabled
                        Next

                        'AddHandler nAddonInterface.GenericEvent, AddressOf GenericRunCallBack

                        'add addon to the list of available scrapers
                        If nAddon.ProcessorModule.Capabilities_AddonEventTypes.Contains(Enums.AddonEventType.Scrape_Movie) OrElse
                            nAddon.ProcessorModule.Capabilities_AddonEventTypes.Contains(Enums.AddonEventType.Scrape_Movieset) OrElse
                            nAddon.ProcessorModule.Capabilities_AddonEventTypes.Contains(Enums.AddonEventType.Scrape_TVEpisode) OrElse
                            nAddon.ProcessorModule.Capabilities_AddonEventTypes.Contains(Enums.AddonEventType.Scrape_TVSeason) OrElse
                            nAddon.ProcessorModule.Capabilities_AddonEventTypes.Contains(Enums.AddonEventType.Scrape_TVShow) Then
                            Master.ScraperList.Add(New ScraperProperties(nAddon.AssemblyName, nAddon.ProcessorModule.Capabilities_ScraperCapatibilities))
                        End If

                        'add addon to the list of available search engines
                        If nAddon.ProcessorModule.Capabilities_AddonEventTypes.Contains(Enums.AddonEventType.Search_Movie) OrElse
                            nAddon.ProcessorModule.Capabilities_AddonEventTypes.Contains(Enums.AddonEventType.Search_Movieset) OrElse
                            nAddon.ProcessorModule.Capabilities_AddonEventTypes.Contains(Enums.AddonEventType.Search_TVEpisode) OrElse
                            nAddon.ProcessorModule.Capabilities_AddonEventTypes.Contains(Enums.AddonEventType.Search_TVSeason) OrElse
                            nAddon.ProcessorModule.Capabilities_AddonEventTypes.Contains(Enums.AddonEventType.Search_TVShow) Then
                            Master.SearchEngineList.Add(New SearchEngineProperties(nAddon.AssemblyName, nAddon.ProcessorModule.Capabilities_AddonEventTypes))
                        End If
                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", nAddon.AssemblyName))
                    End If
                Next
            Next
        Else
            _Logger.Warn(String.Format("[AddonsManager] [LoadAddons] No directory ""{0}"" found!", Master.AddonsPath))
        End If
        _Logger.Trace("[AddonsManager] [LoadAddons] [Done]")
    End Sub

    Function QueryAnyAddonIsBusy() As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Dim lstBusyAddons = Addons.Where(Function(e) e.ProcessorModule.IsBusy)
        If lstBusyAddons.Count() > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'Function QueryScraperCapabilities_Image_Movie(ByVal addon As AddonClass, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Boolean
    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    If ScrapeModifiers.MainBanner AndAlso addon.ProcessorModule.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Banner) Then Return True
    '    If ScrapeModifiers.MainClearArt AndAlso addon.ProcessorModule.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_ClearArt) Then Return True
    '    If ScrapeModifiers.MainClearLogo AndAlso addon.ProcessorModule.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_ClearLogo) Then Return True
    '    If ScrapeModifiers.MainDiscArt AndAlso addon.ProcessorModule.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_DiscArt) Then Return True
    '    If ScrapeModifiers.MainExtrafanarts AndAlso addon.ProcessorModule.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Fanart) Then Return True
    '    If ScrapeModifiers.MainExtrathumbs AndAlso addon.ProcessorModule.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Fanart) Then Return True
    '    If ScrapeModifiers.MainFanart AndAlso addon.ProcessorModule.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Fanart) Then Return True
    '    If ScrapeModifiers.MainKeyArt AndAlso addon.ProcessorModule.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_KeyArt) Then Return True
    '    If ScrapeModifiers.MainLandscape AndAlso addon.ProcessorModule.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Landscape) Then Return True
    '    If ScrapeModifiers.MainPoster AndAlso addon.ProcessorModule.Capabilities_ScraperCapatibilities.Contains(Enums.ScraperCapatibility.Movie_Image_Poster) Then Return True

    '    Return False
    'End Function

    'Function QueryScraperCapabilities_Image_Movie(ByVal addon As AddonClass, ByVal ImageType As Enums.ModifierType) As Boolean
    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    Select Case ImageType
    '        Case Enums.ModifierType.MainExtrafanarts
    '            Return addon.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart)
    '        Case Enums.ModifierType.MainExtrathumbs
    '            Return addon.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart)
    '        Case Enums.ModifierType.MainKeyArt
    '            Return addon.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster)
    '        Case Else
    '            Return addon.ProcessorModule.QueryScraperCapabilities(ImageType)
    '    End Select

    '    Return False
    'End Function

    'Function QueryScraperCapabilities_Image_MovieSet(ByVal externalScraperModule As ScraperAddon_Image_MovieSet, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Boolean
    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    If ScrapeModifiers.MainBanner AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainBanner) Then Return True
    '    If ScrapeModifiers.MainClearArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearArt) Then Return True
    '    If ScrapeModifiers.MainClearLogo AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearLogo) Then Return True
    '    If ScrapeModifiers.MainDiscArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainDiscArt) Then Return True
    '    If ScrapeModifiers.MainFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
    '    If ScrapeModifiers.MainLandscape AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainLandscape) Then Return True
    '    If ScrapeModifiers.MainPoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) Then Return True

    '    Return False
    'End Function

    'Function QueryScraperCapabilities_Image_MovieSet(ByVal externalScraperModule As ScraperAddon_Image_MovieSet, ByVal ImageType As Enums.ModifierType) As Boolean
    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    Select Case ImageType
    '        Case Enums.ModifierType.MainKeyArt
    '            Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster)
    '        Case Else
    '            Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(ImageType)
    '    End Select

    '    Return False
    'End Function

    'Function QueryScraperCapabilities_Image_TV(ByVal externalScraperModule As ScraperAddon_Image_TV, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Boolean
    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    If ScrapeModifiers.EpisodeFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.EpisodeFanart) Then Return True
    '    If ScrapeModifiers.EpisodePoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.EpisodePoster) Then Return True
    '    If ScrapeModifiers.MainBanner AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainBanner) Then Return True
    '    If ScrapeModifiers.MainCharacterArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainCharacterArt) Then Return True
    '    If ScrapeModifiers.MainClearArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearArt) Then Return True
    '    If ScrapeModifiers.MainClearLogo AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearLogo) Then Return True
    '    If ScrapeModifiers.MainFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
    '    If ScrapeModifiers.MainKeyArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) Then Return True
    '    If ScrapeModifiers.MainLandscape AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainLandscape) Then Return True
    '    If ScrapeModifiers.MainPoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) Then Return True
    '    If ScrapeModifiers.SeasonBanner AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonBanner) Then Return True
    '    If ScrapeModifiers.SeasonFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonFanart) Then Return True
    '    If ScrapeModifiers.SeasonLandscape AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonLandscape) Then Return True
    '    If ScrapeModifiers.SeasonPoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonPoster) Then Return True

    '    Return False
    'End Function

    'Function QueryScraperCapabilities_Image_TV(ByVal externalScraperModule As ScraperAddon_Image_TV, ByVal ImageType As Enums.ModifierType) As Boolean
    '    While Not AllAddonsLoaded
    '        Application.DoEvents()
    '    End While

    '    Select Case ImageType
    '        Case Enums.ModifierType.AllSeasonsBanner
    '            If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainBanner) OrElse
    '                externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonBanner) Then Return True
    '        Case Enums.ModifierType.AllSeasonsFanart
    '            If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) OrElse
    '                externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonFanart) Then Return True
    '        Case Enums.ModifierType.AllSeasonsLandscape
    '            If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainLandscape) OrElse
    '                externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonLandscape) Then Return True
    '        Case Enums.ModifierType.AllSeasonsPoster
    '            If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) OrElse
    '                externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonPoster) Then Return True
    '        Case Enums.ModifierType.EpisodeFanart
    '            If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) OrElse
    '                externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.EpisodeFanart) Then Return True
    '        Case Enums.ModifierType.MainExtrafanarts
    '            Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart)
    '        Case Enums.ModifierType.SeasonFanart
    '            If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) OrElse
    '                externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonFanart) Then Return True
    '        Case Enums.ModifierType.MainKeyArt
    '            Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster)
    '        Case Else
    '            Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(ImageType)
    '    End Select

    '    Return False
    'End Function
    ''' <summary>
    ''' Calls all the generic modules of the supplied type (if one is defined), passing the supplied _params.
    ''' The module will do its task and return any expected results in the _refparams.
    ''' </summary>
    ''' <param name="mType">The <c>Enums.ModuleEventType</c> of module to execute.</param>
    ''' <param name="_params">Parameters to pass to the module</param>
    ''' <param name="_singleobjekt"><c>Object</c> representing the module's result (if relevant)</param>
    ''' <param name="RunOnlyOne">If <c>True</c>, allow only one module to perform the required task.</param>
    ''' <returns></returns>
    ''' <remarks>Note that if any module returns a result of breakChain, no further modules are processed</remarks>
    Public Function RunGeneric(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object), Optional ByVal _singleobjekt As Object = Nothing, Optional ByVal RunOnlyOne As Boolean = False, Optional ByRef DBElement As Database.DBElement = Nothing) As Boolean
        _Logger.Trace(String.Format("[AddonsManager] [RunGeneric] [Started] <{0}>", mType.ToString))
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Dim nResult As New Interfaces.AddonResult
        Dim lstAddons = Addons.Where(Function(e) e.ProcessorModule.Capabilities_AddonEventTypes.Contains(mType) AndAlso e.ProcessorModule.IsEnabled)
        If lstAddons.Count > 0 Then
            For Each i In lstAddons
                Try
                    _Logger.Trace("[AddonsManager] [RunGeneric] Run addon <{0}>", i.AssemblyName)
                    nResult = i.ProcessorModule.Run(DBElement, mType, New List(Of Object))
                    _Logger.Trace("[AddonsManager] [RunGeneric] addon done <{0}>", i.AssemblyName)
                Catch ex As Exception
                    _Logger.Error("[AddonsManager] [RunGeneric] Run generic module <{0}>", i.AssemblyName)
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
                If nResult.bBreakChain OrElse RunOnlyOne Then Exit For
            Next
        Else
            _Logger.Info("[AddonsManager] [RunGeneric] No addon defined <{0}>", mType.ToString)
        End If

        _Logger.Trace("[AddonsManager] [RunGeneric] [Done]")
        Return Not nResult.bCancelled
    End Function
    ''' <summary>
    ''' Calls all the generic modules of the supplied type (if one is defined), passing the supplied _params.
    ''' The module will do its task and return any expected results in the _refparams.
    ''' </summary>
    ''' <param name="type">The <c>Enums.AddonEventType</c> of addon to execute.</param>
    ''' <returns></returns>
    ''' <remarks>Note that if any module returns a result of breakChain, no further modules are processed</remarks>
    Public Function RunSearch(ByRef dbelement As Database.DBElement, ByVal type As Enums.AddonEventType) As List(Of MediaContainers.MainDetails)
        _Logger.Trace(String.Format("[AddonsManager] [RunSearch] [Start] {0}", dbelement.FileItem.FirstPathFromStack))
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Dim nSearchResluts As New List(Of MediaContainers.MainDetails)
        If Addons.Count > 0 Then
            For Each cAddon In Addons
                _Logger.Trace(String.Format("[AddonsManager] [RunSearch] [Using] {0}", cAddon.AssemblyName))
                Dim nAddonResult = cAddon.ProcessorModule.Run(dbelement, type, Nothing)
                If nAddonResult IsNot Nothing Then
                    If nAddonResult.SearchResults IsNot Nothing Then nSearchResluts.AddRange(nAddonResult.SearchResults)
                End If
                _Logger.Trace(String.Format("[AddonsManager] [RunSearch] [EndUsing] {0}", cAddon.AssemblyName))
            Next
        Else
            _Logger.Warn("[AddonsManager] [RunSearch] [Abort] No addons found")
        End If

        _Logger.Trace(String.Format("[AddonsManager] [RunSearch] [Done] {0}", dbelement.FileItem.FirstPathFromStack))
        Return nSearchResluts
    End Function

    Function ScraperWithCapabilityAnyEnabled_Image_Movie(ByVal ImageType As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        'For Each _externalScraperModule As ScraperAddon_Image_Movie In ScraperAddons_Image_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        '    Try
        '        ret = QueryScraperCapabilities_Image_Movie(_externalScraperModule, ImageType)
        '        If ret Then Exit For
        '    Catch ex As Exception
        '    End Try
        'Next
        Return True
    End Function

    Function ScraperWithCapabilityAnyEnabled_Image_MovieSet(ByVal ImageType As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        'For Each _externalScraperModule As ScraperAddon_Image_MovieSet In ScraperAddons_Image_Movieset.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        '    Try
        '        ret = QueryScraperCapabilities_Image_MovieSet(_externalScraperModule, ImageType)
        '        If ret Then Exit For
        '    Catch ex As Exception
        '    End Try
        'Next
        Return True
    End Function

    Function ScraperWithCapabilityAnyEnabled_Image_TV(ByVal ImageType As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        'For Each _externalScraperModule As ScraperAddon_Image_TV In ScraperAddons_Image_TV.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        '    Try
        '        ret = QueryScraperCapabilities_Image_TV(_externalScraperModule, ImageType)
        '        If ret Then Exit For
        '    Catch ex As Exception
        '    End Try
        'Next
        Return True
    End Function

    Function ScraperWithCapabilityAnyEnabled_Theme_Movie(ByVal cap As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        'For Each _externalScraperModule As ScraperAddon_Theme_Movie In ScraperAddons_Theme_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        '    Try
        '        ret = True 'if a theme scraper is enabled we can exit.
        '        Exit For
        '    Catch ex As Exception
        '    End Try
        'Next
        Return True
    End Function

    Function ScraperWithCapabilityAnyEnabled_Theme_TV(ByVal cap As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        'For Each _externalScraperModule As ScraperAddon_Theme_TV In ScraperAddons_Theme_TV.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        '    Try
        '        ret = True 'if a theme scraper is enabled we can exit.
        '        Exit For
        '    Catch ex As Exception
        '    End Try
        'Next
        Return True
    End Function

    Function ScraperWithCapabilityAnyEnabled_Trailer_Movie(ByVal cap As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        'For Each _externalScraperModule As ScraperAddon_Trailer_Movie In ScraperAddons_Trailer_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        '    Try
        '        ret = True 'if a trailer scraper is enabled we can exit.
        '        Exit For
        '    Catch ex As Exception
        '    End Try
        'Next
        Return True
    End Function

    Public Sub Settings_Save()
        Dim tmpForXML As New List(Of XMLAddonClass)

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        For Each i In Addons
            Dim t As New XMLAddonClass With {
                .AddonEnabled = i.ProcessorModule.IsEnabled,
                .AssemblyFileName = i.AssemblyFileName,
                .AssemblyName = i.AssemblyName
            }
            tmpForXML.Add(t)
        Next
        Master.eSettings.Addons = tmpForXML
        Master.eSettings.Save()
    End Sub

    Private Sub GenericRunCallBack(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object))
        'RaiseEvent GenericEvent(mType, _params)
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
        Public Version As String

#End Region 'Fields

    End Structure

    Class AddonClass

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IAddon

#End Region 'Fields

    End Class

    Class EmberRuntimeObjects

#Region "Fields"

        Private _ListMovieSets As String
        Private _ListMovies As String
        Private _ListTVShows As String
        Private _LoadMedia As LoadMedia

#End Region 'Fields

#Region "Delegates"

        Delegate Sub LoadMedia(ByVal Scan As Scanner.ScanOrCleanOptions, ByVal SourceID As Long)

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

        Public Property ListMovieSets() As String
            Get
                Return If(_ListMovieSets IsNot Nothing, _ListMovieSets, "moviesetlist")
            End Get
            Set(ByVal value As String)
                _ListMovieSets = value
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

        Public Property ContextMenuMovieList() As ContextMenuStrip
        Public Property ContextMenuMovieSetList() As ContextMenuStrip
        Public Property ContextMenuTVEpisodeList() As ContextMenuStrip
        Public Property ContextMenuTVSeasonList() As ContextMenuStrip
        Public Property ContextMenuTVShowList() As ContextMenuStrip
        Public Property FilterMovies() As String
        Public Property FilterMoviesSearch() As String
        Public Property FilterMoviesType() As String
        Public Property FilterMoviesets() As String
        Public Property FilterTVShows() As String
        Public Property FilterTVShowsSearch() As String
        Public Property FilterTVShowsType() As String
        Public Property MainMenu() As MenuStrip
        Public Property MainToolStrip() As ToolStrip
        Public Property MediaListMovieSets() As DataGridView
        Public Property MediaListMovies() As DataGridView
        Public Property MediaListTVEpisodes() As DataGridView
        Public Property MediaListTVSeasons() As DataGridView
        Public Property MediaListTVShows() As DataGridView
        Public Property MediaTabSelected() As Settings.MainTabSorting
        Public Property TrayMenu() As ContextMenuStrip

#End Region 'Properties

    End Class

    <XmlRoot("Addon")>
    Class XMLAddonClass

#Region "Fields"

        Public AddonEnabled As Boolean
        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String

#End Region 'Fields

    End Class

#End Region 'Nested Types

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class

Public Class XMLAddonSettings

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _strAddonSettingsPath As String = String.Empty
    Private _XMLAddonSettings As New XMLAdvancedSettings

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        _strAddonSettingsPath = Path.Combine(Master.SettingsPath, Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location), "settings.xml")
        Load()
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Function GetComplexSetting(ByVal strKey As String) As List(Of AdvancedSettingsComplexSettingsTableItem)
        Dim v = _XMLAddonSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = strKey)
        Return If(v Is Nothing, Nothing, v.Table.Item)
    End Function

    Public Function GetBooleanSetting(ByVal strKey As String, ByVal strDefValue As Boolean, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None) As Boolean
        If tContentType = Enums.ContentType.None Then
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey)
            Return If(v(0) Is Nothing, strDefValue, Convert.ToBoolean(v(0).Value))
        Else
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            Return If(v(0) Is Nothing, strDefValue, Convert.ToBoolean(v(0).Value))
        End If
        Return True
    End Function

    Public Function GetIntegerSetting(ByVal strKey As String, ByVal iDefValue As Integer, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None) As Integer
        If tContentType = Enums.ContentType.None Then
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey)
            Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing OrElse Not Integer.TryParse(v(0).Value, 0), iDefValue, CInt(v(0).Value))
        Else
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing OrElse Not Integer.TryParse(v(0).Value, 0), iDefValue, CInt(v(0).Value))
        End If
    End Function

    Public Function GetStringSetting(ByVal strKey As String, ByVal strDefValue As String, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None) As String
        If tContentType = Enums.ContentType.None Then
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey)
            Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing, strDefValue, v(0).Value)
        Else
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing, strDefValue, v(0).Value)
        End If
    End Function

    Private Sub Load()
        Try
            If File.Exists(_strAddonSettingsPath) Then
                Dim objStreamReader As New StreamReader(_strAddonSettingsPath)
                Dim xAddonSettings As New XmlSerializer(_XMLAddonSettings.GetType)
                _XMLAddonSettings = CType(xAddonSettings.Deserialize(objStreamReader), XMLAdvancedSettings)
                objStreamReader.Close()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Sub Save()
        '_XMLAddonSettings.Setting.Sort()
        Try
            If Not Directory.Exists(Directory.GetParent(_strAddonSettingsPath).FullName) Then
                Directory.CreateDirectory(Directory.GetParent(_strAddonSettingsPath).FullName)
            End If
            Dim objWriter As New FileStream(_strAddonSettingsPath, FileMode.Create)
            Dim xAddonSettings As New XmlSerializer(_XMLAddonSettings.GetType)
            xAddonSettings.Serialize(objWriter, _XMLAddonSettings)
            objWriter.Close()
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Sub SetComplexSetting(ByVal strKey As String, ByVal tValue As List(Of AdvancedSettingsComplexSettingsTableItem))
        Dim v = _XMLAddonSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = strKey)
        If v Is Nothing Then
            _XMLAddonSettings.ComplexSettings.Add(New AdvancedSettingsComplexSettings With {.Table = New AdvancedSettingsComplexSettingsTable With {.Name = strKey, .Item = tValue}})
        Else
            _XMLAddonSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = strKey).Table.Item = tValue
        End If
    End Sub

    Public Sub SetBooleanSetting(ByVal strKey As String, ByVal bValue As Boolean, Optional ByVal isDefault As Boolean = False, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None)
        If tContentType = Enums.ContentType.None Then
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .DefaultValue = If(isDefault, Convert.ToString(bValue), String.Empty),
                                           .Name = strKey,
                                           .Value = Convert.ToString(bValue)
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey).Value = Convert.ToString(bValue)
            End If
        Else
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .Content = tContentType,
                                           .DefaultValue = If(isDefault, Convert.ToString(bValue), String.Empty),
                                           .Name = strKey,
                                           .Value = Convert.ToString(bValue)
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType).Value = Convert.ToString(bValue)
            End If
        End If
    End Sub

    Public Sub SetIntegerSetting(ByVal strKey As String, ByVal iValue As Integer, Optional ByVal isDefault As Boolean = False, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None)
        If tContentType = Enums.ContentType.None Then
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .DefaultValue = If(isDefault, CStr(iValue), String.Empty),
                                           .Name = strKey,
                                           .Value = CStr(iValue)
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey).Value = CStr(iValue)
            End If
        Else
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .Content = tContentType,
                                           .DefaultValue = If(isDefault, CStr(iValue), String.Empty),
                                           .Name = strKey,
                                           .Value = CStr(iValue)
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType).Value = CStr(iValue)
            End If
        End If
    End Sub

    Public Sub SetStringSetting(ByVal strKey As String, ByVal strValue As String, Optional ByVal isDefault As Boolean = False, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None)
        If tContentType = Enums.ContentType.None Then
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .DefaultValue = If(isDefault, strValue, String.Empty),
                                           .Name = strKey,
                                           .Value = strValue
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey).Value = strValue
            End If
        Else
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .Content = tContentType,
                                           .DefaultValue = If(isDefault, strValue, String.Empty),
                                           .Name = strKey,
                                           .Value = strValue
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType).Value = strValue
            End If
        End If
    End Sub

#End Region 'Methods

End Class