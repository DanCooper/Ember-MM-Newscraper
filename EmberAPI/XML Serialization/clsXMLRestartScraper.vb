'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
 <Serializable> _
Partial Public Class clsXMLRestartScraper
    Private _selectedField As Boolean

    Private _sTypeField As Enums.ScrapeType

    Private _OptionsField As New Structures.ScrapeOptions_Movie

    Private _OptionsField_MovieSet As New Structures.ScrapeOptions_MovieSet

    Private _GlobalScrapeStatus As New Structures.ScrapeModifier_Movie_MovieSet

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

    Public Property Options As Structures.ScrapeOptions_Movie
        Get
            Return Me._OptionsField
        End Get
        Set(value As Structures.ScrapeOptions_Movie)
            Me._OptionsField = value
        End Set
    End Property

    Public Property Options_MovieSet As Structures.ScrapeOptions_MovieSet
        Get
            Return Me._OptionsField_MovieSet
        End Get
        Set(value As Structures.ScrapeOptions_MovieSet)
            Me._OptionsField_MovieSet = value
        End Set
    End Property

    Public Property GlobalScrape As Structures.ScrapeModifier_Movie_MovieSet
        Get
            Return Me._GlobalScrapeStatus
        End Get
        Set(value As Structures.ScrapeModifier_Movie_MovieSet)
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