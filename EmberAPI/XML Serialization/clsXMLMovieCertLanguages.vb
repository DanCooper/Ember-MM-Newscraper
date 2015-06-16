
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="CertLanguages")> _
Partial Public Class clsXMLCertLanguages

    Private languageField As New List(Of CertLanguages)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Language")> _
    Public Property Language() As List(Of CertLanguages)
        Get
            Return Me.languageField
        End Get
        Set(value As List(Of CertLanguages))
            Me.languageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class CertLanguages

    Private nameField As String

    Private abbreviationField As String

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
End Class

