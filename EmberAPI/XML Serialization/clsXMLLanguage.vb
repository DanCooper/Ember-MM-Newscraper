
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="strings")> _
Partial Public Class clsXMLLanguage

    Private _stringField As New List(Of LanguageString)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("string")> _
    Public Property [string]() As List(Of LanguageString)
        Get
            Return Me._stringField
        End Get
        Set(value As List(Of LanguageString))
            Me._stringField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class LanguageString

    Private _idField As UShort

    Private _valueField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property id() As UShort
        Get
            Return Me._idField
        End Get
        Set(value As UShort)
            Me._idField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Property Value() As String
        Get
            Return Me._valueField
        End Get
        Set(value As String)
            Me._valueField = Value
        End Set
    End Property
End Class


