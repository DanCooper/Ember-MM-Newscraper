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

Namespace SmartFilter

#Region "Enums"

    Public Enum Conditions As Integer
        All
        Any
        None
        [Not]
        NotAll
    End Enum

    Public Enum Directions As Integer
        Ascending
        Descending
        NotSet
    End Enum

    Public Enum JoinTypes As Integer
        InnerJoin
        LeftJoin
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

#End Region 'Enums

#Region "Classes"

    <Serializable()>
    Public Class Dependency

#Region "Properties"

        Public Property Type As JoinTypes

        Public Property JoinTable As Database.TableName

        Public Property JoinConditions As List(Of Relation) = New List(Of Relation)

        Public ReadOnly Property JoinConditionsSpecified As Boolean
            Get
                Return JoinConditions.Count > 0
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SqlDependency As String
            Get
                If JoinConditionsSpecified Then
                    Return String.Format("{0} {1} ON {2}",
                                         ConvertJoinTypeToString(Type),
                                         Database.Helpers.GetTableName(JoinTable),
                                         String.Join(" AND ", JoinConditions.Select(Function(f) f.SqlRelation))
                                         ).Trim
                Else
                    Return String.Empty
                End If
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Private Function ConvertJoinTypeToString(ByVal JoinType As JoinTypes) As String
            Select Case JoinType
                Case JoinTypes.InnerJoin
                    Return "INNER JOIN"
                Case JoinTypes.LeftJoin
                    Return "LEFT JOIN"
                Case Else
                    Return String.Empty
            End Select
        End Function

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class Order

#Region "Properties"

        <XmlAttribute("direction")>
        Public Property Direction As Directions = Directions.NotSet

        <XmlText()>
        Public Property SortedBy As Database.ColumnName = Database.ColumnName.Title

        <XmlIgnore()>
        Public ReadOnly Property SqlSortedByWithDirection As String
            Get
                Return String.Format("{0} {1}",
                                     Database.Helpers.GetColumnName(SortedBy),
                                     ConvertDirectionToString(Direction)
                                     ).Trim
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Private Function ConvertDirectionToString(ByVal Direction As Directions) As String
            Select Case Direction
                Case Directions.Ascending
                    Return "ASC"
                Case Directions.Descending
                    Return "DESC"
                Case Else
                    Return String.Empty
            End Select
        End Function

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class Relation

#Region "Properties"

        Public Property Table1 As String

        Public ReadOnly Property Table1Specified As Boolean
            Get
                Return Not String.IsNullOrEmpty(Table1)
            End Get
        End Property

        Public Property Field1 As String

        Public ReadOnly Property Field1Specified As Boolean
            Get
                Return Not String.IsNullOrEmpty(Field1)
            End Get
        End Property

        Public Property Table2 As String

        Public ReadOnly Property Table2Specified As Boolean
            Get
                Return Not String.IsNullOrEmpty(Table2)
            End Get
        End Property

        Public Property Field2 As String

        Public ReadOnly Property Field2Specified As Boolean
            Get
                Return Not String.IsNullOrEmpty(Field2)
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SqlRelation As String
            Get
                If Field1Specified AndAlso Field2Specified Then
                    Dim strSqlRelation As String = String.Empty
                    If Table1Specified Then
                        strSqlRelation = String.Format("{0}.{1}=", Table1, Field1)
                    Else
                        strSqlRelation = String.Format("{0}=", Field1)
                    End If
                    If Table2Specified Then
                        strSqlRelation = String.Format("{0}{1}.{2}", strSqlRelation, Table2, Field2)
                    Else
                        strSqlRelation = String.Format("{0}{1}", strSqlRelation, Field2)
                    End If
                    Return strSqlRelation
                Else
                    Return String.Empty
                End If
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

#End Region 'Methods

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

        <XmlAttribute("outercondition")>
        Public Property OuterCondition As Conditions

        <XmlAttribute("innercondition")>
        Public Property InnerCondition As Conditions

        <XmlElement("rule")>
        Public Property Rules As List(Of Rule) = New List(Of Rule)

#End Region 'Properties

    End Class

#End Region 'Classes

    <Serializable()>
    <XmlRoot("smartfilter")>
    Public Class Filter

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Dim _whereclause As String = String.Empty
        Dim _joinclauses As List(Of Dependency) = New List(Of Dependency)
        Dim _match As Conditions = Conditions.All
        Dim _sqlmaingrouping As String = String.Empty
        Dim _sqlmainquery As String = String.Empty

#End Region 'Fields

#Region "Properties"

        <XmlAttribute("type")>
        Public Property Type As Enums.ContentType

        <XmlElement("name")>
        Private Property Name As String = String.Empty

        <XmlIgnore()>
        Public ReadOnly Property NameSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Name)
            End Get
        End Property

        <XmlElement("match")>
        Public Property Match As Conditions
            Get
                Return _match
            End Get
            Set(value As Conditions)
                If Not _match = value Then
                    _match = value
                    Build()
                End If
            End Set
        End Property
        ''' <summary>
        ''' Returns a list of "JOIN" clauses
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Public ReadOnly Property JoinClauses As List(Of Dependency)
            Get
                Return _joinclauses
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property JoinClauseSpecified() As Boolean
            Get
                Return JoinClauses.Count > 0
            End Get
        End Property

        <XmlElement("limit")>
        Public Property LimitClause As Integer = 0

        <XmlIgnore()>
        Public ReadOnly Property LimitClauseSpecified() As Boolean
            Get
                Return LimitClause > 0
            End Get
        End Property

        <XmlElement("orderby")>
        Public Property OrderBy As List(Of Order) = New List(Of Order)

        <XmlIgnore()>
        Public ReadOnly Property OrderBySpecified As Boolean
            Get
                Return OrderBy.Count > 0
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
                                                f.Field = Database.ColumnName.ActorName OrElse
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
        Public ReadOnly Property FilterForBindingSource As String
            Get
                Return String.Empty  '_whereclause
            End Get
        End Property
        ''' <summary>
        ''' True if the default media list view can't provide all filters and a SQL query is needed
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Public ReadOnly Property NeedsQuery As Boolean
            Get
                Return True 'JoinClauseSpecified
            End Get
        End Property
        ''' <summary>
        ''' Returns the SQL "JOIN" clause
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Private ReadOnly Property SqlClause_Join As String
            Get
                If JoinClauseSpecified Then
                    Return String.Join(" ", JoinClauses.Select(Function(f) f.SqlDependency)).Trim
                End If
                Return String.Empty
            End Get
        End Property
        ''' <summary>
        ''' Returns the SQL "LIMIT" clause
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Private ReadOnly Property SqlClause_Limit As String
            Get
                If LimitClauseSpecified Then
                    Return String.Format("LIMIT {0}", LimitClause).Trim
                End If
                Return String.Empty
            End Get
        End Property
        ''' <summary>
        ''' Returns the SQL "ORDER BY" clause
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Private ReadOnly Property SqlClause_OrderBy As String
            Get
                If OrderBySpecified Then
                    Return String.Format("ORDER BY {0}", String.Join(", ", OrderBy.Select(Function(f) f.SqlSortedByWithDirection))).Trim
                End If
                Return String.Empty
            End Get
        End Property
        ''' <summary>
        ''' Returns the SQL "WHERE" clause
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Private ReadOnly Property SqlClause_Where As String
            Get
                If RulesSpecified OrElse RulesWithOperatorSpecified Then
                    BuildCriteria()
                    Return String.Format("WHERE {0}", _whereclause).Trim
                End If
                Return String.Empty
            End Get
        End Property
        ''' <summary>
        ''' Returns the full SQL query for external use
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Public ReadOnly Property SqlQuery_Full As String
            Get
                Return String.Format("{0} {1} {2} {3} {4} {5};",
                                     _sqlmainquery,
                                     SqlClause_Join,
                                     SqlClause_Where,
                                     SqlClause_OrderBy,
                                     SqlClause_Limit,
                                     _sqlmaingrouping).Trim
            End Get
        End Property

#End Region 'Properties

#Region "Constructors"

        Public Sub New(ByVal PlaylistType As Enums.ContentType)
            Type = PlaylistType
            Build()
        End Sub

#End Region 'Constructors

#Region "Methods"

        Public Sub Build()
            BuildSqlMainQuery()
            BuildCriteria()
        End Sub

        Private Sub BuildCriteria()
            _joinclauses.Clear()
            Dim lstRules As New List(Of String)
            If RulesSpecified Then lstRules.Add(BuildClause_Where(Rules, Match))
            For Each nRuleWithOperator In RulesWithOperator
                lstRules.Add(BuildClause_Where(nRuleWithOperator.Rules, nRuleWithOperator.InnerCondition))
            Next
            'remove empty rules
            lstRules = lstRules.Where(Function(f) Not String.IsNullOrEmpty(f)).ToList
            If lstRules.Count > 0 Then
                logger.Trace(String.Format("Current Filter: {0}", String.Format("({0})", String.Join(String.Format(" {0} ", Match.ToString.ToUpper), lstRules))))
                _whereclause = String.Format("({0})", String.Join(String.Format(" {0} ", Match.ToString.ToUpper), lstRules))
            Else
                _whereclause = String.Empty
            End If
        End Sub

        Private Function BuildClause_Join(ByVal Rule As Rule) As String
            If Rule Is Nothing Then Return Nothing

            Dim strRule As String = Nothing
            Select Case Rule.Field
                Case Database.ColumnName.ActorName
                    strRule = BuildRule(Rule)
                    If strRule IsNot Nothing Then
                        Dim nLinkTable As New Dependency With {
                            .Type = JoinTypes.LeftJoin,
                            .JoinTable = Database.TableName.actor_link
                        }
                        nLinkTable.JoinConditions.Add(New Relation With {
                                                      .Table1 = Database.Helpers.GetMainViewName(Type),
                                                      .Field1 = Database.Helpers.GetMainIdName(Database.Helpers.GetMainViewName(Type)),
                                                      .Table2 = Database.Helpers.GetTableName(Database.TableName.actor_link),
                                                      .Field2 = Database.Helpers.GetColumnName(Database.ColumnName.idMedia)
                                                      })
                        _joinclauses.Add(nLinkTable)
                        Dim nTargetTable As New Dependency With {
                            .Type = JoinTypes.LeftJoin,
                            .JoinTable = Database.TableName.person
                        }
                        nTargetTable.JoinConditions.Add(New Relation With {
                                                        .Table1 = Database.Helpers.GetTableName(Database.TableName.actor_link),
                                                        .Field1 = Database.Helpers.GetMainIdName(Database.TableName.actor_link),
                                                        .Table2 = Database.Helpers.GetTableName(Database.TableName.person),
                                                        .Field2 = Database.Helpers.GetMainIdName(Database.TableName.person)
                                                        })
                        _joinclauses.Add(nTargetTable)
                        strRule = String.Format("{0}.{1}",
                                                Database.Helpers.GetTableName(nTargetTable.JoinTable),
                                                strRule)
                    End If
                Case Database.ColumnName.Role
                    strRule = BuildRule(Rule)
                    If strRule IsNot Nothing Then
                        Dim nTargetTable As New Dependency With {
                            .Type = JoinTypes.LeftJoin,
                            .JoinTable = Database.TableName.actor_link
                        }
                        nTargetTable.JoinConditions.Add(New Relation With {
                                                        .Table1 = Database.Helpers.GetMainViewName(Type),
                                                        .Field1 = Database.Helpers.GetMainIdName(Database.Helpers.GetMainViewName(Type)),
                                                        .Table2 = Database.Helpers.GetTableName(Database.TableName.actor_link),
                                                        .Field2 = Database.Helpers.GetColumnName(Database.ColumnName.idMedia)
                                                        })
                        _joinclauses.Add(nTargetTable)
                        strRule = String.Format("{0}.{1}",
                                                Database.Helpers.GetTableName(nTargetTable.JoinTable),
                                                strRule)
                    End If
                Case Database.ColumnName.VideoAspect
                    strRule = BuildRule(Rule)
                    If strRule IsNot Nothing Then
                        Dim nTargetTable As New Dependency With {
                            .Type = JoinTypes.LeftJoin,
                            .JoinTable = Database.TableName.streamdetail
                        }
                        nTargetTable.JoinConditions.Add(New Relation With {
                                                        .Table1 = Database.Helpers.GetMainViewName(Type),
                                                        .Field1 = Database.Helpers.GetMainIdName(Database.Helpers.GetMainViewName(Type)),
                                                        .Table2 = Database.Helpers.GetTableName(Database.TableName.streamdetail),
                                                        .Field2 = Database.Helpers.GetMainIdName(Database.TableName.streamdetail)
                                                        })
                        strRule = String.Format("{0}.{1}",
                                                Database.Helpers.GetTableName(nTargetTable.JoinTable),
                                                strRule)
                    End If
            End Select
            Return strRule
        End Function

        Private Function BuildClause_Where(ByVal Rules As List(Of Rule), ByVal Condition As Conditions) As String
            Dim lstRules As New List(Of String)
            For Each nRule In Rules
                Dim strRule As String = Nothing
                If Database.Helpers.ColumnIsInMainView(nRule.Field) Then
                    'the Field is in the main view, we don't need a join clause
                    strRule = BuildRule(nRule)
                Else
                    strRule = BuildClause_Join(nRule)
                End If
                If strRule IsNot Nothing Then lstRules.Add(strRule)
            Next
            If lstRules.Count > 0 Then
                Return String.Format("({0})", String.Join(String.Format(" {0} ", ConvertMatchToString(Condition)), lstRules.Where(Function(f) Not String.IsNullOrEmpty(f))))
            End If
            Return String.Empty
        End Function

        Private Function BuildRule(ByVal Rule As Rule) As String
            If Rule Is Nothing Then Return Nothing

            Dim strRule As String = String.Empty
            Select Case Rule.Operator
                Case Operators.After
                Case Operators.Before
                Case Operators.Between
                    If Rule.Value2 IsNot Nothing Then
                        If Database.Helpers.GetDataType(Rule.Field) = Database.DataType.String Then
                            strRule = String.Format("({0} >= '{1}' AND {0} <= '{2}')",
                                                    Database.Helpers.GetColumnName(Rule.Field),
                                                    Rule.Value,
                                                    Rule.Value2)
                        Else
                            strRule = String.Format("({0} >= {1} AND {0} <= {2})",
                                                    Database.Helpers.GetColumnName(Rule.Field),
                                                    Rule.Value,
                                                    Rule.Value2)
                        End If
                    End If
                Case Operators.Contains
                    strRule = String.Format("{0} LIKE '%{1}%'",
                                            Database.Helpers.GetColumnName(Rule.Field),
                                            StringUtils.ConvertToValidFilterString(Rule.Value.ToString))
                Case Operators.DoesNotContain
                    strRule = String.Format("{0} NOT LIKE '%{1}%'",
                                            Database.Helpers.GetColumnName(Rule.Field),
                                            StringUtils.ConvertToValidFilterString(Rule.Value.ToString))
                Case Operators.EndWith
                    strRule = String.Format("{0} LIKE '%{1}'",
                                            Database.Helpers.GetColumnName(Rule.Field),
                                            StringUtils.ConvertToValidFilterString(Rule.Value.ToString))
                Case Operators.False
                    strRule = String.Format("{0} = 0",
                                            Database.Helpers.GetColumnName(Rule.Field))
                Case Operators.GreaterThan
                    strRule = String.Format("{0} > {1}",
                                            Database.Helpers.GetColumnName(Rule.Field),
                                            Rule.Value)
                Case Operators.GreaterThanOrEqual
                    strRule = String.Format("{0} >= {1}",
                                            Database.Helpers.GetColumnName(Rule.Field),
                                            Rule.Value)
                Case Operators.Is
                    If Database.Helpers.GetDataType(Rule.Field) = Database.DataType.String Then
                        strRule = String.Format("{0} = '{1}'",
                                                Database.Helpers.GetColumnName(Rule.Field),
                                                StringUtils.ConvertToValidFilterString(Rule.Value.ToString))
                    Else
                        strRule = String.Format("{0} = {1}",
                                                Database.Helpers.GetColumnName(Rule.Field),
                                                Rule.Value)
                    End If
                Case Operators.IsNot
                    If Database.Helpers.GetDataType(Rule.Field) = Database.DataType.String Then
                        strRule = String.Format("NOT {0} = '{1}'",
                                                Database.Helpers.GetColumnName(Rule.Field),
                                                StringUtils.ConvertToValidFilterString(Rule.Value.ToString))
                    Else
                        strRule = String.Format("NOT {0} = {1}",
                                                Database.Helpers.GetColumnName(Rule.Field),
                                                Rule.Value)
                    End If
                Case Operators.IsNotNull
                    strRule = String.Format("{0} IS NOT NULL",
                                            Database.Helpers.GetColumnName(Rule.Field))
                Case Operators.IsNotNullOrEmpty
                    If Database.Helpers.GetDataType(Rule.Field) = Database.DataType.String Then
                        strRule = String.Format("({0} IS NOT NULL AND NOT {0} = '')",
                                                Database.Helpers.GetColumnName(Rule.Field))
                    Else
                        strRule = String.Format("({0} IS NOT NULL AND NOT {0} = 0)",
                                                Database.Helpers.GetColumnName(Rule.Field))
                    End If
                Case Operators.IsNull
                    strRule = String.Format("({0} IS NULL)",
                                            Database.Helpers.GetColumnName(Rule.Field))
                Case Operators.IsNullOrEmpty
                    If Database.Helpers.GetDataType(Rule.Field) = Database.DataType.String Then
                        strRule = String.Format("({0} IS NULL OR {0} = '')",
                                                Database.Helpers.GetColumnName(Rule.Field))
                    Else
                        strRule = String.Format("({0} IS NULL OR {0} = 0)",
                                                Database.Helpers.GetColumnName(Rule.Field))
                    End If
                Case Operators.LessThan
                    strRule = String.Format("{0} < {1}",
                                            Database.Helpers.GetColumnName(Rule.Field),
                                            Rule.Value)
                Case Operators.LessThanOrEqual
                    strRule = String.Format("{0} <= {1}",
                                            Database.Helpers.GetColumnName(Rule.Field),
                                            Rule.Value)
                Case Operators.NotBetween
                Case Operators.NotIn
                Case Operators.StartWith
                    strRule = String.Format("{0} LIKE '{1}%'",
                                            Database.Helpers.GetColumnName(Rule.Field),
                                            StringUtils.ConvertToValidFilterString(Rule.Value.ToString))
                Case Operators.True
                    strRule = String.Format("{0} = 1",
                                            Database.Helpers.GetColumnName(Rule.Field))
            End Select
            If Not String.IsNullOrEmpty(strRule) Then
                Return strRule
            Else
                Return Nothing
            End If
        End Function

        Private Sub BuildSqlMainQuery()
            Dim strViewName As String = Database.Helpers.GetMainViewName(Type)
            _sqlmainquery = String.Format("SELECT DISTINCT {0}.* FROM {0}", strViewName)
        End Sub

        Private Shared Function ConvertMatchToString(ByVal Match As Conditions) As String
            Select Case Match
                Case Conditions.All
                    Return "AND"
                Case Conditions.Any
                    Return "OR"
                Case Conditions.None
                    Return "AND NOT"
                Case Conditions.Not
                    Return "OR NOT"
                Case Conditions.NotAll
                    Return "AND NOT"
                Case Else
                    Return "AND"
            End Select
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
            RemoveAll(Database.ColumnName.KeyArtPath)
            RemoveAll(Database.ColumnName.LandscapePath)
            RemoveAll(Database.ColumnName.NfoPath)
            RemoveAll(Database.ColumnName.PosterPath)
            RemoveAll(Database.ColumnName.ThemePath)
            RemoveAll(Database.ColumnName.TrailerPath)
        End Sub

        Public Sub RemoveAllSearchbarFilters()
            RemoveAll(Database.ColumnName.ActorName)
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