'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="AdvancedSettings")> _
Partial Public Class clsXMLAdvancedSettings

    Private _settingField As List(Of AdvancedSettingsSetting)

    Private _complexSettingsField As List(Of AdvancedSettingsComplexSettings)

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Setting")> _
    Public Property Setting As List(Of AdvancedSettingsSetting)
        Get
            Return Me._settingField
        End Get
        Set(value As List(Of AdvancedSettingsSetting))
            Me._settingField = value
        End Set
    End Property

    '''<remarks/>
    Public Property ComplexSettings As List(Of AdvancedSettingsComplexSettings)
        Get
            Return Me._complexSettingsField
        End Get
        Set(value As List(Of AdvancedSettingsComplexSettings))
            Me._complexSettingsField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Partial Public Class AdvancedSettingsSetting

    Private _sectionField As String

    Private _nameField As String

    Private _valueField As String

    Private _defaultValueField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Section() As String
        Get
            Return Me._sectionField
        End Get
        Set(value As String)
            Me._sectionField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Name() As String
        Get
            Return Me._nameField
        End Get
        Set(value As String)
            Me._nameField = Value
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

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Property DefaultValue() As String
        Get
            Return Me._defaultValueField
        End Get
        Set(value As String)
            Me._defaultValueField = value
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
            Return Me._tableField
        End Get
        Set(value As AdvancedSettingsComplexSettingsTable)
            Me._tableField = Value
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
            Return Me._itemField
        End Get
        Set(value As List(Of AdvancedSettingsComplexSettingsTableItem))
            Me._itemField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Section() As String
        Get
            Return Me._sectionField
        End Get
        Set(value As String)
            Me._sectionField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Name() As String
        Get
            Return Me._nameField
        End Get
        Set(value As String)
            Me._nameField = Value
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
            Return Me._nameField
        End Get
        Set(value As String)
            Me._nameField = Value
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


