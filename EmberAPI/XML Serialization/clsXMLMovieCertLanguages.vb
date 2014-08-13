
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="MovieCertLanguages")> _
Partial Public Class clsXMLMovieCertLanguages

    Private languageField As New List(Of MovieCertLanguages)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Language")> _
    Public Property Language() As List(Of MovieCertLanguages)
        Get
            Return Me.languageField
        End Get
        Set(value As List(Of MovieCertLanguages))
            Me.languageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class MovieCertLanguages

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

