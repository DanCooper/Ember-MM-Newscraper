
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="genres")> _
Partial Public Class clsXMLGenres

    Private _languages As New List(Of String)

    Private _nameField As New List(Of genresName)

    Private _defaultField As genresDefault

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("language", IsNullable:=False)> _
    Public Property supported() As List(Of String)
        Get
            Return Me._languages
        End Get
        Set(value As List(Of String))
            Me._languages = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("name")> _
    Public Property name() As List(Of genresName)
        Get
            Return Me._nameField
        End Get
        Set(value As List(Of genresName))
            Me._nameField = value
        End Set
    End Property

    '''<remarks/>
    Public Property [default]() As genresDefault
        Get
            Return Me._defaultField
        End Get
        Set(value As genresDefault)
            Me._defaultField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class genresName

    Private _iconField As String

    Private _searchstringField As String

    Private _languageField As String

    '''<remarks/>
    Public Property icon() As String
        Get
            Return Me._iconField
        End Get
        Set(value As String)
            Me._iconField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property searchstring() As String
        Get
            Return Me._searchstringField
        End Get
        Set(value As String)
            Me._searchstringField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property language() As String
        Get
            Return Me._languageField
        End Get
        Set(value As String)
            Me._languageField = Value
        End Set
    End Property

    <System.Xml.Serialization.XmlIgnore()> _
    Public Property languages() As List(Of String)
        Get
            Return Me._languageField.Split("|"c).ToList()
        End Get
        Set(value As List(Of String))
            Me._languageField = String.Join("|", value.ToArray())
        End Set
    End Property
End Class

        '''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class genresDefault

        Private _iconField As String

        '''<remarks/>
        Public Property icon() As String
            Get
                Return Me._iconField
            End Get
            Set(value As String)
                Me._iconField = value
            End Set
        End Property
    End Class

