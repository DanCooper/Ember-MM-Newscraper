'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True),
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="AdvancedSettings")>
Partial Public Class XMLAdvancedSettings

    Private _settingField As New List(Of AdvancedSettingsSetting)

    Private _complexSettingsField As New List(Of AdvancedSettingsComplexSettings)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Setting")>
    Public Property Setting As List(Of AdvancedSettingsSetting)
        Get
            Return _settingField
        End Get
        Set(value As List(Of AdvancedSettingsSetting))
            _settingField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("ComplexSettings")>
    Public Property ComplexSettings As List(Of AdvancedSettingsComplexSettings)
        Get
            Return _complexSettingsField
        End Get
        Set(value As List(Of AdvancedSettingsComplexSettings))
            _complexSettingsField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class AdvancedSettingsSetting

    Private _sectionField As String

    Private _nameField As String

    Private _contentField As Enums.ContentType

    Private _valueField As String

    Private _defaultValueField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Section() As String
        Get
            Return _sectionField
        End Get
        Set(value As String)
            _sectionField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Content() As Enums.ContentType
        Get
            Return _contentField
        End Get
        Set(value As Enums.ContentType)
            _contentField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Name() As String
        Get
            Return _nameField
        End Get
        Set(value As String)
            _nameField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Property Value() As String
        Get
            Return _valueField
        End Get
        Set(value As String)
            _valueField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property DefaultValue() As String
        Get
            Return _defaultValueField
        End Get
        Set(value As String)
            _defaultValueField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class AdvancedSettingsComplexSettings

    Private _tableField As AdvancedSettingsComplexSettingsTable

    '''<remarks/>
    Public Property Table() As AdvancedSettingsComplexSettingsTable
        Get
            Return _tableField
        End Get
        Set(value As AdvancedSettingsComplexSettingsTable)
            _tableField = Value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class AdvancedSettingsComplexSettingsTable

    Private _itemField As List(Of AdvancedSettingsComplexSettingsTableItem)

    Private _sectionField As String

    Private _nameField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Item")> _
    Public Property Item() As List(Of AdvancedSettingsComplexSettingsTableItem)
        Get
            Return _itemField
        End Get
        Set(value As List(Of AdvancedSettingsComplexSettingsTableItem))
            _itemField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Section() As String
        Get
            Return _sectionField
        End Get
        Set(value As String)
            _sectionField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Name() As String
        Get
            Return _nameField
        End Get
        Set(value As String)
            _nameField = Value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class AdvancedSettingsComplexSettingsTableItem

    Private _nameField As String

    Private _valueField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Name() As String
        Get
            Return _nameField
        End Get
        Set(value As String)
            _nameField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Property Value() As String
        Get
            Return _valueField
        End Get
        Set(value As String)
            _valueField = Value
        End Set
    End Property
End Class


