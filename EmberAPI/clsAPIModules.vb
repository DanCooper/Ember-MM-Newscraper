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

Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Windows.Forms
Imports System.Drawing
Imports NLog
Public Class ModulesManager

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Shared AssemblyList As New List(Of AssemblyListItem)
    Public Shared VersionList As New List(Of VersionItem)

    Public externalProcessorModules As New List(Of _externalGenericModuleClass)
    Public externalScrapersModules_Data_Movie As New List(Of _externalScraperModuleClass_Data_Movie)
    Public externalScrapersModules_Image_Movie As New List(Of _externalScraperModuleClass_Image_Movie)
    Public externalScrapersModules_Theme_Movie As New List(Of _externalScraperModuleClass_Theme_Movie)
    Public externalScrapersModules_Trailer_Movie As New List(Of _externalScraperModuleClass_Trailer_Movie)
    Public externalScrapersModules_Data_MovieSet As New List(Of _externalScraperModuleClass_Data_MovieSet)
    'Public externalTVDataScrapersModules As New List(Of _externalTVScraperModuleClass_Data)
    'Public externalTVPosterScrapersModules As New List(Of _externalTVScraperModuleClass_Poster)
    Public externalScrapersModules_TV As New List(Of _externalScraperModuleClass_TV)
    Public externalScrapersModules_Theme_TV As New List(Of _externalScraperModuleClass_Theme_TV)
    Public RuntimeObjects As New EmberRuntimeObjects

    'Singleton Instace for module manager .. allways use this one
    Private Shared Singleton As ModulesManager = Nothing

    Private moduleLocation As String = Path.Combine(Functions.AppPath, "Modules")

    Friend WithEvents bwloadModules As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwloadMovieScrapersModules As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwloadMovieSetScrapersModules As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwloadTVScrapersModules As New System.ComponentModel.BackgroundWorker

    Dim bwloadModules_done As Boolean
    Dim bwloadMovieScrapersModules_done As Boolean
    Dim bwloadMovieSetScrapersModules_done As Boolean
    Dim bwloadTVScrapersModules_done As Boolean

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Event MovieScraperEvent(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object)

    Event MovieSetScraperEvent(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object)

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Event TVScraperEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)

#End Region 'Events

#Region "Properties"

    Public Shared ReadOnly Property Instance() As ModulesManager
        Get
            If (Singleton Is Nothing) Then
                Singleton = New ModulesManager()
            End If
            Return Singleton
        End Get
    End Property

    Public ReadOnly Property TVIsBusy() As Boolean
        Get
            Dim ret As Boolean = False
            For Each _externaltvScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(e) e.ProcessorModule.IsScraper AndAlso e.ProcessorModule.ScraperEnabled)
                ret = ret OrElse _externaltvScraperModule.ProcessorModule.IsBusy
            Next
            Return ret
        End Get

    End Property

    Public ReadOnly Property ModulesLoaded() As Boolean
        Get
            Return bwloadModules_done AndAlso bwloadMovieScrapersModules_done AndAlso bwloadMovieSetScrapersModules_done AndAlso bwloadTVScrapersModules_done
        End Get

    End Property
#End Region 'Properties

#Region "Methods"

    Public Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As MediaContainers.EpisodeDetails
        Dim epDetails As New MediaContainers.EpisodeDetails

        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While

        If Not String.IsNullOrEmpty(TVDBID) AndAlso Not String.IsNullOrEmpty(Lang) Then
            Dim ret As Interfaces.ModuleResult
            For Each _externaltvScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(e) e.ProcessorModule.IsScraper AndAlso e.ProcessorModule.ScraperEnabled)
                ret = _externaltvScraperModule.ProcessorModule.GetSingleEpisode(ShowID, TVDBID, Season, Episode, Lang, Ordering, Options, epDetails)
                If ret.breakChain Then Exit For
            Next
        End If
        Return epDetails
    End Function

    Public Function GetMovieCollectionID(ByVal sIMDBID As String) As String
        Dim CollectionID As String = String.Empty

        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While

        If Not String.IsNullOrEmpty(sIMDBID) Then
            Dim ret As Interfaces.ModuleResult
            For Each _externalScraperModuleClass_Data As _externalScraperModuleClass_Data_MovieSet In externalScrapersModules_Data_MovieSet.Where(Function(e) e.ProcessorModule.ModuleName = "TMDB_Data")
                ret = _externalScraperModuleClass_Data.ProcessorModule.GetCollectionID(sIMDBID, CollectionID)
                If ret.breakChain Then Exit For
            Next
        End If
        Return CollectionID
    End Function

    Public Sub GetVersions()
        Dim dlgVersions As New dlgVersions
        Dim li As ListViewItem
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each v As VersionItem In VersionList
            li = dlgVersions.lstVersions.Items.Add(v.Name)
            li.SubItems.Add(v.Version)
        Next
        dlgVersions.ShowDialog()
    End Sub

    Public Sub Handler_MovieScraperEvent(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object)
        RaiseEvent MovieScraperEvent(eType, Parameter)
    End Sub

    Public Sub Handler_MovieSetScraperEvent(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object)
        RaiseEvent MovieSetScraperEvent(eType, Parameter)
    End Sub

    Public Sub Handler_TVScraperEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)
        RaiseEvent TVScraperEvent(eType, iProgress, Parameter)
    End Sub

    Public Sub LoadAllModules()
        'loadModules()
        'loadScrapersModules()
        'loadTVScrapersModules()
        'BuildVersionList()
        bwloadModules_done = False
        bwloadMovieScrapersModules_done = False
        bwloadMovieSetScrapersModules_done = False
        bwloadTVScrapersModules_done = False
        bwloadModules.RunWorkerAsync()
        bwloadMovieScrapersModules.RunWorkerAsync()
        bwloadMovieSetScrapersModules.RunWorkerAsync()
        bwloadTVScrapersModules.RunWorkerAsync()


        'Master.eLang.LoadAllLanguage(Master.eSettings.Language)
    End Sub

    ''' <summary>
    ''' Load all Generic Modules and field in externalProcessorModules List
    ''' </summary>
    Public Sub loadModules(Optional ByVal modulefile As String = "*.dll")
        logger.Trace("loadModules started")
        If Directory.Exists(moduleLocation) Then
            'Assembly to load the file
            Dim assembly As System.Reflection.Assembly
            'For each .dll file in the module directory
            For Each file As String In System.IO.Directory.GetFiles(moduleLocation, modulefile)
                Try
                    'Load the assembly
                    assembly = System.Reflection.Assembly.LoadFile(file)
                    'Loop through each of the assemeblies type
                    For Each fileType As Type In assembly.GetTypes
                        Try
                            'Activate the located module
                            Dim t As Type = fileType.GetInterface("EmberExternalModule")
                            If Not t Is Nothing Then
                                Dim ProcessorModule As Interfaces.GenericModule 'Object
                                ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.GenericModule)
                                'Add the activated module to the arraylist
                                Dim _externalProcessorModule As New _externalGenericModuleClass
                                Dim filename As String = file
                                If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                    AssemblyList.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                                End If
                                _externalProcessorModule.ProcessorModule = ProcessorModule
                                _externalProcessorModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                                _externalProcessorModule.AssemblyFileName = Path.GetFileName(file)
                                _externalProcessorModule.Type = ProcessorModule.ModuleType
                                externalProcessorModules.Add(_externalProcessorModule)
                                ProcessorModule.Init(_externalProcessorModule.AssemblyName, Path.GetFileNameWithoutExtension(file))
                                Dim found As Boolean = False
                                For Each i In Master.eSettings.EmberModules
                                    If i.AssemblyName = _externalProcessorModule.AssemblyName Then
                                        _externalProcessorModule.ProcessorModule.Enabled = i.GenericEnabled
                                        found = True
                                    End If
                                Next
                                If Not found AndAlso Path.GetFileNameWithoutExtension(file).Contains("generic.EmberCore") Then
                                    _externalProcessorModule.ProcessorModule.Enabled = True
                                    'SetModuleEnable(_externalProcessorModule.AssemblyName, True)
                                End If
                                AddHandler ProcessorModule.GenericEvent, AddressOf GenericRunCallBack
                                'ProcessorModule.Enabled = _externalProcessorModule.ProcessorModule.Enabled
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                Catch ex As Exception
                End Try
            Next
            Dim c As Integer = 0
            For Each ext As _externalGenericModuleClass In externalProcessorModules.OrderBy(Function(x) x.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next

        End If
        logger.Trace("loadModules finished")

    End Sub

    ''' <summary>
    ''' Load all Scraper Modules and field in externalScrapersModules List
    ''' </summary>
    Public Sub loadMovieScrapersModules(Optional ByVal modulefile As String = "*.dll")
        logger.Trace("loadMovieScrapersModules started")
        Dim DataScraperAnyEnabled As Boolean = False
        Dim DataScraperFound As Boolean = False
        Dim ImageScraperAnyEnabled As Boolean = False
        Dim PosterScraperFound As Boolean = False
        Dim ThemeScraperAnyEnabled As Boolean = False
        Dim ThemeScraperFound As Boolean = False
        Dim TrailerScraperAnyEnabled As Boolean = False
        Dim TrailerScraperFound As Boolean = False

        If Directory.Exists(moduleLocation) Then
            'Assembly to load the file
            Dim assembly As System.Reflection.Assembly
            'For each .dll file in the module directory
            For Each file As String In System.IO.Directory.GetFiles(moduleLocation, modulefile)
                Try
                    assembly = System.Reflection.Assembly.LoadFile(file)
                    'Loop through each of the assemeblies type
                    For Each fileType As Type In assembly.GetTypes

                        'Activate the located module
                        Dim t1 As Type = fileType.GetInterface("ScraperModule_Data_Movie")
                        If Not t1 Is Nothing Then
                            Dim ProcessorModule As Interfaces.ScraperModule_Data_Movie
                            ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Data_Movie)
                            'Add the activated module to the arraylist
                            Dim _externalScraperModule As New _externalScraperModuleClass_Data_Movie
                            Dim filename As String = file
                            If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                AssemblyList.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                            End If
                            _externalScraperModule.ProcessorModule = ProcessorModule
                            _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                            _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                            externalScrapersModules_Data_Movie.Add(_externalScraperModule)
                            _externalScraperModule.ProcessorModule.Init(_externalScraperModule.AssemblyName)
                            For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(x) x.AssemblyName = _externalScraperModule.AssemblyName AndAlso _
                                                                                                          x.ContentType = Enums.Content_Type.Movie)
                                _externalScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                                DataScraperAnyEnabled = DataScraperAnyEnabled OrElse i.ModuleEnabled
                                _externalScraperModule.ModuleOrder = i.ModuleOrder
                                DataScraperFound = True
                            Next
                            If Not DataScraperFound Then
                                _externalScraperModule.ModuleOrder = 999
                            End If
                        Else
                            Dim t2 As Type = fileType.GetInterface("ScraperModule_Image_Movie")
                            If Not t2 Is Nothing Then
                                Dim ProcessorModule As Interfaces.ScraperModule_Image_Movie
                                ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Image_Movie)
                                'Add the activated module to the arraylist
                                Dim _externalScraperModule As New _externalScraperModuleClass_Image_Movie
                                Dim filename As String = file
                                If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                    AssemblyList.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                                End If
                                _externalScraperModule.ProcessorModule = ProcessorModule
                                _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                                _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                                externalScrapersModules_Image_Movie.Add(_externalScraperModule)
                                _externalScraperModule.ProcessorModule.Init(_externalScraperModule.AssemblyName)
                                For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(x) x.AssemblyName = _externalScraperModule.AssemblyName AndAlso _
                                                                                                          x.ContentType = Enums.Content_Type.Movie)
                                    _externalScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                                    ImageScraperAnyEnabled = ImageScraperAnyEnabled OrElse i.ModuleEnabled
                                    _externalScraperModule.ModuleOrder = i.ModuleOrder
                                    PosterScraperFound = True
                                Next
                                If Not PosterScraperFound Then
                                    _externalScraperModule.ModuleOrder = 999
                                End If
                            Else
                                Dim t3 As Type = fileType.GetInterface("ScraperModule_Trailer_Movie")
                                If Not t3 Is Nothing Then
                                    Dim ProcessorModule As Interfaces.ScraperModule_Trailer_Movie
                                    ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Trailer_Movie)
                                    'Add the activated module to the arraylist
                                    Dim _externalScraperModule As New _externalScraperModuleClass_Trailer_Movie
                                    Dim filename As String = file
                                    If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                        AssemblyList.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                                    End If
                                    _externalScraperModule.ProcessorModule = ProcessorModule
                                    _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                                    _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                                    externalScrapersModules_Trailer_Movie.Add(_externalScraperModule)
                                    _externalScraperModule.ProcessorModule.Init(_externalScraperModule.AssemblyName)
                                    For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(x) x.AssemblyName = _externalScraperModule.AssemblyName AndAlso _
                                                                                                          x.ContentType = Enums.Content_Type.Movie)
                                        _externalScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                                        TrailerScraperAnyEnabled = TrailerScraperAnyEnabled OrElse i.ModuleEnabled
                                        _externalScraperModule.ModuleOrder = i.ModuleOrder
                                        TrailerScraperFound = True
                                    Next
                                    If Not TrailerScraperFound Then
                                        _externalScraperModule.ModuleOrder = 999
                                    End If
                                Else
                                    Dim t4 As Type = fileType.GetInterface("ScraperModule_Theme_Movie")
                                    If Not t4 Is Nothing Then
                                        Dim ProcessorModule As Interfaces.ScraperModule_Theme_Movie
                                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Theme_Movie)
                                        'Add the activated module to the arraylist
                                        Dim _externalScraperModule As New _externalScraperModuleClass_Theme_Movie
                                        Dim filename As String = file
                                        If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                            AssemblyList.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                                        End If
                                        _externalScraperModule.ProcessorModule = ProcessorModule
                                        _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                                        _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                                        externalScrapersModules_Theme_Movie.Add(_externalScraperModule)
                                        _externalScraperModule.ProcessorModule.Init(_externalScraperModule.AssemblyName)
                                        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(x) x.AssemblyName = _externalScraperModule.AssemblyName AndAlso _
                                                                                                          x.ContentType = Enums.Content_Type.Movie)
                                            _externalScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                                            ThemeScraperAnyEnabled = ThemeScraperAnyEnabled OrElse i.ModuleEnabled
                                            _externalScraperModule.ModuleOrder = i.ModuleOrder
                                            ThemeScraperFound = True
                                        Next
                                        If Not ThemeScraperFound Then
                                            _externalScraperModule.ModuleOrder = 999
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                Catch ex As Exception
                End Try
            Next
            Dim c As Integer = 0
            For Each ext As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie.OrderBy(Function(x) x.ModuleOrder)    ' .Where(Function(x) x.ProcessorModule.ScraperEnabled)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Image_Movie In externalScrapersModules_Image_Movie.OrderBy(Function(x) x.ModuleOrder)    '.Where(Function(x) x.ProcessorModule.PostScraperEnabled)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Theme_Movie In externalScrapersModules_Theme_Movie.OrderBy(Function(x) x.ModuleOrder)     '.Where(Function(x) x.ProcessorModule.PostScraperEnabled)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Trailer_Movie In externalScrapersModules_Trailer_Movie.OrderBy(Function(x) x.ModuleOrder)     '.Where(Function(x) x.ProcessorModule.PostScraperEnabled)
                ext.ModuleOrder = c
                c += 1
            Next
            If Not DataScraperAnyEnabled AndAlso Not DataScraperFound Then
                SetScraperEnable_Data_Movie("scraper.Data.TMDB.ScraperModule.TMDB_Data", True)
            End If
            If Not ImageScraperAnyEnabled AndAlso Not PosterScraperFound Then
                SetScraperEnable_Image_Movie("scraper.Image.TMDB.ScraperModule.TMDB_Image", True)
            End If
            If Not ThemeScraperAnyEnabled AndAlso Not ThemeScraperFound Then
                SetScraperEnable_Theme_Movie("scraper.Theme.GoEar.ScraperModule.GoEar_Theme", True)
            End If
            If Not TrailerScraperAnyEnabled AndAlso Not TrailerScraperFound Then
                SetScraperEnable_Trailer_Movie("scraper.Trailer.TMDB.ScraperModule.TMDB_Trailer", True)
            End If
        End If
        logger.Trace("loadMovieScrapersModules finished")
    End Sub

    ''' <summary>
    ''' Load all Scraper Modules and field in externalScrapersModules List
    ''' </summary>
    Public Sub loadMovieSetScrapersModules(Optional ByVal modulefile As String = "*.dll")
        logger.Trace("loadMovieSetScrapersModules started")
        Dim DataScraperAnyEnabled As Boolean = False
        Dim DataScraperFound As Boolean = False
        Dim PosterScraperAnyEnabled As Boolean = False
        Dim PosterScraperFound As Boolean = False

        If Directory.Exists(moduleLocation) Then
            'Assembly to load the file
            Dim assembly As System.Reflection.Assembly
            'For each .dll file in the module directory
            For Each file As String In System.IO.Directory.GetFiles(moduleLocation, modulefile)
                Try
                    assembly = System.Reflection.Assembly.LoadFile(file)
                    'Loop through each of the assemeblies type
                    For Each fileType As Type In assembly.GetTypes

                        'Activate the located module
                        Dim t1 As Type = fileType.GetInterface("ScraperModule_Data_MovieSet")
                        If Not t1 Is Nothing Then
                            Dim ProcessorModule As Interfaces.ScraperModule_Data_MovieSet
                            ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Data_MovieSet)
                            'Add the activated module to the arraylist
                            Dim _externalScraperModule As New _externalScraperModuleClass_Data_MovieSet
                            Dim filename As String = file
                            If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                AssemblyList.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                            End If
                            _externalScraperModule.ProcessorModule = ProcessorModule
                            _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                            _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                            externalScrapersModules_Data_MovieSet.Add(_externalScraperModule)
                            _externalScraperModule.ProcessorModule.Init(_externalScraperModule.AssemblyName)
                            For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(x) x.AssemblyName = _externalScraperModule.AssemblyName AndAlso _
                                                                                                          x.ContentType = Enums.Content_Type.MovieSet)
                                _externalScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                                DataScraperAnyEnabled = DataScraperAnyEnabled OrElse i.ModuleEnabled
                                _externalScraperModule.ModuleOrder = i.ModuleOrder
                                DataScraperFound = True
                            Next
                            If Not DataScraperFound Then
                                _externalScraperModule.ModuleOrder = 999
                            End If
                            'Else
                            '    Dim t2 As Type = fileType.GetInterface("EmberMovieSetScraperModule_Poster")
                            '    If Not t2 Is Nothing Then
                            '        Dim ProcessorModule As Interfaces.EmberMovieScraperModule_Poster
                            '        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.EmberMovieScraperModule_Poster)
                            '        'Add the activated module to the arraylist
                            '        Dim _externalScraperModule As New _externalMovieScraperModuleClass_Poster
                            '        Dim filename As String = file
                            '        If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                            '            AssemblyList.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                            '        End If
                            '        _externalScraperModule.ProcessorModule = ProcessorModule
                            '        _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                            '        _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                            '        externalMoviePosterScrapersModules.Add(_externalScraperModule)
                            '        _externalScraperModule.ProcessorModule.Init(_externalScraperModule.AssemblyName)
                            '        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(x) x.AssemblyName = _externalScraperModule.AssemblyName AndAlso _
                            '                                                                              x.ModuleType = Enums.Module_Type.MovieSet)
                            '            _externalScraperModule.ProcessorModule.ScraperEnabled = i.ScraperEnabled
                            '            PosterScraperAnyEnabled = PosterScraperAnyEnabled OrElse i.ScraperEnabled
                            '            _externalScraperModule.ScraperOrder = i.ScraperOrder
                            '            PosterScraperFound = True
                            '        Next
                            '        If Not PosterScraperFound Then
                            '            _externalScraperModule.ScraperOrder = 999
                            '        End If
                            'End If
                        End If
                    Next
                Catch ex As Exception
                End Try
            Next
            Dim c As Integer = 0
            For Each ext As _externalScraperModuleClass_Data_MovieSet In externalScrapersModules_Data_MovieSet.OrderBy(Function(x) x.ModuleOrder)    ' .Where(Function(x) x.ProcessorModule.ScraperEnabled)
                ext.ModuleOrder = c
                c += 1
            Next
            'c = 0
            'For Each ext As _externalMovieScraperModuleClass_Poster In externalMoviePosterScrapersModules.OrderBy(Function(x) x.ScraperOrder)    '.Where(Function(x) x.ProcessorModule.PostScraperEnabled)
            '    ext.ScraperOrder = c
            '    c += 1
            'Next
            If Not DataScraperAnyEnabled AndAlso Not DataScraperFound Then
                SetScraperEnable_Data_MovieSet("scraper.Data.TMDB.ScraperModule.TMDB_Data", True)
            End If
            'If Not PosterScraperAnyEnabled AndAlso Not PosterScraperFound Then
            '    SetMoviePosterScraperEnable("scraper.TMDB.Poster.EmberMovieScraperModule.TMDB_Poster", True)
            'End If
        End If
        logger.Trace("loadMovieScrapersModules finished")
    End Sub

    Public Sub loadTVScrapersModules()
        logger.Trace("loadTVScrapersModules started")
        Dim ScraperAnyEnabled As Boolean = False
        Dim PostScraperAnyEnabled As Boolean = False
        Dim TVScraperFound As Boolean = False
        Dim TVThemeScraperAnyEnabled As Boolean = False
        Dim TVThemeScraperFound As Boolean = False
        If Directory.Exists(moduleLocation) Then
            'Assembly to load the file
            Dim assembly As System.Reflection.Assembly
            'For each .dll file in the module directory
            For Each file As String In System.IO.Directory.GetFiles(moduleLocation, "*.dll")
                Try
                    assembly = System.Reflection.Assembly.LoadFile(file)
                    'Loop through each of the assemeblies type

                    For Each fileType As Type In assembly.GetTypes

                        'Activate the located module
                        Dim t1 As Type = fileType.GetInterface("ScraperModule_TV")
                        If Not t1 Is Nothing Then
                            Dim ProcessorModule As Interfaces.ScraperModule_TV
                            ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_TV)
                            'Add the activated module to the arraylist
                            Dim _externalTVScraperModule As New _externalScraperModuleClass_TV
                            Dim filename As String = file
                            If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                AssemblyList.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                            End If

                            _externalTVScraperModule.ProcessorModule = ProcessorModule
                            _externalTVScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                            _externalTVScraperModule.AssemblyFileName = Path.GetFileName(file)
                            externalScrapersModules_TV.Add(_externalTVScraperModule)
                            _externalTVScraperModule.ProcessorModule.Init(_externalTVScraperModule.AssemblyName)
                            For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(x) x.AssemblyName = _externalTVScraperModule.AssemblyName)
                                _externalTVScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                                ScraperAnyEnabled = ScraperAnyEnabled OrElse i.ModuleEnabled
                                _externalTVScraperModule.ProcessorModule.PosterScraperEnabled = i.PostScraperEnabled
                                PostScraperAnyEnabled = PostScraperAnyEnabled OrElse i.PostScraperEnabled
                                _externalTVScraperModule.ModuleOrder = i.ModuleOrder
                                _externalTVScraperModule.PostScraperOrder = i.PostScraperOrder
                                TVScraperFound = True
                            Next
                            If Not TVScraperFound Then
                                _externalTVScraperModule.ModuleOrder = 999
                                _externalTVScraperModule.PostScraperOrder = 999
                            End If
                            AddHandler _externalTVScraperModule.ProcessorModule.TVScraperEvent, AddressOf Handler_TVScraperEvent
                        Else
                            Dim t2 As Type = fileType.GetInterface("ScraperModule_Theme_TV")
                            If Not t2 Is Nothing Then
                                Dim ProcessorModule As Interfaces.ScraperModule_Theme_TV
                                ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Theme_TV)
                                'Add the activated module to the arraylist
                                Dim _externalTVScraperModule As New _externalScraperModuleClass_Theme_TV
                                Dim filename As String = file
                                If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                    AssemblyList.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                                End If

                                _externalTVScraperModule.ProcessorModule = ProcessorModule
                                _externalTVScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                                _externalTVScraperModule.AssemblyFileName = Path.GetFileName(file)
                                externalScrapersModules_Theme_TV.Add(_externalTVScraperModule)
                                _externalTVScraperModule.ProcessorModule.Init(_externalTVScraperModule.AssemblyName)
                                For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(x) x.AssemblyName = _externalTVScraperModule.AssemblyName)
                                    _externalTVScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                                    TVThemeScraperAnyEnabled = TVThemeScraperAnyEnabled OrElse i.ModuleEnabled
                                    _externalTVScraperModule.ModuleOrder = i.ModuleOrder
                                    TVThemeScraperFound = True
                                Next
                                If Not TVThemeScraperFound Then
                                    _externalTVScraperModule.ModuleOrder = 999
                                End If
                            End If
                        End If
                    Next
                Catch ex As Exception
                End Try
            Next
            Dim c As Integer = 0
            For Each ext As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(x) x.ProcessorModule.ScraperEnabled)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(x) x.ProcessorModule.PosterScraperEnabled)
                ext.PostScraperOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Theme_TV In externalScrapersModules_Theme_TV.OrderBy(Function(x) x.ModuleOrder)     '.Where(Function(x) x.ProcessorModule.PostScraperEnabled)
                ext.ModuleOrder = c
                c += 1
            Next
            If Not ScraperAnyEnabled AndAlso Not TVScraperFound Then
                SetTVScraperEnable("scraper.TVDB.EmberTVScraperModule.TVDB_Data_Poster", True)
            End If
            If Not PostScraperAnyEnabled AndAlso Not TVScraperFound Then
                SetTVPosterScraperEnable("scraper.TVDB.EmberTVScraperModule.TVDB_Data_Poster", True)
            End If
            If Not TVThemeScraperAnyEnabled AndAlso Not TVThemeScraperFound Then
                SetTVThemeScraperEnable("scraper.TelevisionTunes.Theme.EmberTVScraperModule.TelevisionTunes_Theme", True)
            End If
        End If
        logger.Trace("loadTVScrapersModules finished")
    End Sub

    'Public Function MoviePostScrapeOnly(ByRef DBMovie As Structures.DBMovie, ByVal ScrapeType As Enums.ScraperCapabilities) As Interfaces.ModuleResult
    '    Dim ret As Interfaces.ModuleResult
    '    For Each _externalScraperModule As _externalScraperModuleClass_Poster In externalPosterScrapersModules.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ScraperOrder)
    '        AddHandler _externalScraperModule.ProcessorModule.MovieScraperEvent, AddressOf Handler_MovieScraperEvent
    '        Try
    '            ret = _externalScraperModule.ProcessorModule.Scraper(DBMovie, ScrapeType)
    '        Catch ex As Exception
    '        End Try
    '        RemoveHandler _externalScraperModule.ProcessorModule.MovieScraperEvent, AddressOf Handler_MovieScraperEvent
    '        If ret.breakChain Then Exit For
    '    Next
    '    Return ret
    'End Function

    'Function ScraperSelectImageOfType(ByRef DBMovie As Structures.DBMovie, ByVal _DLType As Enums.MovieImageType, ByRef pResults As Containers.ImgResult, Optional ByVal _isEdit As Boolean = False, Optional ByVal preload As Boolean = False) As Boolean
    '    Dim ret As Interfaces.ModuleResult
    '    For Each _externalScraperModule As _externalScraperModuleClass_Poster In externalPosterScrapersModules.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ScraperOrder)
    '        Try
    '            'ret = _externalScraperModule.ProcessorModule.SelectImageOfType(DBMovie, _DLType, pResults, _isEdit, preload)
    '        Catch ex As Exception
    '        End Try
    '        If ret.breakChain Then Exit For
    '    Next
    '    Return ret.Cancelled
    'End Function

    ''' <summary>
    ''' Request that enabled movie scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="ScrapeType">What kind of scrape is being requested, such as whether user-validation is desired</param>
    ''' <param name="Options">What kind of data is being requested from the scrape</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    Public Function MovieScrapeOnly(ByRef DBMovie As Structures.DBMovie, ByVal ScrapeType As Enums.ScrapeType, ByVal Options As Structures.MovieScrapeOptions) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_Movie) = externalScrapersModules_Data_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult

        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("No movie scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In modules
                logger.Trace("Scraping movie data using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_MovieScraperEvent
                Try
                    ret = _externalScraperModule.ProcessorModule.Scraper(DBMovie, ScrapeType, Options)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Error scraping movie data using <" & _externalScraperModule.ProcessorModule.ModuleName & ">", ex)
                End Try
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_MovieScraperEvent
                If ret.breakChain Then Exit For
            Next
        End If

        Return ret.Cancelled
    End Function
    ''' <summary>
    ''' Request that enabled movie image scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="Type">What kind of image is being scraped (poster, fanart, etc)</param>
    ''' <param name="ImageList">List of images that the scraper should add to</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_Movie) = externalScrapersModules_Image_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult
        Dim aList As List(Of MediaContainers.Image)

        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("No movie image scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In modules
                logger.Trace("Scraping movie images using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                If _externalScraperModule.ProcessorModule.QueryScraperCapabilities(Type) Then
                    AddHandler _externalScraperModule.ProcessorModule.MovieScraperEvent, AddressOf Handler_MovieScraperEvent
                    Try
                        aList = New List(Of MediaContainers.Image)
                        ret = _externalScraperModule.ProcessorModule.Scraper(DBMovie, Type, aList)
                        If Not IsNothing(aList) AndAlso aList.Count > 0 Then
                            For Each aIm In aList
                                ImageList.Add(aIm)
                            Next
                        End If
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name & "Error scraping movie images using <" & _externalScraperModule.ProcessorModule.ModuleName & ">", ex)
                    End Try
                    RemoveHandler _externalScraperModule.ProcessorModule.MovieScraperEvent, AddressOf Handler_MovieScraperEvent
                    If ret.breakChain Then Exit For
                End If
            Next
        End If
        Return ret.Cancelled
    End Function

    ''' <summary>
    ''' Request that enabled movie scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBMovieSet">MovieSet to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="ScrapeType">What kind of scrape is being requested, such as whether user-validation is desired</param>
    ''' <param name="Options">What kind of data is being requested from the scrape</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie set scrapers are enabled, a silent warning is generated.</remarks>
    Public Function MovieSetScrapeOnly(ByRef DBMovieSet As Structures.DBMovieSet, ByVal ScrapeType As Enums.ScrapeType, ByVal Options As Structures.MovieSetScrapeOptions) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_MovieSet) = externalScrapersModules_Data_MovieSet.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult

        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("No movie scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Data_MovieSet In modules
                logger.Trace("Scraping movie set data using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_MovieSetScraperEvent
                Try
                    ret = _externalScraperModule.ProcessorModule.Scraper(DBMovieSet, ScrapeType, Options)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Error scraping movie set data using <" & _externalScraperModule.ProcessorModule.ModuleName & ">", ex)
                End Try
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_MovieSetScraperEvent
                If ret.breakChain Then Exit For
            Next
        End If

        Return ret.Cancelled
    End Function
    ''' <summary>
    ''' Request that enabled movieset image scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBMovieSet">Movieset to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="Type">What kind of image is being scraped (poster, fanart, etc)</param>
    ''' <param name="ImageList">List of images that the scraper should add to</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    Public Function MovieSetScrapeImages(ByRef DBMovieSet As Structures.DBMovieSet, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_Movie) = externalScrapersModules_Image_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult
        Dim aList As List(Of MediaContainers.Image)

        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn(New StackFrame().GetMethod().Name, "No movie image scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In modules
                logger.Trace(New StackFrame().GetMethod().Name, "Scraping movie images using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                If _externalScraperModule.ProcessorModule.QueryScraperCapabilities(Type) Then
                    AddHandler _externalScraperModule.ProcessorModule.MovieScraperEvent, AddressOf Handler_MovieScraperEvent
                    Try
                        aList = New List(Of MediaContainers.Image)
                        ret = _externalScraperModule.ProcessorModule.Scraper(DBMovieSet, Type, aList)
                        If Not IsNothing(aList) AndAlso aList.Count > 0 Then
                            For Each aIm In aList
                                ImageList.Add(aIm)
                            Next
                        End If
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name & "Error scraping movie images using <" & _externalScraperModule.ProcessorModule.ModuleName & ">", ex)
                    End Try
                    RemoveHandler _externalScraperModule.ProcessorModule.MovieScraperEvent, AddressOf Handler_MovieScraperEvent
                    If ret.breakChain Then Exit For
                End If
            Next
        End If
        Return ret.Cancelled
    End Function
    ''' <summary>
    ''' Request that enabled movie theme scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="URLList">List of Themes objects that the scraper will append to. Note that only the URL is returned, 
    ''' not the full content of the trailer</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks></remarks>
    Public Function MovieScrapeTheme(ByRef DBMovie As Structures.DBMovie, ByRef URLList As List(Of Themes)) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Theme_Movie) = externalScrapersModules_Theme_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult
        Dim aList As List(Of Themes)

        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("No movie theme scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Theme_Movie In modules
                logger.Trace("Scraping movie themes using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                AddHandler _externalScraperModule.ProcessorModule.MovieScraperEvent, AddressOf Handler_MovieScraperEvent
                Try
                    aList = New List(Of Themes)
                    ret = _externalScraperModule.ProcessorModule.Scraper(DBMovie, aList)
                    If Not IsNothing(aList) AndAlso aList.Count > 0 Then
                        For Each aIm In aList
                            URLList.Add(aIm)
                        Next
                    End If
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Error scraping movie themes using <" & _externalScraperModule.ProcessorModule.ModuleName & ">", ex)
                End Try
                RemoveHandler _externalScraperModule.ProcessorModule.MovieScraperEvent, AddressOf Handler_MovieScraperEvent
                If ret.breakChain Then Exit For
            Next
        End If
        Return ret.Cancelled
    End Function
    ''' <summary>
    ''' Request that enabled movie trailer scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="Type">NOT ACTUALLY USED!</param>
    ''' <param name="URLList">List of Trailer objects that the scraper will append to. Note that only the URL is returned, 
    ''' not the full content of the trailer</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks></remarks>
    Public Function MovieScrapeTrailer(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef URLList As List(Of Trailers)) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Trailer_Movie) = externalScrapersModules_Trailer_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult
        Dim aList As List(Of Trailers)

        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("No movie trailer scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In modules
                logger.Trace("Scraping movie trailers using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                AddHandler _externalScraperModule.ProcessorModule.MovieScraperEvent, AddressOf Handler_MovieScraperEvent
                Try
                    aList = New List(Of Trailers)
                    ret = _externalScraperModule.ProcessorModule.Scraper(DBMovie, Type, aList)
                    If Not IsNothing(aList) AndAlso aList.Count > 0 Then
                        For Each aIm In aList
                            URLList.Add(aIm)
                        Next
                    End If
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Error scraping movie trailers using <" & _externalScraperModule.ProcessorModule.ModuleName & ">", ex)
                End Try
                RemoveHandler _externalScraperModule.ProcessorModule.MovieScraperEvent, AddressOf Handler_MovieScraperEvent
                If ret.breakChain Then Exit For
            Next
        End If
        Return ret.Cancelled
    End Function
    ''' <summary>
    ''' Calls all the generic modules of the supplied type (if one is defined), passing the supplied _params.
    ''' The module will do its task and return any expected results in the _refparams.
    ''' </summary>
    ''' <param name="mType">The <c>Enums.ModuleEventType</c> of module to execute.</param>
    ''' <param name="_params">Parameters to pass to the module</param>
    ''' <param name="_refparam"><c>Object</c> representing the module's result (if relevant)</param>
    ''' <param name="RunOnlyOne">If <c>True</c>, allow only one module to perform the required task.</param>
    ''' <returns></returns>
    ''' <remarks>Note that if any module returns a result of breakChain, no further modules are processed</remarks>
    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), Optional ByVal _refparam As Object = Nothing, Optional ByVal RunOnlyOne As Boolean = False) As Boolean
        Dim ret As Interfaces.ModuleResult

        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While

        Try
            Dim modules As IEnumerable(Of _externalGenericModuleClass) = externalProcessorModules.Where(Function(e) e.ProcessorModule.ModuleType.Contains(mType) AndAlso e.ProcessorModule.Enabled)
            If (modules.Count() <= 0) Then
                logger.Warn("No generic modules defined <{0}>", mType.ToString)
            Else
                For Each _externalGenericModule As _externalGenericModuleClass In modules
                    Try
                        logger.Trace("Run generic module <{0}>", _externalGenericModule.ProcessorModule.ModuleName)
                        ret = _externalGenericModule.ProcessorModule.RunGeneric(mType, _params, _refparam)
                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name & vbTab & "Error scraping movies images using <" & _externalGenericModule.ProcessorModule.ModuleName & ">", ex)
                    End Try
                    If ret.breakChain OrElse RunOnlyOne Then Exit For
                Next
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return ret.Cancelled
    End Function

    Public Sub SaveSettings()
        Dim tmpForXML As New List(Of _XMLEmberModuleClass)

        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While

        For Each _externalProcessorModule As _externalGenericModuleClass In externalProcessorModules
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalProcessorModule.AssemblyName
            t.AssemblyFileName = _externalProcessorModule.AssemblyFileName
            t.GenericEnabled = _externalProcessorModule.ProcessorModule.Enabled
            t.ContentType = _externalProcessorModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Data_MovieSet In externalScrapersModules_Data_MovieSet
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In externalScrapersModules_Image_Movie
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Theme_Movie In externalScrapersModules_Theme_Movie
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In externalScrapersModules_Trailer_Movie
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalTVScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalTVScraperModule.AssemblyName
            t.AssemblyFileName = _externalTVScraperModule.AssemblyFileName
            t.PostScraperEnabled = _externalTVScraperModule.ProcessorModule.PosterScraperEnabled
            t.ModuleEnabled = _externalTVScraperModule.ProcessorModule.ScraperEnabled
            t.PostScraperOrder = _externalTVScraperModule.PostScraperOrder
            t.ModuleOrder = _externalTVScraperModule.ModuleOrder
            tmpForXML.Add(t)
        Next
        For Each _externalTVScraperModule As _externalScraperModuleClass_Theme_TV In externalScrapersModules_Theme_TV
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalTVScraperModule.AssemblyName
            t.AssemblyFileName = _externalTVScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalTVScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalTVScraperModule.ModuleOrder
            t.ContentType = _externalTVScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        Master.eSettings.EmberModules = tmpForXML
        Master.eSettings.Save()
    End Sub

    ''' <summary>
    ''' Sets the enabled flag of the module identified by <paramref name="ModuleAssembly"/> to the value of <paramref name="value"/>
    ''' </summary>
    ''' <param name="ModuleAssembly"><c>String</c> representing the assembly name of the module</param>
    ''' <param name="value"><c>Boolean</c> value to set the enabled flag to</param>
    ''' <remarks></remarks>
    Public Sub SetModuleEnable(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalGenericModuleClass) = externalProcessorModules.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalProcessorModule As _externalGenericModuleClass In modules
                Try
                    _externalProcessorModule.ProcessorModule.Enabled = value
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">", ex)
                End Try
            Next
        End If
    End Sub

    ''' <summary>
    ''' Sets the enabled flag of the module identified by <paramref name="ModuleAssembly"/> to the value of <paramref name="value"/>
    ''' </summary>
    ''' <param name="ModuleAssembly"><c>String</c> representing the assembly name of the module</param>
    ''' <param name="value"><c>Boolean</c> value to set the enabled flag to</param>
    ''' <remarks></remarks>

    Public Sub SetScraperEnable_Theme_Movie(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Theme_Movie) = externalScrapersModules_Theme_Movie.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Theme_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">", ex)
                End Try
            Next
        End If
    End Sub
    ''' <summary>
    ''' Sets the enabled flag of the module identified by <paramref name="ModuleAssembly"/> to the value of <paramref name="value"/>
    ''' </summary>
    ''' <param name="ModuleAssembly"><c>String</c> representing the assembly name of the module</param>
    ''' <param name="value"><c>Boolean</c> value to set the enabled flag to</param>
    ''' <remarks></remarks>

    Public Sub SetScraperEnable_Trailer_Movie(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Trailer_Movie) = externalScrapersModules_Trailer_Movie.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">", ex)
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Image_Movie(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_Movie) = externalScrapersModules_Image_Movie.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">", ex)
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Data_Movie(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_Movie) = externalScrapersModules_Data_Movie.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">", ex)
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Data_MovieSet(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_MovieSet) = externalScrapersModules_Data_MovieSet.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Data_MovieSet In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">", ex)
                End Try
            Next
        End If
    End Sub

    Public Sub SetTVPosterScraperEnable(ByVal ModuleAssembly As String, ByVal value As Boolean)
        For Each _externalScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(p) p.AssemblyName = ModuleAssembly)
            Try
                _externalScraperModule.ProcessorModule.PosterScraperEnabled = value
            Catch ex As Exception
            End Try
        Next
    End Sub

    Public Sub SetTVScraperEnable(ByVal ModuleAssembly As String, ByVal value As Boolean)
        For Each _externalScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(p) p.AssemblyName = ModuleAssembly)
            Try
                _externalScraperModule.ProcessorModule.ScraperEnabled = value
            Catch ex As Exception
            End Try
        Next
    End Sub

    Public Sub SetTVThemeScraperEnable(ByVal ModuleAssembly As String, ByVal value As Boolean)
        For Each _externalScraperModule As _externalScraperModuleClass_Theme_TV In externalScrapersModules_Theme_TV.Where(Function(p) p.AssemblyName = ModuleAssembly)
            Try
                _externalScraperModule.ProcessorModule.ScraperEnabled = value
            Catch ex As Exception
            End Try
        Next
    End Sub

    Public Sub TVCancelAsync()
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externaltvScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(e) e.ProcessorModule.IsScraper AndAlso e.ProcessorModule.ScraperEnabled)
            Try
                _externaltvScraperModule.ProcessorModule.CancelAsync()
            Catch ex As Exception
            End Try
        Next
    End Sub

    Public Function TVGetLangs(ByVal sMirror As String) As clsXMLTVDBLanguages
        Dim ret As Interfaces.ModuleResult
        Dim Langs As New clsXMLTVDBLanguages
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externaltvScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(e) e.ProcessorModule.IsPostScraper AndAlso e.ProcessorModule.PosterScraperEnabled).OrderBy(Function(e) e.PostScraperOrder)
            Try
                ret = _externaltvScraperModule.ProcessorModule.GetLangs(sMirror, Langs)
            Catch ex As Exception
            End Try
            If ret.breakChain Then Exit For
        Next
        Return Langs
    End Function

    Public Function TVScrapeEpisode(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iEpisode As Integer, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As Boolean
        Dim ret As Interfaces.ModuleResult
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externaltvScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(e) e.ProcessorModule.IsScraper AndAlso e.ProcessorModule.ScraperEnabled)
            Try
                ret = _externaltvScraperModule.ProcessorModule.ScrapeEpisode(ShowID, ShowTitle, TVDBID, iEpisode, iSeason, Lang, Ordering, Options)
            Catch ex As Exception
            End Try
            If ret.breakChain Then Exit For
        Next
        Return ret.Cancelled
    End Function

    Public Function TVScrapeOnly(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal ShowLang As String, ByVal SourceLang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByVal ScrapeType As Enums.ScrapeType, ByVal WithCurrent As Boolean) As Boolean
        Dim ret As Interfaces.ModuleResult
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externaltvScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(e) e.ProcessorModule.IsScraper AndAlso e.ProcessorModule.ScraperEnabled)
            Try
                ret = _externaltvScraperModule.ProcessorModule.Scraper(ShowID, ShowTitle, TVDBID, ShowLang, SourceLang, Ordering, Options, ScrapeType, WithCurrent)
            Catch ex As Exception
            End Try
            If ret.breakChain Then Exit For
        Next
        Return ret.Cancelled
    End Function

    Public Function TVScrapeSeason(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As Boolean
        Dim ret As Interfaces.ModuleResult
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externaltvScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(e) e.ProcessorModule.IsScraper AndAlso e.ProcessorModule.ScraperEnabled)
            Try
                ret = _externaltvScraperModule.ProcessorModule.ScrapeSeason(ShowID, ShowTitle, TVDBID, iSeason, Lang, Ordering, Options)
            Catch ex As Exception
            End Try
            If ret.breakChain Then Exit For
        Next
        Return ret.Cancelled
    End Function

    Public Function TVSingleImageOnly(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As Enums.TVImageType, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal CurrentImage As Images) As Images
        Dim Image As New Images
        Dim ret As Interfaces.ModuleResult
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externaltvScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(e) e.ProcessorModule.IsScraper AndAlso e.ProcessorModule.ScraperEnabled)
            Try
                ret = _externaltvScraperModule.ProcessorModule.GetSingleImage(Title, ShowID, TVDBID, Type, Season, Episode, Lang, Ordering, CurrentImage, Image)
            Catch ex As Exception
            End Try
            If ret.breakChain Then Exit For
        Next
        Return Image
    End Function

    Private Sub BuildVersionList()
        VersionList.Clear()
        VersionList.Add(New VersionItem With {.AssemblyFileName = "*EmberAPP", .Name = "Ember Application", .Version = My.Application.Info.Version.ToString()})
        VersionList.Add(New VersionItem With {.AssemblyFileName = "*EmberAPI", .Name = "Ember API", .Version = Functions.EmberAPIVersion()})
        For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName, _
              .AssemblyFileName = _externalScraperModule.AssemblyFileName, _
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In externalScrapersModules_Image_Movie
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName, _
              .AssemblyFileName = _externalScraperModule.AssemblyFileName, _
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Theme_Movie In externalScrapersModules_Theme_Movie
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName, _
              .AssemblyFileName = _externalScraperModule.AssemblyFileName, _
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In externalScrapersModules_Trailer_Movie
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName, _
              .AssemblyFileName = _externalScraperModule.AssemblyFileName, _
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalTVScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV
            VersionList.Add(New VersionItem With {.Name = _externalTVScraperModule.ProcessorModule.ModuleName, _
                    .AssemblyFileName = _externalTVScraperModule.AssemblyFileName, _
                    .Version = _externalTVScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalTVThemeScraperModule As _externalScraperModuleClass_Theme_TV In externalScrapersModules_Theme_TV
            VersionList.Add(New VersionItem With {.Name = _externalTVThemeScraperModule.ProcessorModule.ModuleName, _
                    .AssemblyFileName = _externalTVThemeScraperModule.AssemblyFileName, _
                    .Version = _externalTVThemeScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalModule As _externalGenericModuleClass In externalProcessorModules
            VersionList.Add(New VersionItem With {.Name = _externalModule.ProcessorModule.ModuleName, _
              .AssemblyFileName = _externalModule.AssemblyFileName, _
              .Version = _externalModule.ProcessorModule.ModuleVersion})
        Next
    End Sub

    Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String) As MediaContainers.EpisodeDetails
        Dim ret As Interfaces.ModuleResult
        Dim epDetails As New MediaContainers.EpisodeDetails
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externaltvScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(e) e.ProcessorModule.IsPostScraper AndAlso e.ProcessorModule.PosterScraperEnabled).OrderBy(Function(e) e.PostScraperOrder)
            Try
                ret = _externaltvScraperModule.ProcessorModule.ChangeEpisode(ShowID, TVDBID, Lang, epDetails)
            Catch ex As Exception
            End Try
            If ret.breakChain Then Exit For
        Next
        Return epDetails
    End Function

    Private Sub GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        RaiseEvent GenericEvent(mType, _params)
    End Sub

    Function QueryPostScraperCapabilities(ByVal cap As Enums.ScraperCapabilities) As Boolean
        Dim ret As Boolean = False
        Dim sStudio As New List(Of String)
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In externalScrapersModules_Image_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = _externalScraperModule.ProcessorModule.QueryScraperCapabilities(cap) 'if a trailer scraper is enabled we can exit.
                If ret Then Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    Function QueryTrailerScraperCapabilities(ByVal cap As Enums.ScraperCapabilities) As Boolean
        Dim ret As Boolean = False
        Dim sStudio As New List(Of String)
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In externalScrapersModules_Trailer_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = True 'if a trailer scraper is enabled we can exit.
                Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie) As List(Of String)
        Dim ret As Interfaces.ModuleResult
        Dim sStudio As New List(Of String)
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = _externalScraperModule.ProcessorModule.GetMovieStudio(DBMovie, sStudio)
            Catch ex As Exception
            End Try
            If ret.breakChain Then Exit For
        Next
        Return sStudio
    End Function

    'Function ScraperDownloadTrailer(ByRef DBMovie As Structures.DBMovie) As String
    '    Dim ret As Interfaces.ModuleResult
    '    Dim sURL As String = String.Empty
    '    For Each _externalScraperModule As _externalScraperModuleClass_Trailer In externalTrailerScrapersModules.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ScraperOrder)
    '        Try
    '            ret = _externalScraperModule.ProcessorModule.DownloadTrailer(DBMovie, sURL)
    '        Catch ex As Exception
    '        End Try
    '        If ret.breakChain Then Exit For
    '    Next
    '    Return sURL
    'End Function

    Sub TVSaveImages()
        Dim ret As Interfaces.ModuleResult
        While Not (bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done)
            Application.DoEvents()
        End While
        For Each _externaltvScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(e) e.ProcessorModule.IsPostScraper AndAlso e.ProcessorModule.PosterScraperEnabled).OrderBy(Function(e) e.PostScraperOrder)
            Try
                ret = _externaltvScraperModule.ProcessorModule.SaveImages()
            Catch ex As Exception
            End Try
            If ret.breakChain Then Exit For
        Next
    End Sub

    Private Sub bwloadModules_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwloadModules.DoWork
        loadModules()
    End Sub

    Private Sub bwloadModules_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwloadModules.RunWorkerCompleted
        bwloadModules_done = True
        If bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done Then
            BuildVersionList()
        End If
    End Sub

    Private Sub bwloadMovieScrapersModules_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwloadMovieScrapersModules.DoWork
        loadMovieScrapersModules()
    End Sub

    Private Sub bwloadMovieScrapersModules_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwloadMovieScrapersModules.RunWorkerCompleted
        bwloadMovieScrapersModules_done = True
        If bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done Then
            BuildVersionList()
        End If
    End Sub

    Private Sub bwloadMovieSetScrapersModules_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwloadMovieSetScrapersModules.DoWork
        loadMovieSetScrapersModules()
    End Sub

    Private Sub bwloadMovieSetScrapersModules_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwloadMovieSetScrapersModules.RunWorkerCompleted
        bwloadMovieSetScrapersModules_done = True
        If bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done Then
            BuildVersionList()
        End If
    End Sub

    Private Sub bwloadTVScrapersModules_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwloadTVScrapersModules.DoWork
        loadTVScrapersModules()
    End Sub

    Private Sub bwloadTVScrapersModules_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwloadTVScrapersModules.RunWorkerCompleted
        bwloadTVScrapersModules_done = True
        If bwloadModules_done And bwloadMovieScrapersModules_done And bwloadMovieSetScrapersModules_done And bwloadTVScrapersModules_done Then
            BuildVersionList()
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure AssemblyListItem

#Region "Fields"

        Public Assembly As System.Reflection.Assembly
        Public AssemblyName As String

#End Region 'Fields

    End Structure

    Structure VersionItem

#Region "Fields"

        Public AssemblyFileName As String
        Public Name As String
        Public NeedUpdate As Boolean
        Public Version As String

#End Region 'Fields

    End Structure

    Class EmberRuntimeObjects

#Region "Fields"

        Private _LoadMedia As LoadMedia
        Private _MainTool As System.Windows.Forms.ToolStrip
        Private _MediaList As System.Windows.Forms.DataGridView
        Private _MenuMediaList As System.Windows.Forms.ContextMenuStrip
        Private _MenuTVShowList As System.Windows.Forms.ContextMenuStrip
        Private _OpenImageViewer As OpenImageViewer
        Private _TopMenu As System.Windows.Forms.MenuStrip
        Private _TrayMenu As System.Windows.Forms.ContextMenuStrip
        Private _MediaTabSelected As Integer = 0

#End Region 'Fields

#Region "Delegates"

        Delegate Sub LoadMedia(ByVal Scan As Structures.Scans, ByVal SourceName As String)

        'all runtime object including Function (delegate) that need to be exposed to Modules
        Delegate Sub OpenImageViewer(ByVal _Image As Image)

#End Region 'Delegates

#Region "Properties"
        Public Property MediaTabSelected() As Integer
            Get
                Return _MediaTabSelected
            End Get
            Set(ByVal value As Integer)
                _MediaTabSelected = value
            End Set
        End Property
        Public Property MainTool() As System.Windows.Forms.ToolStrip
            Get
                Return _MainTool
            End Get
            Set(ByVal value As System.Windows.Forms.ToolStrip)
                _MainTool = value
            End Set
        End Property

        Public Property MediaList() As System.Windows.Forms.DataGridView
            Get
                Return _MediaList
            End Get
            Set(ByVal value As System.Windows.Forms.DataGridView)
                _MediaList = value
            End Set
        End Property

        Public Property MenuMediaList() As System.Windows.Forms.ContextMenuStrip
            Get
                Return _MenuMediaList
            End Get
            Set(ByVal value As System.Windows.Forms.ContextMenuStrip)
                _MenuMediaList = value
            End Set
        End Property

        Public Property MenuTVShowList() As System.Windows.Forms.ContextMenuStrip
            Get
                Return _MenuTVShowList
            End Get
            Set(ByVal value As System.Windows.Forms.ContextMenuStrip)
                _MenuTVShowList = value
            End Set
        End Property

        Public Property TopMenu() As System.Windows.Forms.MenuStrip
            Get
                Return _TopMenu
            End Get
            Set(ByVal value As System.Windows.Forms.MenuStrip)
                _TopMenu = value
            End Set
        End Property

        Public Property TrayMenu() As System.Windows.Forms.ContextMenuStrip
            Get
                Return _TrayMenu
            End Get
            Set(ByVal value As System.Windows.Forms.ContextMenuStrip)
                _TrayMenu = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub DelegateLoadMedia(ByRef lm As LoadMedia)
            'Setup from EmberAPP
            _LoadMedia = lm
        End Sub

        Public Sub DelegateOpenImageViewer(ByRef IV As OpenImageViewer)
            _OpenImageViewer = IV
        End Sub

        Public Sub InvokeLoadMedia(ByVal Scan As Structures.Scans, ByVal SourceName As String)
            'Invoked from Modules
            _LoadMedia.Invoke(Scan, SourceName)
        End Sub

        Public Sub InvokeOpenImageViewer(ByRef _image As Image)
            _OpenImageViewer.Invoke(_image)
        End Sub

#End Region 'Methods

    End Class

    Class _externalGenericModuleClass

#Region "Fields"

        Public AssemblyFileName As String

        'Public Enabled As Boolean
        Public AssemblyName As String
        Public ModuleOrder As Integer 'TODO: not important at this point.. for 1.5
        Public ProcessorModule As Interfaces.GenericModule 'Object
        Public Type As List(Of Enums.ModuleEventType)
        Public ContentType As Enums.Content_Type = Enums.Content_Type.Generic

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Data_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Data_Movie 'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.Content_Type = Enums.Content_Type.Movie

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Image_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Image_Movie  'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.Content_Type = Enums.Content_Type.Movie

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Theme_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Theme_Movie     'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.Content_Type = Enums.Content_Type.Movie

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Trailer_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Trailer_Movie     'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.Content_Type = Enums.Content_Type.Movie

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Data_MovieSet

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Data_MovieSet 'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.Content_Type = Enums.Content_Type.MovieSet

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_TV

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public PostScraperOrder As Integer
        Public ProcessorModule As Interfaces.ScraperModule_TV 'Object
        Public ModuleOrder As Integer

#End Region 'Fields

    End Class

    '    Class _externalTVScraperModuleClass_Data

    '#Region "Fields"

    '        Public AssemblyFileName As String
    '        Public AssemblyName As String
    '        Public ProcessorModule As Interfaces.EmberTVScraperModule_Data 'Object
    '        Public ScraperOrder As Integer

    '#End Region 'Fields

    '    End Class

    '    Class _externalTVScraperModuleClass_Poster

    '#Region "Fields"

    '        Public AssemblyFileName As String
    '        Public AssemblyName As String
    '        Public ProcessorModule As Interfaces.EmberTVScraperModule_Poster  'Object
    '        Public ScraperOrder As Integer

    '#End Region 'Fields

    '    End Class

    Class _externalScraperModuleClass_Theme_TV

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Theme_TV  'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.Content_Type = Enums.Content_Type.TV

#End Region 'Fields

    End Class

    <XmlRoot("EmberModule")> _
    Class _XMLEmberModuleClass

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ContentType As Enums.Content_Type
        Public GenericEnabled As Boolean
        Public PostScraperEnabled As Boolean    'only for TV
        Public PostScraperOrder As Integer      'only for TV
        Public ModuleEnabled As Boolean
        Public ModuleOrder As Integer

#End Region 'Fields

    End Class

#End Region 'Nested Types

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class