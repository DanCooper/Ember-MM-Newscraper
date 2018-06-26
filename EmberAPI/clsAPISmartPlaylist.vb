' ################################################################################
' #                             EMBER MEDIA MANAGER                              #
' ################################################################################
' ################################################################################
' # This file is part of Ember Media Manager.                                    #
' #                                                                              #
' # Ember Media Manager is free software: you can redistribute it and/or modify  #
' # it under the terms of the GNU General Public License as published by         #
' # the Free Software Foundation, either version 3 of the License, or            #
' # (at your option) any later version.                                          #
' #                                                                              #
' # Ember Media Manager is distributed in the hope that it will be useful,       #
' # but WITHOUT ANY WARRANTY; without even the implied warranty of               #
' # MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
' # GNU General Public License for more details.                                 #
' #                                                                              #
' # You should have received a copy of the GNU General Public License            #
' # along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
' ################################################################################

Imports NLog
Imports System.Xml.Serialization

Namespace SmartPlaylist

    Public Enum Condition As Integer
        [And]
        [Or]
    End Enum

    Public Enum Direction As Integer
        Ascending
        Descending
        NotSet
    End Enum

    Public Enum Operators As Integer
        After
        Before
        Between
        Contains
        DoesNotContain
        EndWith
        [False]
        GreaterThan
        GreaterThanOrEqual
        [In]
        [Is]
        [IsNot]
        IsNotNull
        IsNotNullOrEmpty
        IsNull
        IsNullOrEmpty
        LessThan
        LessThanOrEqual
        NotBetween
        NotIn
        StartWith
        [True]
    End Enum

    <Serializable()>
    Public Class Order

#Region "Properties"

        <XmlAttribute("direction")>
        Public Property Direction As Direction = Direction.NotSet

        <XmlText()>
        Public Property SortedBy As Database.ColumnName = Database.ColumnName.Title

#End Region 'Properties

    End Class

    <Serializable()>
    Public Class Rule

#Region "Properties"

        <XmlAttribute("field")>
        Public Property Field As Database.ColumnName

        <XmlAttribute("operator")>
        Public Property [Operator] As Operators

        <XmlElement("value")>
        Public Property Value As Object

        <XmlElement("value2")>
        Public Property Value2 As Object

#End Region 'Properties

    End Class


    <Serializable()>
    Public Class RuleWithOperator

#Region "Properties"

        <XmlAttribute("innercondition")>
        Public Property InnerCondition As Condition

        <XmlAttribute("outercondition")>
        Public Property OuterCondition As Condition

        <XmlElement("rule")>
        Public Property Rules As List(Of Rule) = New List(Of Rule)

#End Region 'Properties

    End Class

    <Serializable()>
    <XmlRoot("smartplaylist")>
    Public Class Playlist

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Dim _filter As String = String.Empty
        Dim _sqlfullquery As String = String.Empty
        Dim _sqlquery As String = String.Empty

#End Region 'Fields

#Region "Properties"

        <XmlAttribute("type")>
        Private Property Type As Enums.ContentType

        <XmlElement("name")>
        Private Property Name As String = String.Empty

        <XmlIgnore()>
        Public ReadOnly Property NameSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Name)
            End Get
        End Property

        <XmlElement("match")>
        Public Property Match As Condition

        <XmlElement("limit")>
        Public Property Limit As Integer = 0

        <XmlIgnore()>
        Public ReadOnly Property LimitSpecified() As Boolean
            Get
                Return Not Limit = 0
            End Get
        End Property

        <XmlElement("order")>
        Public Property Order As Order = New Order

        <XmlIgnore()>
        Public ReadOnly Property OrderSpecified As Boolean
            Get
                Return Not Order.Direction = Direction.NotSet
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property AnyRuleSpecified As Boolean
            Get
                Return RulesSpecified OrElse RulesWithOperatorSpecified
            End Get
        End Property

        <XmlElement("rule")>
        Public Property Rules As List(Of Rule) = New List(Of Rule)

        <XmlIgnore()>
        Private ReadOnly Property RulesSpecified As Boolean
            Get
                Return Rules.Count > 0
            End Get
        End Property

        <XmlElement("rulewithoperator")>
        Public Property RulesWithOperator As List(Of RuleWithOperator) = New List(Of RuleWithOperator)

        <XmlIgnore()>
        Private ReadOnly Property RulesWithOperatorSpecified As Boolean
            Get
                Return RulesWithOperator.Count > 0
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property Contains(ByVal field As Database.ColumnName) As Boolean
            Get
                Return Rules.FirstOrDefault(Function(f) f.Field = field) IsNot Nothing
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property Contains(ByVal field As Database.ColumnName, ByVal [operator] As Operators) As Boolean
            Get
                Return Rules.FirstOrDefault(Function(f) f.Field = field AndAlso f.Operator = [operator]) IsNot Nothing
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property ContainsAnyDataFieldFilter() As Boolean
            Get
                Return Rules.FirstOrDefault(Function(f) _
                                                f.Field = Database.ColumnName.Aired OrElse
                                                f.Field = Database.ColumnName.Certifications OrElse
                                                f.Field = Database.ColumnName.Countries OrElse
                                                f.Field = Database.ColumnName.Creators OrElse
                                                f.Field = Database.ColumnName.Credits OrElse
                                                f.Field = Database.ColumnName.Directors OrElse
                                                f.Field = Database.ColumnName.EpisodeGuideURL OrElse
                                                f.Field = Database.ColumnName.Genres OrElse
                                                f.Field = Database.ColumnName.LastPlayed OrElse
                                                f.Field = Database.ColumnName.MPAA OrElse
                                                f.Field = Database.ColumnName.OriginalTitle OrElse
                                                f.Field = Database.ColumnName.Outline OrElse
                                                f.Field = Database.ColumnName.PlayCount OrElse
                                                f.Field = Database.ColumnName.Plot OrElse
                                                f.Field = Database.ColumnName.Premiered OrElse
                                                f.Field = Database.ColumnName.Ratings OrElse
                                                f.Field = Database.ColumnName.ReleaseDate OrElse
                                                f.Field = Database.ColumnName.Runtime OrElse
                                                f.Field = Database.ColumnName.SortTitle OrElse
                                                f.Field = Database.ColumnName.Status OrElse
                                                f.Field = Database.ColumnName.Studios OrElse
                                                f.Field = Database.ColumnName.Tagline OrElse
                                                f.Field = Database.ColumnName.Tags OrElse
                                                f.Field = Database.ColumnName.Title OrElse
                                                f.Field = Database.ColumnName.Top250 OrElse
                                                f.Field = Database.ColumnName.Trailer OrElse
                                                f.Field = Database.ColumnName.UserRating OrElse
                                                f.Field = Database.ColumnName.VideoSource OrElse
                                                f.Field = Database.ColumnName.Year) IsNot Nothing
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property ContainsAnyFromSearchBar() As Boolean
            Get
                Return Rules.FirstOrDefault(Function(f) _
                                                f.Field = Database.ColumnName.Actor OrElse
                                                f.Field = Database.ColumnName.Countries OrElse
                                                f.Field = Database.ColumnName.Creators OrElse
                                                f.Field = Database.ColumnName.Credits OrElse
                                                f.Field = Database.ColumnName.Directors OrElse
                                                f.Field = Database.ColumnName.MovieTitles OrElse
                                                f.Field = Database.ColumnName.OriginalTitle OrElse
                                                f.Field = Database.ColumnName.Role OrElse
                                                f.Field = Database.ColumnName.Studios OrElse
                                                f.Field = Database.ColumnName.Title) IsNot Nothing
            End Get
        End Property
        ''' <summary>
        ''' Returns only the filter for use in DataGridViews
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Public ReadOnly Property Filter As String
            Get
                Return _filter
            End Get
        End Property
        ''' <summary>
        ''' Returns the full SQL query for external use
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Public ReadOnly Property SqlFullQuery As String
            Get
                Return _sqlfullquery
            End Get
        End Property
        ''' <summary>
        ''' Returns only the SQL query without the filter part
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Public ReadOnly Property SqlQuery As String
            Get
                Return _sqlquery
            End Get
        End Property

#End Region 'Properties

#Region "Constructors"

        Public Sub New(ByVal playlistType As Enums.ContentType)
            Type = playlistType
        End Sub

#End Region 'Constructors

#Region "Methods"

        Private Function BuildFilter(ByVal condition As Condition) As String
            Dim lstRules As New List(Of String)
            If RulesSpecified Then lstRules.Add(BuildRule(Rules, condition))
            For Each nRuleWithOperator In RulesWithOperator
                lstRules.Add(BuildRule(nRuleWithOperator.Rules, nRuleWithOperator.InnerCondition))
            Next
            lstRules = lstRules.Where(Function(f) Not String.IsNullOrEmpty(f)).ToList
            If lstRules.Count > 0 Then
                logger.Trace(String.Format("Current Filter: {0}", String.Format("({0})", String.Join(String.Format(" {0} ", condition.ToString.ToUpper), lstRules))))
                Return String.Format("({0})", String.Join(String.Format(" {0} ", condition.ToString.ToUpper), lstRules))
            End If
            Return String.Empty
        End Function

        Private Function BuildRule(ByVal rules As List(Of Rule), ByVal condition As Condition) As String
            Dim lstRules As New List(Of String)
            For Each nRule In rules
                Dim strRule As String = String.Empty
                Select Case nRule.Operator
                    Case Operators.After
                    Case Operators.Before
                    Case Operators.Between
                        If nRule.Value2 IsNot Nothing Then
                            If Database.Helpers.GetDataType(nRule.Field) = Database.DataType.String Then
                                strRule = String.Format("({0} >= '{1}' AND {0} <='{2}')", Database.Helpers.GetColumnName(nRule.Field), nRule.Value, nRule.Value2)
                            Else
                                strRule = String.Format("({0} >= {1} AND {0} <={2})", Database.Helpers.GetColumnName(nRule.Field), nRule.Value, nRule.Value2)
                            End If
                        End If
                    Case Operators.Contains
                        strRule = String.Format("{0} LIKE '%{1}%'", Database.Helpers.GetColumnName(nRule.Field), StringUtils.ConvertToValidFilterString(nRule.Value.ToString))
                    Case Operators.DoesNotContain
                        strRule = String.Format("{0} NOT LIKE '%{1}%'", Database.Helpers.GetColumnName(nRule.Field), StringUtils.ConvertToValidFilterString(nRule.Value.ToString))
                    Case Operators.EndWith
                        strRule = String.Format("{0} LIKE '%{1}'", Database.Helpers.GetColumnName(nRule.Field), StringUtils.ConvertToValidFilterString(nRule.Value.ToString))
                    Case Operators.False
                        strRule = String.Format("{0}=0", Database.Helpers.GetColumnName(nRule.Field))
                    Case Operators.GreaterThan
                        strRule = String.Format("{0} > {1}", Database.Helpers.GetColumnName(nRule.Field), nRule.Value)
                    Case Operators.GreaterThanOrEqual
                        strRule = String.Format("{0} >= {1}", Database.Helpers.GetColumnName(nRule.Field), nRule.Value)
                    Case Operators.Is
                        If Database.Helpers.GetDataType(nRule.Field) = Database.DataType.String Then
                            strRule = String.Format("{0}='{1}'", Database.Helpers.GetColumnName(nRule.Field), StringUtils.ConvertToValidFilterString(nRule.Value.ToString))
                        Else
                            strRule = String.Format("{0}={1}", Database.Helpers.GetColumnName(nRule.Field), nRule.Value)
                        End If
                    Case Operators.IsNot
                        If Database.Helpers.GetDataType(nRule.Field) = Database.DataType.String Then
                            strRule = String.Format("NOT {0}='{1}'", Database.Helpers.GetColumnName(nRule.Field), StringUtils.ConvertToValidFilterString(nRule.Value.ToString))
                        Else
                            strRule = String.Format("NOT {0}={1}", Database.Helpers.GetColumnName(nRule.Field), nRule.Value)
                        End If
                    Case Operators.IsNotNull
                        strRule = String.Format("{0} IS NOT NULL", Database.Helpers.GetColumnName(nRule.Field))
                    Case Operators.IsNotNullOrEmpty
                        If Database.Helpers.GetDataType(nRule.Field) = Database.DataType.String Then
                            strRule = String.Format("({0} IS NOT NULL AND NOT {0}='')", Database.Helpers.GetColumnName(nRule.Field))
                        Else
                            strRule = String.Format("({0} IS NOT NULL AND NOT {0}=0)", Database.Helpers.GetColumnName(nRule.Field))
                        End If
                    Case Operators.IsNull
                        strRule = String.Format("({0} IS NULL)", Database.Helpers.GetColumnName(nRule.Field))
                    Case Operators.IsNullOrEmpty
                        If Database.Helpers.GetDataType(nRule.Field) = Database.DataType.String Then
                            strRule = String.Format("({0} IS NULL OR {0}='')", Database.Helpers.GetColumnName(nRule.Field))
                        Else
                            strRule = String.Format("({0} IS NULL OR {0}=0)", Database.Helpers.GetColumnName(nRule.Field))
                        End If
                    Case Operators.LessThan
                        strRule = String.Format("{0} < {1}", Database.Helpers.GetColumnName(nRule.Field), nRule.Value)
                    Case Operators.LessThanOrEqual
                        strRule = String.Format("{0} <= {1}", Database.Helpers.GetColumnName(nRule.Field), nRule.Value)
                    Case Operators.NotBetween
                    Case Operators.NotIn
                    Case Operators.StartWith
                        strRule = String.Format("{0} LIKE '{1}%'", Database.Helpers.GetColumnName(nRule.Field), StringUtils.ConvertToValidFilterString(nRule.Value.ToString))
                    Case Operators.True
                        strRule = String.Format("{0}=1", Database.Helpers.GetColumnName(nRule.Field))
                End Select
                If Not String.IsNullOrEmpty(strRule) Then lstRules.Add(strRule)
            Next
            If lstRules.Count > 0 Then
                Return String.Format("({0})", String.Join(String.Format(" {0} ", condition.ToString.ToUpper), lstRules.Where(Function(f) Not String.IsNullOrEmpty(f))))
            End If
            Return String.Empty
        End Function

        Public Function BuildSqlQuery() As String
            Dim strQuery As String = String.Empty
            Select Case Type
                Case Enums.ContentType.Movie
                   ' strQuery = String.Format()
                Case Enums.ContentType.MovieSet
                Case Enums.ContentType.TVShow
            End Select
            Return strQuery
        End Function


        Public Shared Function ConvertStringToOperator(ByVal [operator] As String) As Operators
            Select Case [operator].ToUpper
                Case "=", "IS"
                    Return Operators.Is
                Case "<>", "NOT"
                    Return Operators.IsNot
                Case ">="
                    Return Operators.GreaterThanOrEqual
                Case ">"
                    Return Operators.GreaterThan
                Case "<="
                    Return Operators.LessThanOrEqual
                Case "<"
                    Return Operators.LessThan
                Case "BETWEEN"
                    Return Operators.Between
                Case "LIKE"
                    Return Operators.Contains
                Case "IN"
                    Return Operators.In
                Case "NOT BETWEEN"
                    Return Operators.NotBetween
                Case "NOT IN"
                    Return Operators.NotIn
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Sub RemoveAll(ByVal field As Database.ColumnName)
            Rules.RemoveAll(Function(f) f.Field = field)
            RulesWithOperator.RemoveAll(Function(f) f.Rules.Where(Function(r) r.Field = field).Count > 0)
        End Sub

        Public Sub RemoveAll(ByVal field As Database.ColumnName, ByVal [operator] As Operators)
            Rules.RemoveAll(Function(f) f.Field = field And f.Operator = [operator])
            RulesWithOperator.RemoveAll(Function(f) f.Rules.Where(Function(r) r.Field = field And r.Operator = [operator]).Count > 0)
        End Sub

        Public Sub RemoveAllDataFieldFilters()
            RemoveAll(Database.ColumnName.Aired)
            RemoveAll(Database.ColumnName.Certifications)
            RemoveAll(Database.ColumnName.Countries)
            RemoveAll(Database.ColumnName.Creators)
            RemoveAll(Database.ColumnName.Credits)
            RemoveAll(Database.ColumnName.Directors)
            RemoveAll(Database.ColumnName.EpisodeGuideURL)
            RemoveAll(Database.ColumnName.Genres)
            RemoveAll(Database.ColumnName.LastPlayed)
            RemoveAll(Database.ColumnName.MPAA)
            RemoveAll(Database.ColumnName.OriginalTitle)
            RemoveAll(Database.ColumnName.Outline)
            RemoveAll(Database.ColumnName.PlayCount)
            RemoveAll(Database.ColumnName.Plot)
            RemoveAll(Database.ColumnName.Premiered)
            RemoveAll(Database.ColumnName.Ratings)
            RemoveAll(Database.ColumnName.ReleaseDate)
            RemoveAll(Database.ColumnName.Runtime)
            RemoveAll(Database.ColumnName.SortTitle)
            RemoveAll(Database.ColumnName.Status)
            RemoveAll(Database.ColumnName.Studios)
            RemoveAll(Database.ColumnName.Tagline)
            RemoveAll(Database.ColumnName.Tags)
            RemoveAll(Database.ColumnName.Title)
            RemoveAll(Database.ColumnName.Top250)
            RemoveAll(Database.ColumnName.Trailer)
            RemoveAll(Database.ColumnName.UserRating)
            RemoveAll(Database.ColumnName.VideoSource)
            RemoveAll(Database.ColumnName.Year)
        End Sub

        Public Sub RemoveAllMissingFilters()
            RemoveAll(Database.ColumnName.BannerPath)
            RemoveAll(Database.ColumnName.CharacterArtPath)
            RemoveAll(Database.ColumnName.ClearArtPath)
            RemoveAll(Database.ColumnName.ClearLogoPath)
            RemoveAll(Database.ColumnName.DiscArtPath)
            RemoveAll(Database.ColumnName.ExtrafanartsPath)
            RemoveAll(Database.ColumnName.ExtrathumbsPath)
            RemoveAll(Database.ColumnName.FanartPath)
            RemoveAll(Database.ColumnName.HasSubtitles)
            RemoveAll(Database.ColumnName.LandscapePath)
            RemoveAll(Database.ColumnName.NfoPath)
            RemoveAll(Database.ColumnName.PosterPath)
            RemoveAll(Database.ColumnName.ThemePath)
            RemoveAll(Database.ColumnName.TrailerPath)
        End Sub

        Public Sub RemoveAllSearchbarFilters()
            RemoveAll(Database.ColumnName.Actor)
            RemoveAll(Database.ColumnName.Countries)
            RemoveAll(Database.ColumnName.Creators)
            RemoveAll(Database.ColumnName.Credits)
            RemoveAll(Database.ColumnName.Directors)
            RemoveAll(Database.ColumnName.MovieTitles)
            RemoveAll(Database.ColumnName.OriginalTitle)
            RemoveAll(Database.ColumnName.Role)
            RemoveAll(Database.ColumnName.Studios)
            RemoveAll(Database.ColumnName.Title)
        End Sub

#End Region 'Methods

    End Class

End Namespace