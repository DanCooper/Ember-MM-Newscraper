'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
 <Serializable> _
Partial Public Class clsXMLRestartScraper
    Private _selectedField As Boolean

    Private _sTypeField As Enums.ScrapeType

    Private _OptionsField As New Structures.MovieScrapeOptions

    Private _GlobalScrapeStatus As New Structures.ScrapeModifier

    Private _ScrapeListField As New System.Collections.Generic.List(Of Object())  'Need an array of objects to binary serialize 

    '''<remarks/>
    Public Property Selected As Boolean
        Get
            Return Me._selectedField
        End Get
        Set(value As Boolean)
            Me._selectedField = value
        End Set
    End Property

    Public Property sType As Enums.ScrapeType
        Get
            Return Me._sTypeField
        End Get
        Set(value As Enums.ScrapeType)
            Me._sTypeField = value
        End Set
    End Property

    Public Property Options As Structures.MovieScrapeOptions
        Get
            Return Me._OptionsField
        End Get
        Set(value As Structures.MovieScrapeOptions)
            Me._OptionsField = value
        End Set
    End Property

    Public Property GlobalScrape As Structures.ScrapeModifier
        Get
            Return Me._GlobalScrapeStatus
        End Get
        Set(value As Structures.ScrapeModifier)
            Me._GlobalScrapeStatus = value
        End Set
    End Property

    Public Property ScrapeList As System.Collections.Generic.List(Of Object())
        Get
            Return Me._ScrapeListField
        End Get
        Set(value As System.Collections.Generic.List(Of Object()))
            Me._ScrapeListField = value
        End Set
    End Property
End Class