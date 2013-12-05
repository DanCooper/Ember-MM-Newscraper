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

Namespace DVDProfiler

    <XmlRoot("Collection")> _
    Public Class Collection

#Region "Fields"

        Private _dvd As New List(Of cDVD)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("DVD")> _
        Public Property DVD() As List(Of cDVD)
            Get
                Return Me._dvd
            End Get
            Set(ByVal Value As List(Of cDVD))
                Me._dvd = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._dvd = New List(Of cDVD)
        End Sub
#End Region 'Methods
    End Class

    '<XmlRoot("DVD")> _
    Public Class cDVD

#Region "Fields"

        Private _title As String
        Private _productionyear As String
        Private _casetype As String
        Private _mediatypes As New dMediaTypes
        Private _discs As New dDiscs
        Private _audio As New dAudio
        Private _subtitles As New dSubtitles

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("Title")> _
        Public Property Title() As String
            Get
                Return Me._title
            End Get
            Set(ByVal value As String)
                Me._title = value
            End Set
        End Property

        <XmlElement("ProductionYear")> _
        Public Property ProductionYear() As String
            Get
                Return Me._productionyear
            End Get
            Set(ByVal value As String)
                Me._productionyear = value
            End Set
        End Property

        <XmlElement("CaseType")> _
        Public Property CaseType() As String
            Get
                Return Me._casetype
            End Get
            Set(ByVal value As String)
                Me._casetype = value
            End Set
        End Property

        <XmlElement("MediaTypes")> _
        Public Property MediaTypes() As dMediaTypes
            Get
                Return Me._mediatypes
            End Get
            Set(ByVal Value As dMediaTypes)
                Me._mediatypes = Value
            End Set
        End Property

        <XmlElement("Discs")> _
        Public Property Discs() As dDiscs
            Get
                Return Me._discs
            End Get
            Set(ByVal Value As dDiscs)
                Me._discs = Value
            End Set
        End Property

        <XmlElement("Audio")> _
        Public Property Audio() As dAudio
            Get
                Return Me._audio
            End Get
            Set(ByVal Value As dAudio)
                Me._audio = Value
            End Set
        End Property

        <XmlElement("Subtitles")> _
        Public Property Subtitles() As dSubtitles
            Get
                Return Me._subtitles
            End Get
            Set(ByVal Value As dSubtitles)
                Me._subtitles = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._title = String.Empty
            Me._productionyear = String.Empty
            Me._casetype = String.Empty
            Me._mediatypes.Clear()
            Me._discs.Clear()
            Me._audio.Clear()
            Me._subtitles.Clear()
        End Sub

#End Region 'Methods
    End Class

    Public Class dMediaTypes

#Region "Fields"

        Private _dvd As Boolean
        Private _hddvd As Boolean
        Private _bluray As Boolean

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("DVD")> _
        Public Property DVD() As Boolean
            Get
                Return Me._dvd
            End Get
            Set(ByVal Value As Boolean)
                Me._dvd = Value
            End Set
        End Property

        <XmlElement("HDDVD")> _
        Public Property HDDVD() As Boolean
            Get
                Return Me._hddvd
            End Get
            Set(ByVal Value As Boolean)
                Me._hddvd = Value
            End Set
        End Property

        <XmlElement("BluRay")> _
        Public Property BluRay() As Boolean
            Get
                Return Me._bluray
            End Get
            Set(ByVal Value As Boolean)
                Me._bluray = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._dvd = False
            Me._hddvd = False
            Me._bluray = False
        End Sub

#End Region 'Methods
    End Class

    Public Class dSubtitles

#Region "Fields"

        Private _subtitle As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("Subtitle")> _
        Public Property Subtitle() As String
            Get
                Return Me._subtitle
            End Get
            Set(ByVal Value As String)
                Me._subtitle = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._subtitle = String.Empty
        End Sub

#End Region 'Methods
    End Class

    Public Class dDiscs

#Region "Fields"

        Private _disc As New List(Of dDisc)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("Disc")> _
        Public Property Disc() As List(Of dDisc)
            Get
                Return Me._disc
            End Get
            Set(ByVal Value As List(Of dDisc))
                Me._disc = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._disc.Clear()
        End Sub

#End Region 'Methods
    End Class

    Public Class dDisc

#Region "Fields"

        Private _location As String
        Private _slot As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("Location")> _
        Public Property Location() As String
            Get
                Return Me._location
            End Get
            Set(ByVal Value As String)
                Me._location = Value
            End Set
        End Property

        <XmlElement("Slot")> _
        Public Property Slot() As String
            Get
                Return Me._slot
            End Get
            Set(ByVal Value As String)
                Me._slot = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._location = String.Empty
            Me._slot = String.Empty
        End Sub

#End Region 'Methods
    End Class

    Public Class dAudio

#Region "Fields"

        Private _audiotrack As New List(Of dAudioTrack)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("AudioTrack")> _
        Public Property AudioTrack() As List(Of dAudioTrack)
            Get
                Return Me._audiotrack
            End Get
            Set(ByVal Value As List(Of dAudioTrack))
                Me._audiotrack = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._audiotrack.Clear()
        End Sub

#End Region 'Methods
    End Class

    Public Class dAudioTrack

#Region "Fields"

        Private _audiocontent As String
        Private _audioformat As String
        Private _audiochannels As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("AudioContent")> _
        Public Property AudioContent() As String
            Get
                Return Me._audiocontent
            End Get
            Set(ByVal Value As String)
                Me._audiocontent = Value
            End Set
        End Property

        <XmlElement("AudioFormat")> _
        Public Property AudioFormat() As String
            Get
                Return Me._audioformat
            End Get
            Set(ByVal Value As String)
                Me._audioformat = Value
            End Set
        End Property

        <XmlElement("AudioChannels")> _
        Public Property AudioChannels() As String
            Get
                Return Me._audiochannels
            End Get
            Set(ByVal Value As String)
                Me._audiochannels = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._audiocontent = String.Empty
            Me._audioformat = String.Empty
            Me._audiochannels = String.Empty
        End Sub

#End Region 'Methods
    End Class

End Namespace

