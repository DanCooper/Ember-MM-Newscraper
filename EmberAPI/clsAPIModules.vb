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
    Public externalScrapersModules_Data_MovieSet As New List(Of _externalScraperModuleClass_Data_MovieSet)
    Public externalScrapersModules_Image_Movie As New List(Of _externalScraperModuleClass_Image_Movie)
    Public externalScrapersModules_Image_MovieSet As New List(Of _externalScraperModuleClass_Image_MovieSet)
    Public externalScrapersModules_Theme_Movie As New List(Of _externalScraperModuleClass_Theme_Movie)
    Public externalScrapersModules_Trailer_Movie As New List(Of _externalScraperModuleClass_Trailer_Movie)
    'Public externalTVDataScrapersModules As New List(Of _externalTVScraperModuleClass_Data)
    'Public externalTVPosterScrapersModules As New List(Of _externalTVScraperModuleClass_Poster)
    Public externalScrapersModules_TV As New List(Of _externalScraperModuleClass_TV)
    Public externalScrapersModules_Theme_TV As New List(Of _externalScraperModuleClass_Theme_TV)
    Public RuntimeObjects As New EmberRuntimeObjects

    'Singleton Instace for module manager .. allways use this one
    Private Shared Singleton As ModulesManager = Nothing

    Private moduleLocation As String = Path.Combine(Functions.AppPath, "Modules")

    Friend WithEvents bwloadGenericModules As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwloadScrapersModules_Movie As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwloadScrapersModules_MovieSet As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwloadScrapersModules_TV As New System.ComponentModel.BackgroundWorker

    Dim bwloadGenericModules_done As Boolean
    Dim bwloadScrapersModules_Movie_done As Boolean
    Dim bwloadScrapersModules_MovieSet_done As Boolean
    Dim bwloadScrapersModules_TV_done As Boolean

    Dim AssemblyList_Generic As New List(Of AssemblyListItem)
    Dim AssemblyList_Movie As New List(Of AssemblyListItem)
    Dim AssemblyList_MovieSet As New List(Of AssemblyListItem)
    Dim AssemblyList_TV As New List(Of AssemblyListItem)

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Event ScraperEvent_Movie(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object)

    Event ScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object)

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Event ScraperEvent_TV(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)

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
            Return bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done
        End Get

    End Property
#End Region 'Properties

#Region "Methods"

    Public Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As MediaContainers.EpisodeDetails
        Dim epDetails As New MediaContainers.EpisodeDetails

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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

    Public Function GetMovieTMDBID(ByRef sIMDBID As String) As String
        Dim TMDBID As String = String.Empty

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
            Application.DoEvents()
        End While

        If Not String.IsNullOrEmpty(sIMDBID) Then
            Dim ret As Interfaces.ModuleResult
            For Each _externalScraperModuleClass_Data As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie.Where(Function(e) e.ProcessorModule.ModuleName = "TMDB_Data")
                ret = _externalScraperModuleClass_Data.ProcessorModule.GetTMDBID(sIMDBID, TMDBID)
                If ret.breakChain Then Exit For
            Next
        End If
        Return TMDBID
    End Function

    Public Sub GetVersions()
        Dim dlgVersions As New dlgVersions
        Dim li As ListViewItem
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
            Application.DoEvents()
        End While
        For Each v As VersionItem In VersionList
            li = dlgVersions.lstVersions.Items.Add(v.Name)
            li.SubItems.Add(v.Version)
        Next
        dlgVersions.ShowDialog()
    End Sub

    Public Sub Handler_ScraperEvent_Movie(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object)
        RaiseEvent ScraperEvent_Movie(eType, Parameter)
    End Sub

    Public Sub Handler_ScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object)
        RaiseEvent ScraperEvent_MovieSet(eType, Parameter)
    End Sub

    Public Sub Handler_ScraperEvent_TV(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)
        RaiseEvent ScraperEvent_TV(eType, iProgress, Parameter)
    End Sub

    Public Sub LoadAllModules()
        'loadModules()
        'loadScrapersModules()
        'loadTVScrapersModules()
        'BuildVersionList()

        bwloadGenericModules_done = False
        bwloadScrapersModules_Movie_done = False
        bwloadScrapersModules_MovieSet_done = False
        bwloadScrapersModules_TV_done = False

        bwloadGenericModules.RunWorkerAsync()
        bwloadScrapersModules_Movie.RunWorkerAsync()
        bwloadScrapersModules_MovieSet.RunWorkerAsync()
        bwloadScrapersModules_TV.RunWorkerAsync()


        'Master.eLang.LoadAllLanguage(Master.eSettings.Language)
    End Sub

    ''' <summary>
    ''' Load all Generic Modules and field in externalProcessorModules List
    ''' </summary>
    Public Sub loadGenericModules(Optional ByVal modulefile As String = "*.dll")
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
                            Dim t As Type = fileType.GetInterface("GenericModule")
                            If Not t Is Nothing Then
                                Dim ProcessorModule As Interfaces.GenericModule 'Object
                                ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.GenericModule)
                                'Add the activated module to the arraylist
                                Dim _externalProcessorModule As New _externalGenericModuleClass
                                Dim filename As String = file
                                If String.IsNullOrEmpty(AssemblyList_Generic.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                    AssemblyList_Generic.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
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
                            logger.Error(New StackFrame().GetMethod().Name, ex)
                        End Try
                    Next
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
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
    Public Sub loadScrapersModules_Movie(Optional ByVal modulefile As String = "*.dll")
        logger.Trace("loadMovieScrapersModules started")
        Dim DataScraperAnyEnabled As Boolean = False
        Dim DataScraperFound As Boolean = False
        Dim ImageScraperAnyEnabled As Boolean = False
        Dim ImageScraperFound As Boolean = False
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
                            If String.IsNullOrEmpty(AssemblyList_Movie.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                AssemblyList_Movie.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                            End If
                            _externalScraperModule.ProcessorModule = ProcessorModule
                            _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                            _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                            externalScrapersModules_Data_Movie.Add(_externalScraperModule)
                            logger.Trace(String.Concat("Scraper Added: ", _externalScraperModule.AssemblyName, "_", _externalScraperModule.ContentType))
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
                                If String.IsNullOrEmpty(AssemblyList_Movie.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                    AssemblyList_Movie.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                                End If
                                _externalScraperModule.ProcessorModule = ProcessorModule
                                _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                                _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                                externalScrapersModules_Image_Movie.Add(_externalScraperModule)
                                logger.Trace(String.Concat("Scraper Added: ", _externalScraperModule.AssemblyName, "_", _externalScraperModule.ContentType))
                                _externalScraperModule.ProcessorModule.Init(_externalScraperModule.AssemblyName)
                                For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(x) x.AssemblyName = _externalScraperModule.AssemblyName AndAlso _
                                                                                                          x.ContentType = Enums.Content_Type.Movie)
                                    _externalScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                                    ImageScraperAnyEnabled = ImageScraperAnyEnabled OrElse i.ModuleEnabled
                                    _externalScraperModule.ModuleOrder = i.ModuleOrder
                                    ImageScraperFound = True
                                Next
                                If Not ImageScraperFound Then
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
                                    If String.IsNullOrEmpty(AssemblyList_Movie.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                        AssemblyList_Movie.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                                    End If
                                    _externalScraperModule.ProcessorModule = ProcessorModule
                                    _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                                    _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                                    externalScrapersModules_Trailer_Movie.Add(_externalScraperModule)
                                    logger.Trace(String.Concat("Scraper Added: ", _externalScraperModule.AssemblyName, "_", _externalScraperModule.ContentType))
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
                                        If String.IsNullOrEmpty(AssemblyList_Movie.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                            AssemblyList_Movie.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                                        End If
                                        _externalScraperModule.ProcessorModule = ProcessorModule
                                        _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                                        _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                                        externalScrapersModules_Theme_Movie.Add(_externalScraperModule)
                                        logger.Trace(String.Concat("Scraper Added: ", _externalScraperModule.AssemblyName, "_", _externalScraperModule.ContentType))
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
                    logger.Error(New StackFrame().GetMethod().Name, ex)
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
            If Not ImageScraperAnyEnabled AndAlso Not ImageScraperFound Then
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
    Public Sub loadScrapersModules_MovieSet(Optional ByVal modulefile As String = "*.dll")
        logger.Trace("loadMovieSetScrapersModules started")
        Dim DataScraperAnyEnabled As Boolean = False
        Dim DataScraperFound As Boolean = False
        Dim ImageScraperAnyEnabled As Boolean = False
        Dim ImageScraperFound As Boolean = False

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
                            If String.IsNullOrEmpty(AssemblyList_MovieSet.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                AssemblyList_MovieSet.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                            End If
                            _externalScraperModule.ProcessorModule = ProcessorModule
                            _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                            _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                            externalScrapersModules_Data_MovieSet.Add(_externalScraperModule)
                            logger.Trace(String.Concat("Scraper Added: ", _externalScraperModule.AssemblyName, "_", _externalScraperModule.ContentType))
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
                        Else
                            Dim t2 As Type = fileType.GetInterface("ScraperModule_Image_MovieSet")
                            If Not t2 Is Nothing Then
                                Dim ProcessorModule As Interfaces.ScraperModule_Image_MovieSet
                                ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Image_MovieSet)
                                'Add the activated module to the arraylist
                                Dim _externalScraperModule As New _externalScraperModuleClass_Image_MovieSet
                                Dim filename As String = file
                                If String.IsNullOrEmpty(AssemblyList_MovieSet.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                    AssemblyList_MovieSet.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
                                End If
                                _externalScraperModule.ProcessorModule = ProcessorModule
                                _externalScraperModule.AssemblyName = String.Concat(Path.GetFileNameWithoutExtension(file), ".", fileType.FullName)
                                _externalScraperModule.AssemblyFileName = Path.GetFileName(file)

                                externalScrapersModules_Image_MovieSet.Add(_externalScraperModule)
                                logger.Trace(String.Concat("Scraper Added: ", _externalScraperModule.AssemblyName, "_", _externalScraperModule.ContentType))
                                _externalScraperModule.ProcessorModule.Init(_externalScraperModule.AssemblyName)
                                For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(x) x.AssemblyName = _externalScraperModule.AssemblyName AndAlso _
                                                                                                          x.ContentType = Enums.Content_Type.MovieSet)
                                    _externalScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                                    ImageScraperAnyEnabled = ImageScraperAnyEnabled OrElse i.ModuleEnabled
                                    _externalScraperModule.ModuleOrder = i.ModuleOrder
                                    ImageScraperFound = True
                                Next
                                If Not ImageScraperFound Then
                                    _externalScraperModule.ModuleOrder = 999
                                End If
                            End If
                        End If
                    Next
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            Next
            Dim c As Integer = 0
            For Each ext As _externalScraperModuleClass_Data_MovieSet In externalScrapersModules_Data_MovieSet.OrderBy(Function(x) x.ModuleOrder)    ' .Where(Function(x) x.ProcessorModule.ScraperEnabled)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Image_MovieSet In externalScrapersModules_Image_MovieSet.OrderBy(Function(x) x.ModuleOrder)    '.Where(Function(x) x.ProcessorModule.PostScraperEnabled)
                ext.ModuleOrder = c
                c += 1
            Next
            If Not DataScraperAnyEnabled AndAlso Not DataScraperFound Then
                SetScraperEnable_Data_MovieSet("scraper.Data.TMDB.ScraperModule.TMDB_Data", True)
            End If
            If Not ImageScraperAnyEnabled AndAlso Not ImageScraperFound Then
                SetScraperEnable_Image_MovieSet("scraper.Image.TMDB.ScraperModule.TMDB_Image", True)
            End If
        End If
        logger.Trace("loadMovieScrapersModules finished")
    End Sub

    Public Sub loadScrapersModules_TV()
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
                            If String.IsNullOrEmpty(AssemblyList_TV.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                AssemblyList_TV.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
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
                            AddHandler _externalTVScraperModule.ProcessorModule.TVScraperEvent, AddressOf Handler_ScraperEvent_TV
                        Else
                            Dim t2 As Type = fileType.GetInterface("ScraperModule_Theme_TV")
                            If Not t2 Is Nothing Then
                                Dim ProcessorModule As Interfaces.ScraperModule_Theme_TV
                                ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Theme_TV)
                                'Add the activated module to the arraylist
                                Dim _externalTVScraperModule As New _externalScraperModuleClass_Theme_TV
                                Dim filename As String = file
                                If String.IsNullOrEmpty(AssemblyList_TV.FirstOrDefault(Function(x) x.AssemblyName = Path.GetFileNameWithoutExtension(filename)).AssemblyName) Then
                                    AssemblyList_TV.Add(New AssemblyListItem With {.AssemblyName = Path.GetFileNameWithoutExtension(filename), .Assembly = assembly})
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
                    logger.Error(New StackFrame().GetMethod().Name, ex)
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
                SetScraperEnable_Data_TV("scraper.TVDB.EmberTVScraperModule.TVDB_Data_Poster", True)
            End If
            If Not PostScraperAnyEnabled AndAlso Not TVScraperFound Then
                SetScraperEnable_Image_TV("scraper.TVDB.EmberTVScraperModule.TVDB_Data_Poster", True)
            End If
            If Not TVThemeScraperAnyEnabled AndAlso Not TVThemeScraperFound Then
                SetScraperEnable_Theme_TV("scraper.TelevisionTunes.Theme.EmberTVScraperModule.TelevisionTunes_Theme", True)
            End If
        End If
        logger.Trace("loadTVScrapersModules finished")
    End Sub

    ''' <summary>
    ''' Request that enabled movie scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped</param>
    ''' <param name="ScrapeType">What kind of scrape is being requested, such as whether user-validation is desired</param>
    ''' <param name="Options">What kind of data is being requested from the scrape</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    Public Function ScrapeData_Movie(ByRef DBMovie As Structures.DBMovie, ByVal ScrapeType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_Movie) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_Movie) = externalScrapersModules_Data_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult
        Dim oMovie As New Structures.DBMovie
        Dim ScrapedList As New List(Of MediaContainers.Movie)

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
            Application.DoEvents()
        End While

        'clean DBMovie if the movie is to be changed. For this, all existing (incorrect) information must be deleted and the images triggers set to remove.
        If (ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto) AndAlso Master.GlobalScrapeMod.DoSearch Then
            DBMovie.RemoveActorThumbs = True
            DBMovie.RemoveBanner = True
            DBMovie.RemoveClearArt = True
            DBMovie.RemoveClearLogo = True
            DBMovie.RemoveDiscArt = True
            DBMovie.RemoveEFanarts = True
            DBMovie.RemoveEThumbs = True
            DBMovie.RemoveFanart = True
            DBMovie.RemoveLandscape = True
            DBMovie.RemovePoster = True
            DBMovie.RemoveTheme = True
            DBMovie.RemoveTrailer = True
            DBMovie.BannerPath = String.Empty
            DBMovie.ClearArtPath = String.Empty
            DBMovie.ClearLogoPath = String.Empty
            DBMovie.DiscArtPath = String.Empty
            DBMovie.EFanartsPath = String.Empty
            DBMovie.EThumbsPath = String.Empty
            DBMovie.FanartPath = String.Empty
            DBMovie.LandscapePath = String.Empty
            DBMovie.NfoPath = String.Empty
            DBMovie.PosterPath = String.Empty
            DBMovie.SubPath = String.Empty
            DBMovie.ThemePath = String.Empty
            DBMovie.TrailerPath = String.Empty
            DBMovie.Movie.Clear()

            Dim tmpTitle As String = String.Empty
            If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
                tmpTitle = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name, False)
            ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
                tmpTitle = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name, False)
            Else
                tmpTitle = StringUtils.FilterName_Movie(If(DBMovie.IsSingle, Directory.GetParent(DBMovie.Filename).Name, Path.GetFileNameWithoutExtension(DBMovie.Filename)))
            End If

            Dim tmpYear As String = String.Empty
            If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
                tmpYear = StringUtils.GetYear(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name)
            ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
                tmpYear = StringUtils.GetYear(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name)
            Else
                If DBMovie.UseFolder AndAlso DBMovie.IsSingle Then
                    tmpYear = StringUtils.GetYear(Directory.GetParent(DBMovie.Filename).Name)
                Else
                    tmpYear = StringUtils.GetYear(Path.GetFileNameWithoutExtension(DBMovie.Filename))
                End If
            End If

            DBMovie.Movie.Title = tmpTitle
            DBMovie.Movie.Year = tmpYear
        End If

        'create a copy of DBMovie
        oMovie.Filename = DBMovie.Filename
        oMovie.Movie = New MediaContainers.Movie With {.Title = DBMovie.Movie.Title, .OriginalTitle = DBMovie.Movie.OriginalTitle, .Year = DBMovie.Movie.Year, _
                                                       .ID = DBMovie.Movie.ID, .IMDBID = DBMovie.Movie.IMDBID, .TMDBID = DBMovie.Movie.TMDBID}

        If (modules.Count() <= 0) Then
            logger.Warn("No movie scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In modules
                logger.Trace("Scraping movie data using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                Try
                    Dim nMovie As New MediaContainers.Movie
                    ret = _externalScraperModule.ProcessorModule.Scraper(oMovie, nMovie, ScrapeType, Options)

                    If ret.Cancelled Then Return ret.Cancelled

                    If Not IsNothing(nMovie) Then
                        ScrapedList.Add(nMovie)
                    End If

                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Error scraping movie data using <" & _externalScraperModule.ProcessorModule.ModuleName & ">", ex)
                End Try
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                If ret.breakChain Then Exit For
            Next

            'Merge scraperresults considering global datascraper settings
            DBMovie = NFO.MergeDataScraperResults(DBMovie, ScrapedList, ScrapeType, Options)
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
    Public Function ScrapeData_MovieSet(ByRef DBMovieSet As Structures.DBMovieSet, ByVal ScrapeType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_MovieSet) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_MovieSet) = externalScrapersModules_Data_MovieSet.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("No movie scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Data_MovieSet In modules
                logger.Trace("Scraping movie set data using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet
                Try
                    ret = _externalScraperModule.ProcessorModule.Scraper(DBMovieSet, ScrapeType, Options)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name & vbTab & "Error scraping movie set data using <" & _externalScraperModule.ProcessorModule.ModuleName & ">", ex)
                End Try
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet
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
    Public Function ScrapeImage_Movie(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_Movie) = externalScrapersModules_Image_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult
        Dim aList As List(Of MediaContainers.Image)

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("No movie image scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In modules
                logger.Trace("Scraping movie images using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                If _externalScraperModule.ProcessorModule.QueryScraperCapabilities(Type) Then
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
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
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                    If ret.breakChain Then Exit For
                End If
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
    Public Function ScrapeImage_MovieSet(ByRef DBMovieSet As Structures.DBMovieSet, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_MovieSet) = externalScrapersModules_Image_MovieSet.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult
        Dim aList As List(Of MediaContainers.Image)

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn(New StackFrame().GetMethod().Name, "No movie image scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Image_MovieSet In modules
                logger.Trace(New StackFrame().GetMethod().Name, "Scraping movieset images using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                If _externalScraperModule.ProcessorModule.QueryScraperCapabilities(Type) Then
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet
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
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet
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
    Public Function ScrapeTheme_Movie(ByRef DBMovie As Structures.DBMovie, ByRef URLList As List(Of Themes)) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Theme_Movie) = externalScrapersModules_Theme_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult
        Dim aList As List(Of Themes)

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("No movie theme scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Theme_Movie In modules
                logger.Trace("Scraping movie themes using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
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
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
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
    Public Function ScrapeTrailer_Movie(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef URLList As List(Of Trailers)) As Boolean
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Trailer_Movie) = externalScrapersModules_Trailer_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult
        Dim aList As List(Of Trailers)

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("No movie trailer scrapers are defined")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In modules
                logger.Trace("Scraping movie trailers using <{0}>", _externalScraperModule.ProcessorModule.ModuleName)
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
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
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
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
    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), Optional ByVal _refparam As Object = Nothing, Optional ByVal RunOnlyOne As Boolean = False, Optional ByRef DBMovie As Structures.DBMovie = Nothing, Optional ByRef DBTV As Structures.DBTV = Nothing) As Boolean
        Dim ret As Interfaces.ModuleResult

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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
                        ret = _externalGenericModule.ProcessorModule.RunGeneric(mType, _params, _refparam, DBMovie, DBTV)
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

        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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
        For Each _externalScraperModule As _externalScraperModuleClass_Image_MovieSet In externalScrapersModules_Image_MovieSet
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

    Public Sub SetScraperEnable_Image_MovieSet(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_MovieSet) = externalScrapersModules_Image_MovieSet.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Image_MovieSet In modules
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

    Public Sub SetScraperEnable_Image_TV(ByVal ModuleAssembly As String, ByVal value As Boolean)
        For Each _externalScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(p) p.AssemblyName = ModuleAssembly)
            Try
                _externalScraperModule.ProcessorModule.PosterScraperEnabled = value
            Catch ex As Exception
            End Try
        Next
    End Sub

    Public Sub SetScraperEnable_Data_TV(ByVal ModuleAssembly As String, ByVal value As Boolean)
        For Each _externalScraperModule As _externalScraperModuleClass_TV In externalScrapersModules_TV.Where(Function(p) p.AssemblyName = ModuleAssembly)
            Try
                _externalScraperModule.ProcessorModule.ScraperEnabled = value
            Catch ex As Exception
            End Try
        Next
    End Sub

    Public Sub SetScraperEnable_Theme_TV(ByVal ModuleAssembly As String, ByVal value As Boolean)
        For Each _externalScraperModule As _externalScraperModuleClass_Theme_TV In externalScrapersModules_Theme_TV.Where(Function(p) p.AssemblyName = ModuleAssembly)
            Try
                _externalScraperModule.ProcessorModule.ScraperEnabled = value
            Catch ex As Exception
            End Try
        Next
    End Sub

    Public Sub TVCancelAsync()
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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

    Private Sub CreateAssemblyList()
        For Each assembly As AssemblyListItem In AssemblyList_Generic
            If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = assembly.AssemblyName).AssemblyName) Then
                AssemblyList.Add(assembly)
            End If
        Next
        For Each assembly As AssemblyListItem In AssemblyList_Movie
            If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = assembly.AssemblyName).AssemblyName) Then
                AssemblyList.Add(assembly)
            End If
        Next
        For Each assembly As AssemblyListItem In AssemblyList_MovieSet
            If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = assembly.AssemblyName).AssemblyName) Then
                AssemblyList.Add(assembly)
            End If
        Next
        For Each assembly As AssemblyListItem In AssemblyList_TV
            If String.IsNullOrEmpty(AssemblyList.FirstOrDefault(Function(x) x.AssemblyName = assembly.AssemblyName).AssemblyName) Then
                AssemblyList.Add(assembly)
            End If
        Next
    End Sub

    Private Sub BuildVersionList()
        VersionList.Clear()
        VersionList.Add(New VersionItem With {.AssemblyFileName = "*EmberAPP", .Name = "Ember Application", .Version = My.Application.Info.Version.ToString()})
        VersionList.Add(New VersionItem With {.AssemblyFileName = "*EmberAPI", .Name = "Ember API", .Version = Functions.EmberAPIVersion()})
        For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName, _
              .AssemblyFileName = _externalScraperModule.AssemblyFileName, _
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Data_MovieSet In externalScrapersModules_Data_MovieSet
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName, _
              .AssemblyFileName = _externalScraperModule.AssemblyFileName, _
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In externalScrapersModules_Image_Movie
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName, _
              .AssemblyFileName = _externalScraperModule.AssemblyFileName, _
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Image_MovieSet In externalScrapersModules_Image_MovieSet
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
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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

    Function QueryScraperCapabilities_Image_Movie(ByVal cap As Enums.ScraperCapabilities) As Boolean
        Dim ret As Boolean = False
        Dim sStudio As New List(Of String)
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In externalScrapersModules_Image_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = _externalScraperModule.ProcessorModule.QueryScraperCapabilities(cap)
                If ret Then Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    Function QueryScraperCapabilities_Image_MovieSet(ByVal cap As Enums.ScraperCapabilities) As Boolean
        Dim ret As Boolean = False
        Dim sStudio As New List(Of String)
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Image_MovieSet In externalScrapersModules_Image_MovieSet.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = _externalScraperModule.ProcessorModule.QueryScraperCapabilities(cap)
                If ret Then Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    Function QueryScraperCapabilities_Trailer_Movie(ByVal cap As Enums.ScraperCapabilities) As Boolean
        Dim ret As Boolean = False
        Dim sStudio As New List(Of String)
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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
        While Not (bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done)
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

    Private Sub bwloadGenericModules_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwloadGenericModules.DoWork
        loadGenericModules()
    End Sub

    Private Sub bwloadGenericModules_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwloadGenericModules.RunWorkerCompleted
        bwloadGenericModules_done = True
        If bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done Then
            CreateAssemblyList()
            BuildVersionList()
        End If
    End Sub

    Private Sub bwloadScrapersModules_Movie_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwloadScrapersModules_Movie.DoWork
        loadScrapersModules_Movie()
    End Sub

    Private Sub bwloadScrapersModules_Movie_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwloadScrapersModules_Movie.RunWorkerCompleted
        bwloadScrapersModules_Movie_done = True
        If bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done Then
            CreateAssemblyList()
            BuildVersionList()
        End If
    End Sub

    Private Sub bwloadScrapersModules_MovieSet_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwloadScrapersModules_MovieSet.DoWork
        loadScrapersModules_MovieSet()
    End Sub

    Private Sub bwloadScrapersModules_MovieSet_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwloadScrapersModules_MovieSet.RunWorkerCompleted
        bwloadScrapersModules_MovieSet_done = True
        If bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done Then
            CreateAssemblyList()
            BuildVersionList()
        End If
    End Sub

    Private Sub bwloadScrapersModules_TV_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwloadScrapersModules_TV.DoWork
        loadScrapersModules_TV()
    End Sub

    Private Sub bwloadScrapersModules_TV_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwloadScrapersModules_TV.RunWorkerCompleted
        bwloadScrapersModules_TV_done = True
        If bwloadGenericModules_done AndAlso bwloadScrapersModules_Movie_done AndAlso bwloadScrapersModules_MovieSet_done AndAlso bwloadScrapersModules_TV_done Then
            CreateAssemblyList()
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
        Private _MediaListMovies As System.Windows.Forms.DataGridView
        Private _MediaListEpisodes As System.Windows.Forms.DataGridView
        Private _MediaListShows As System.Windows.Forms.DataGridView
        Private _MenuMovieList As System.Windows.Forms.ContextMenuStrip
        Private _MenuMovieSetList As System.Windows.Forms.ContextMenuStrip
        Private _MenuTVEpisodeList As System.Windows.Forms.ContextMenuStrip
        Private _MenuTVSeasonList As System.Windows.Forms.ContextMenuStrip
        Private _MenuTVShowList As System.Windows.Forms.ContextMenuStrip
        Private _OpenImageViewer As OpenImageViewer
        Private _TopMenu As System.Windows.Forms.MenuStrip
        Private _TrayMenu As System.Windows.Forms.ContextMenuStrip
        Private _MediaTabSelected As Integer = 0
        Private _FilterMovies As String
        Private _FilterMoviesSearch As String
        Private _FilterMoviesType As String
        Private _FilterShows As String
        Private _FilterShowsSearch As String
        Private _FilterShowsType As String


#End Region 'Fields

#Region "Delegates"

        Delegate Sub LoadMedia(ByVal Scan As Structures.Scans, ByVal SourceName As String)

        'all runtime object including Function (delegate) that need to be exposed to Modules
        Delegate Sub OpenImageViewer(ByVal _Image As Image)

#End Region 'Delegates

#Region "Properties"

        Public Property FilterMovies() As String
            Get
                Return _FilterMovies
            End Get
            Set(ByVal value As String)
                _FilterMovies = value
            End Set
        End Property

        Public Property FilterMoviesSearch() As String
            Get
                Return _FilterMoviesSearch
            End Get
            Set(ByVal value As String)
                _FilterMoviesSearch = value
            End Set
        End Property

        Public Property FilterMoviesType() As String
            Get
                Return _FilterMoviesType
            End Get
            Set(ByVal value As String)
                _FilterMoviesType = value
            End Set
        End Property
        Public Property FilterShows() As String
            Get
                Return _FilterShows
            End Get
            Set(ByVal value As String)
                _FilterShows = value
            End Set
        End Property

        Public Property FilterShowsSearch() As String
            Get
                Return _FilterShowsSearch
            End Get
            Set(ByVal value As String)
                _FilterShowsSearch = value
            End Set
        End Property

        Public Property FilterShowsType() As String
            Get
                Return _FilterShowsType
            End Get
            Set(ByVal value As String)
                _FilterShowsType = value
            End Set
        End Property

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

        Public Property MediaListEpisodes() As System.Windows.Forms.DataGridView
            Get
                Return _MediaListEpisodes
            End Get
            Set(ByVal value As System.Windows.Forms.DataGridView)
                _MediaListEpisodes = value
            End Set
        End Property

        Public Property MediaListMovies() As System.Windows.Forms.DataGridView
            Get
                Return _MediaListMovies
            End Get
            Set(ByVal value As System.Windows.Forms.DataGridView)
                _MediaListMovies = value
            End Set
        End Property

        Public Property MediaListShows() As System.Windows.Forms.DataGridView
            Get
                Return _MediaListShows
            End Get
            Set(ByVal value As System.Windows.Forms.DataGridView)
                _MediaListShows = value
            End Set
        End Property

        Public Property MenuMovieList() As System.Windows.Forms.ContextMenuStrip
            Get
                Return _MenuMovieList
            End Get
            Set(ByVal value As System.Windows.Forms.ContextMenuStrip)
                _MenuMovieList = value
            End Set
        End Property

        Public Property MenuMovieSetList() As System.Windows.Forms.ContextMenuStrip
            Get
                Return _MenuMovieSetList
            End Get
            Set(ByVal value As System.Windows.Forms.ContextMenuStrip)
                _MenuMovieSetList = value
            End Set
        End Property

        Public Property MenuTVEpisodeList() As System.Windows.Forms.ContextMenuStrip
            Get
                Return _MenuTVEpisodeList
            End Get
            Set(ByVal value As System.Windows.Forms.ContextMenuStrip)
                _MenuTVEpisodeList = value
            End Set
        End Property

        Public Property MenuTVSeasonList() As System.Windows.Forms.ContextMenuStrip
            Get
                Return _MenuTVSeasonList
            End Get
            Set(ByVal value As System.Windows.Forms.ContextMenuStrip)
                _MenuTVSeasonList = value
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

    Class _externalScraperModuleClass_Data_MovieSet

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Data_MovieSet 'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.Content_Type = Enums.Content_Type.MovieSet

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

    Class _externalScraperModuleClass_Image_MovieSet

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Image_MovieSet  'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.Content_Type = Enums.Content_Type.MovieSet

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