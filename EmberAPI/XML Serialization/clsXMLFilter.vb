
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="filters")> _
Partial Public Class clsXMLFilter

    Private _filterField As New List(Of FilterName)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("filter")> _
    Public Property filter() As List(Of FilterName)
        Get
            Return Me._filterField
        End Get
        Set(value As List(Of FilterName))
            Me._filterField = value
        End Set
    End Property

End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class FilterName

    Private _queryField As String

    Private _nameField As String

    Private _typeField As String

    '''<remarks/>
    Public Property query() As String
        Get
            Return Me._queryField
        End Get
        Set(value As String)
            Me._queryField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property name() As String
        Get
            Return Me._nameField
        End Get
        Set(value As String)
            Me._nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property type() As String
        Get
            Return Me._typeField
        End Get
        Set(value As String)
            Me._typeField = value
        End Set
    End Property

End Class
