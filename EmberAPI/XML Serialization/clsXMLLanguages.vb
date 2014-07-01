
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="Languages")> _
Partial Public Class clsXMLLanguages

    Private languageField As New List(Of LanguagesLanguage)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Language")> _
    Public Property Language() As List(Of LanguagesLanguage)
        Get
            Return Me.languageField
        End Get
        Set(value As List(Of LanguagesLanguage))
            Me.languageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class LanguagesLanguage

    Private alpha2Field As String

    Private alpha3Field As String

    Private nameField As String

    '''<remarks/>
    Public Property Alpha2() As String
        Get
            Return Me.alpha2Field
        End Get
        Set(value As String)
            Me.alpha2Field = Value
        End Set
    End Property

    '''<remarks/>
    Public Property Alpha3() As String
        Get
            Return Me.alpha3Field
        End Get
        Set(value As String)
            Me.alpha3Field = Value
        End Set
    End Property

    '''<remarks/>
    Public Property Name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = Value
        End Set
    End Property
End Class

