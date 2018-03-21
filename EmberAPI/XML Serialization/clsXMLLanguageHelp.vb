
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="strings")> _
Partial Public Class clsXMLLanguageHelp

    Private _stringField As New List(Of HelpString)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("string")> _
    Public Property [string]() As List(Of HelpString)
        Get
            Return _stringField
        End Get
        Set(value As List(Of HelpString))
            _stringField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class HelpString

    Private _controlField As String

    Private _valueField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property control() As String
        Get
            Return _controlField
        End Get
        Set(value As String)
            _controlField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Property Value() As String
        Get
            Return _valueField
        End Get
        Set(value As String)
            _valueField = value
        End Set
    End Property
End Class

