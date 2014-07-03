
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="ratings")> _
Partial Public Class clsXMLRatings

    Private moviesField As New List(Of ratingsNameMovie)

    Private tvField As New List(Of ratingsNameTV)

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("name", IsNullable:=False)> _
    Public Property movies() As List(Of ratingsNameMovie)
        Get
            Return Me.moviesField
        End Get
        Set(value As List(Of ratingsNameMovie))
            Me.moviesField = value
        End Set
    End Property
    <System.Xml.Serialization.XmlArrayItemAttribute("name", IsNullable:=False)> _
    Public Property tv() As List(Of ratingsNameTV)
        Get
            Return Me.tvField
        End Get
        Set(value As List(Of ratingsNameTV))
            Me.tvField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class ratingsNameMovie

    Private countryField As String

    Private iconField As String

    Private searchstringField As String


    Public Property country() As String
        Get
            Return Me.countryField
        End Get
        Set(value As String)
            Me.countryField = value
        End Set
    End Property

    Public Property icon() As String
        Get
            Return Me.iconField
        End Get
        Set(value As String)
            Me.iconField = value
        End Set
    End Property
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property searchstring() As String
        Get
            Return Me.searchstringField
        End Get
        Set(value As String)
            Me.searchstringField = value
        End Set
    End Property
End Class

Partial Public Class ratingsNameTV

    Private countryField As String

    Private iconField As String

    Private searchstringField As String


    Public Property country() As String
        Get
            Return Me.countryField
        End Get
        Set(value As String)
            Me.countryField = value
        End Set
    End Property

    Public Property icon() As String
        Get
            Return Me.iconField
        End Get
        Set(value As String)
            Me.iconField = value
        End Set
    End Property
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property searchstring() As String
        Get
            Return Me.searchstringField
        End Get
        Set(value As String)
            Me.searchstringField = value
        End Set
    End Property
End Class

