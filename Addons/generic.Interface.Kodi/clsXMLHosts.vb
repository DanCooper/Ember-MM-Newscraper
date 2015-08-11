
'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="hosts")> _
Partial Public Class clsXMLHosts

    Private hostField() As Host

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("host")> _
    Public Property host() As Host()
        Get
            Return Me.hostField
        End Get
        Set(value As Host())
            Me.hostField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class Host

    Private sourceField() As HostSource

    Private nameField As String

    Private portField As Integer

    Private usernameField As String

    Private addressField As String

    Private passwordField As String

    Private realtimesyncField As Boolean

    Private moviesetpathField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("source")> _
    Public Property source() As HostSource()
        Get
            Return Me.sourceField
        End Get
        Set(value As HostSource())
            Me.sourceField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property port() As Integer
        Get
            Return Me.portField
        End Get
        Set(value As Integer)
            Me.portField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property username() As String
        Get
            Return Me.usernameField
        End Get
        Set(value As String)
            Me.usernameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property address() As String
        Get
            Return Me.addressField
        End Get
        Set(value As String)
            Me.addressField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property password() As String
        Get
            Return Me.passwordField
        End Get
        Set(value As String)
            Me.passwordField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property realtimesync() As Boolean
        Get
            Return Me.realtimesyncField
        End Get
        Set(value As Boolean)
            Me.realtimesyncField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property moviesetpath() As String
        Get
            Return Me.moviesetpathField
        End Get
        Set(value As String)
            Me.moviesetpathField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class HostSource

    Private typeField As String

    Private remotepathField As String

    Private applicationpathField As String

    '''<remarks/>
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set(value As String)
            Me.typeField = value
        End Set
    End Property


    '''<remarks/>
    Public Property remotepath() As String
        Get
            Return Me.remotepathField
        End Get
        Set(value As String)
            Me.remotepathField = value
        End Set
    End Property

    '''<remarks/>
    Public Property applicationpath() As String
        Get
            Return Me.applicationpathField
        End Get
        Set(value As String)
            Me.applicationpathField = value
        End Set
    End Property
End Class

