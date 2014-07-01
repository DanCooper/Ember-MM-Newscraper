
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="Languages")> _
Partial Public Class clsXMLTVDBLanguages

    Private languageField As New List(Of TVDBLanguagesLanguage)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Language")> _
    Public Property Language() As List(Of TVDBLanguagesLanguage)
        Get
            Return Me.languageField
        End Get
        Set(value As List(Of TVDBLanguagesLanguage))
            Me.languageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class TVDBLanguagesLanguage

    Private nameField As String

    Private abbreviationField As String

    Private idField As Byte

    '''<remarks/>
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    Public Property abbreviation() As String
        Get
            Return Me.abbreviationField
        End Get
        Set(value As String)
            Me.abbreviationField = value
        End Set
    End Property

    '''<remarks/>
    Public Property id() As Byte
        Get
            Return Me.idField
        End Get
        Set(value As Byte)
            Me.idField = value
        End Set
    End Property
End Class

