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

Imports System.Xml.Serialization
Imports System.IO
Imports EmberAPI
Imports NLog

Public Class genericMediaBrowser
    Implements Interfaces.GenericModule


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private fMediaBrowser As frmMediaBrowser
    Private _enabled As Boolean = False
    Private _name As String = "MediaBrowser Compatibility"
    Private _AssemblyName As String = String.Empty
#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object)) Implements Interfaces.GenericModule.GenericEvent

    Public Event ModuleSettingsChanged() Implements Interfaces.GenericModule.ModuleSettingsChanged

    Public Event ModuleSetupChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged

    Public Event SetupNeedsRestart() Implements Interfaces.GenericModule.SetupNeedsRestart


#End Region 'Events

#Region "Properties"

    Public Property Enabled() As Boolean Implements Interfaces.GenericModule.Enabled
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            If _enabled = value Then Return
            _enabled = value
            If _enabled Then
                'Enable()
            Else
                'Disable()
            End If
        End Set
    End Property

    Public ReadOnly Property ModuleName() As String Implements Interfaces.GenericModule.ModuleName
        Get
            Return "MediaBrowser Compatibility"
        End Get
    End Property

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.GenericModule.IsBusy
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ModuleType() As List(Of Enums.ModuleEventType) Implements Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Generic, Enums.ModuleEventType.OnFanartSave_Movie, Enums.ModuleEventType.OnNFOSave_Movie})
        End Get
    End Property

    Public ReadOnly Property ModuleVersion() As String Implements Interfaces.GenericModule.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String) Implements Interfaces.GenericModule.Init
        _AssemblyName = sAssemblyName
    End Sub

    Public Function InjectSetup() As EmberAPI.Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        fMediaBrowser = New frmMediaBrowser
        fMediaBrowser.chkEnabled.Checked = _enabled
        'Me.fMediaBrowser.chkVideoTSParent.Checked = Master.eSettings.VideoTSParent
        fMediaBrowser.chkMyMovies.Checked = clsAdvancedSettings.GetBooleanSetting("MediaBrowserMyMovie", False)
        fMediaBrowser.chkBackdrop.Checked = clsAdvancedSettings.GetBooleanSetting("MediaBrowserBackdrop", False)
        SPanel.Name = _name
        SPanel.Text = Master.eLang.GetString(599, "MediaBrowser Compatibility")
        SPanel.Prefix = "MediaBrowser_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(_enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = fMediaBrowser.pnlSettings
        AddHandler fMediaBrowser.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler fMediaBrowser.ModuleEnabledChanged, AddressOf Handle_SetupChanged
        AddHandler fMediaBrowser.GenericEvent, AddressOf DeploySyncSettings
        Return SPanel
        'Return Nothing
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupChanged(ByVal state As Boolean, ByVal difforder As Integer)
        RaiseEvent ModuleSetupChanged(_name, state, difforder)
    End Sub

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Enabled = fMediaBrowser.chkEnabled.Checked
        'Master.eSettings.VideoTSParent = Me.fMediaBrowser.chkVideoTSParent.Checked
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("MediaBrowserMyMovie", fMediaBrowser.chkMyMovies.Checked)
            settings.SetBooleanSetting("MediaBrowserBackdrop", fMediaBrowser.chkBackdrop.Checked)
        End Using
    End Sub

    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Dim doContinue As Boolean
        Dim mMovie As Database.DBElement
        Dim _image As Images
        If Enabled Then
            Try
                Select Case mType
                    Case Enums.ModuleEventType.OnNFOSave_Movie
                        If clsAdvancedSettings.GetBooleanSetting("MediaBrowserMyMovie", False) Then
                            mMovie = DirectCast(_params(0), Database.DBElement)
                            doContinue = DirectCast(_singleobjekt, Boolean)
                            XMLmymovies.SaveMovieDB(mMovie)
                            _singleobjekt = doContinue
                        End If
                    Case Enums.ModuleEventType.OnFanartSave_Movie
                        If clsAdvancedSettings.GetBooleanSetting("MediaBrowserBackdrop", False) Then
                            mMovie = DirectCast(_params(0), Database.DBElement)
                            _image = DirectCast(_singleobjekt, Images)
                            Dim fPath As String = Path.Combine(Path.GetDirectoryName(mMovie.Filename), "backdrop.jpg")
                            Dim eimage As New Images
                            eimage = _image
                            eimage.Save(fPath)
                        End If
                End Select

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End If
    End Function

    Protected Overrides Sub Finalize()
        RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf SyncSettings
        MyBase.Finalize()
    End Sub

    Public Sub New()
        AddHandler ModulesManager.Instance.GenericEvent, AddressOf SyncSettings
    End Sub

    Sub SyncSettings(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        If mType = Enums.ModuleEventType.SyncModuleSettings AndAlso fMediaBrowser IsNot Nothing Then
            RemoveHandler fMediaBrowser.GenericEvent, AddressOf DeploySyncSettings
            'Me.fMediaBrowser.chkVideoTSParent.Checked = Master.eSettings.VideoTSParent
            AddHandler fMediaBrowser.GenericEvent, AddressOf DeploySyncSettings
        End If
    End Sub

    Sub DeploySyncSettings(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        If fMediaBrowser IsNot Nothing Then
            'Master.eSettings.VideoTSParent = Me.fMediaBrowser.chkVideoTSParent.Checked
            RaiseEvent GenericEvent(mType, _params)
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"
    <XmlRoot("Title")>
    Public Class XMLmymovies
        Private _ID As String
        Private _CollectionNumber As String
        Private _Type As String
        Private _LocalTitle As String
        Private _OriginalTitle As String
        Private _SortTitle As String
        Private _ForcedTitle As String
        Private _IMDBrating As String
        Private _ProductionYear As String
        'Private _Revenue As String
        'Private _Budget As String
        Private _Added As String
        Private _IMDbId As String
        Private _RunningTime As String
        Private _TMDbId As String
        Private _Studios As New List(Of Studio)
        Private _CDUniverseId As String
        Private _Persons As New List(Of Person)
        Private _Genres As New List(Of Genre)
        Private _Description As String
        Private _AudioTracks As New List(Of AudioTrack)
        Private _Subtitles As New List(Of Subtitle)
        Private _MPAARating As String

        <XmlElement("ID")>
        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property IDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_ID)
            End Get
        End Property

        <XmlElement("CollectionNumber")>
        Public Property CollectionNumber() As String
            Get
                Return _CollectionNumber
            End Get
            Set(ByVal value As String)
                _CollectionNumber = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property CollectionNumberSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_CollectionNumber)
            End Get
        End Property

        <XmlElement("Type")>
        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                _Type = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TypeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_Type)
            End Get
        End Property

        <XmlElement("LocalTitle")>
        Public Property LocalTitle() As String
            Get
                Return _LocalTitle
            End Get
            Set(ByVal value As String)
                _LocalTitle = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property LocalTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_LocalTitle)
            End Get
        End Property

        <XmlElement("OriginalTitle")>
        Public Property OriginalTitle() As String
            Get
                Return _OriginalTitle
            End Get
            Set(ByVal value As String)
                _OriginalTitle = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property OriginalTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_OriginalTitle)
            End Get
        End Property

        <XmlElement("SortTitle")>
        Public Property SortTitle() As String
            Get
                Return _SortTitle
            End Get
            Set(ByVal value As String)
                _SortTitle = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SortTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_SortTitle)
            End Get
        End Property

        <XmlElement("ForcedTitle")>
        Public Property ForcedTitle() As String
            Get
                Return _ForcedTitle
            End Get
            Set(ByVal value As String)
                _ForcedTitle = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property ForcedTitleSpecified() As Boolean
            Get
                Return True 'Not String.IsNullOrEmpty(Me._ForcedTitle)
            End Get
        End Property

        <XmlElement("IMDBrating")>
        Public Property IMDBrating() As String
            Get
                Return _IMDBrating
            End Get
            Set(ByVal value As String)
                _IMDBrating = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property IMDBratingSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_IMDBrating)
            End Get
        End Property

        <XmlElement("MPAARating")>
        Public Property MPAARating() As String
            Get
                Return _MPAARating
            End Get
            Set(ByVal value As String)
                _MPAARating = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property MPAARatingSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_MPAARating)
            End Get
        End Property

        <XmlElement("ProductionYear")>
        Public Property ProductionYear() As String
            Get
                Return _ProductionYear
            End Get
            Set(ByVal value As String)
                _ProductionYear = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property ProductionYearSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_ProductionYear)
            End Get
        End Property

        <XmlElement("Added")>
        Public Property Added() As String
            Get
                Return _Added
            End Get
            Set(ByVal value As String)
                _Added = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property AddedSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_Added)
            End Get
        End Property

        <XmlElement("IMDB")>
        Public Property IMDB() As String
            Get
                Return _IMDbId
            End Get
            Set(ByVal value As String)
                _IMDbId = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property IMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_IMDbId)
            End Get
        End Property

        <XmlElement("IMDbId")>
        Public Property IMDbId() As String
            Get
                Return _IMDbId
            End Get
            Set(ByVal value As String)
                _IMDbId = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property _IMDbIdSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_IMDbId)
            End Get
        End Property

        <XmlElement("RunningTime")>
        Public Property RunningTime() As String
            Get
                Return _RunningTime
            End Get
            Set(ByVal value As String)
                _RunningTime = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property RunningTimeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_RunningTime)
            End Get
        End Property

        <XmlElement("TMDbId")>
        Public Property TMDbId() As String
            Get
                Return _TMDbId
            End Get
            Set(ByVal value As String)
                _TMDbId = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TMDbIdSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_TMDbId)
            End Get
        End Property

        <XmlArray("Studios")>
        <XmlArrayItem("Studio")>
        Public Property Studios() As List(Of Studio)
            Get
                Return _Studios
            End Get
            Set(ByVal value As List(Of Studio))
                _Studios = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property StudiosSpecified() As Boolean
            Get
                Return (_Studios.Count > 0)
            End Get
        End Property

        <XmlElement("CDUniverseId")>
        Public Property CDUniverseId() As String
            Get
                Return _CDUniverseId
            End Get
            Set(ByVal value As String)
                _CDUniverseId = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property CDUniverseIdSpecified() As Boolean
            Get
                Return True 'Not String.IsNullOrEmpty(Me._CDUniverseId)
            End Get
        End Property

        <XmlArray("Persons")>
        <XmlArrayItem("Person")>
        Public Property Persons() As List(Of Person)
            Get
                Return _Persons
            End Get
            Set(ByVal value As List(Of Person))
                _Persons = value
            End Set
        End Property

        <XmlAttribute("ActorsComplete")>
        Public ActorsComplete As String
        <XmlIgnore()>
        Public ReadOnly Property PersonsSpecified() As Boolean
            Get
                Return (_Persons.Count > 0)
            End Get
        End Property

        <XmlArray("Genres")>
        <XmlArrayItem("Genre")>
        Public Property Genres() As List(Of Genre)
            Get
                Return _Genres
            End Get
            Set(ByVal value As List(Of Genre))
                _Genres = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property GenresSpecified() As Boolean
            Get
                Return (_Genres.Count > 0)
            End Get
        End Property

        <XmlElement("Description")>
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property DescriptionSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_Description)
            End Get
        End Property

        <XmlArray("AudioTracks")>
        <XmlArrayItem("AudioTrack")>
        Public Property AudioTracks() As List(Of AudioTrack)
            Get
                Return _AudioTracks
            End Get
            Set(ByVal value As List(Of AudioTrack))
                _AudioTracks = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property AudioTracksSpecified() As Boolean
            Get
                Return (_AudioTracks.Count > 0)
            End Get
        End Property

        <XmlArray("Subtitles")>
        <XmlArrayItem("Subtitle")>
        Public Property Subtitles() As List(Of Subtitle)
            Get
                Return _Subtitles
            End Get
            Set(ByVal value As List(Of Subtitle))
                _Subtitles = value
            End Set

        End Property

        <XmlAttribute("NotPresent")>
        Public SubsNotPresent As String
        <XmlIgnore()>
        Public ReadOnly Property SubtitlesSpecified() As Boolean
            Get
                Return (_Subtitles.Count > 0)
            End Get
        End Property

        Public Class Studio
            <XmlText()> _
            Public Studio As String
        End Class

        Public Class Person
            '<XmlAttribute("Type")> _
            'Public _type As String
            Public Name As String
            Public Type As String
            Public Role As String
        End Class

        Public Class Genre
            <XmlText()> _
            Public Genre As String
        End Class

        Public Class AudioTrack
            <XmlAttribute("Language")> _
            Public Language As String
            <XmlAttribute("Type")> _
            Public Type As String
            <XmlAttribute("Channels")> _
            Public Channels As String
        End Class

        Public Class Subtitle
            <XmlAttribute("Language")> _
            Public Language As String
        End Class

        Public Shared Function GetFromMovieDB(ByVal movie As Database.DBElement) As XMLmymovies
            Dim myself As New XMLmymovies
            myself.ForcedTitle = String.Empty
            myself.CDUniverseId = String.Empty
            myself.ID = movie.ID.ToString
            myself.LocalTitle = movie.Movie.Title
            myself.OriginalTitle = movie.Movie.OriginalTitle
            myself.SortTitle = movie.Movie.SortTitle
            myself.IMDbId = movie.Movie.ID
            myself.RunningTime = movie.Movie.Runtime
            myself.Description = movie.Movie.Plot
            myself.IMDBrating = movie.Movie.Rating
            myself.ProductionYear = movie.Movie.Year
            myself.MPAARating = movie.Movie.MPAA
            For Each g As String In movie.Movie.Genre.Split(Convert.ToChar("/"))
                myself.Genres.Add(New Genre With {.Genre = g.Trim})
            Next
            For Each s As String In movie.Movie.Studio.Split(Convert.ToChar("/"))
                myself.Studios.Add(New Studio With {.Studio = s.Trim})
            Next
            For Each p As MediaContainers.Person In movie.Movie.Actors
                myself.Persons.Add(New Person With {.Name = p.Name, .Role = p.Role, .Type = "Actor"})
            Next
            If Not String.IsNullOrEmpty(movie.Movie.Director) Then myself.Persons.Add(New Person With {.Name = movie.Movie.Director, .Type = "Director"})
            Return myself
        End Function

        Public Shared Sub SaveMovieDB(ByVal movie As Database.DBElement, Optional ByVal tpath As String = "")
            Dim myself As XMLmymovies = GetFromMovieDB(movie)
            Dim xmlSer As New XmlSerializer(GetType(XMLmymovies))
            If tpath = String.Empty Then
                tpath = Path.Combine(Path.GetDirectoryName(movie.NfoPath), "mymovies.xml")
            End If
            Using xmlSW As New StreamWriter(tpath)
                xmlSer.Serialize(xmlSW, myself)
            End Using
        End Sub
    End Class


#End Region 'Nested Types

End Class

