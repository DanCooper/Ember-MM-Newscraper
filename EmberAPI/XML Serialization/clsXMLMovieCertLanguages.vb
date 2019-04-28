
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True),
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="CertLanguages")>
Partial Public Class XMLCertLanguages

    Private languageField As New List(Of CertLanguages)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Language")>
    Public Property Language() As List(Of CertLanguages)
        Get
            Return languageField
        End Get
        Set(value As List(Of CertLanguages))
            languageField = value
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
            Return nameField
        End Get
        Set(value As String)
            nameField = value
        End Set
    End Property

    '''<remarks/>
    Public Property abbreviation() As String
        Get
            Return abbreviationField
        End Get
        Set(value As String)
            abbreviationField = value
        End Set
    End Property
End Class

