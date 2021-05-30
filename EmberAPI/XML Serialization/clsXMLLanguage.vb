
'''<remarks/>
<Xml.Serialization.XmlType(AnonymousType:=True),
 Xml.Serialization.XmlRoot([Namespace]:="", IsNullable:=False, ElementName:="strings")>
Partial Public Class clsXMLLanguage

    Private _stringField As New List(Of LanguageString)

    '''<remarks/>
    <Xml.Serialization.XmlElement("string")>
    Public Property [string]() As List(Of LanguageString)
        Get
            Return _stringField
        End Get
        Set(value As List(Of LanguageString))
            _stringField = value
        End Set
    End Property
End Class

<Xml.Serialization.XmlType(AnonymousType:=True)>
Partial Public Class LanguageString

    Private _idField As UShort

    Private _valueField As String

    <Xml.Serialization.XmlAttribute()>
    Public Property id() As UShort
        Get
            Return _idField
        End Get
        Set(value As UShort)
            _idField = value
        End Set
    End Property

    '''<remarks/>
    <Xml.Serialization.XmlText()>
    Public Property Value() As String
        Get
            Return _valueField
        End Get
        Set(value As String)
            _valueField = value
        End Set
    End Property
End Class