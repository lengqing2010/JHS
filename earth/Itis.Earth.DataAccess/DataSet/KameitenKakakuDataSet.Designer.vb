﻿'------------------------------------------------------------------------------
' <auto-generated>
'     このコードはツールによって生成されました。
'     ランタイム バージョン:2.0.50727.832
'
'     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
'     コードが再生成されるときに損失したりします。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System


<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"),  _
 Serializable(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.ComponentModel.ToolboxItem(true),  _
 System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema"),  _
 System.Xml.Serialization.XmlRootAttribute("KameitenKakakuDataSet"),  _
 System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")>  _
Partial Public Class KameitenKakakuDataSet
    Inherits System.Data.DataSet
    
    Private tableKameitenMasterKakakuTable As KameitenMasterKakakuTableDataTable
    
    Private _schemaSerializationMode As System.Data.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Sub New()
        MyBase.New
        Me.BeginInit
        Me.InitClass
        Dim schemaChangedHandler As System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        AddHandler MyBase.Tables.CollectionChanged, schemaChangedHandler
        AddHandler MyBase.Relations.CollectionChanged, schemaChangedHandler
        Me.EndInit
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
        MyBase.New(info, context, false)
        If (Me.IsBinarySerialized(info, context) = true) Then
            Me.InitVars(false)
            Dim schemaChangedHandler1 As System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
            AddHandler Me.Tables.CollectionChanged, schemaChangedHandler1
            AddHandler Me.Relations.CollectionChanged, schemaChangedHandler1
            Return
        End If
        Dim strSchema As String = CType(info.GetValue("XmlSchema", GetType(String)),String)
        If (Me.DetermineSchemaSerializationMode(info, context) = System.Data.SchemaSerializationMode.IncludeSchema) Then
            Dim ds As System.Data.DataSet = New System.Data.DataSet
            ds.ReadXmlSchema(New System.Xml.XmlTextReader(New System.IO.StringReader(strSchema)))
            If (Not (ds.Tables("KameitenMasterKakakuTable")) Is Nothing) Then
                MyBase.Tables.Add(New KameitenMasterKakakuTableDataTable(ds.Tables("KameitenMasterKakakuTable")))
            End If
            Me.DataSetName = ds.DataSetName
            Me.Prefix = ds.Prefix
            Me.Namespace = ds.Namespace
            Me.Locale = ds.Locale
            Me.CaseSensitive = ds.CaseSensitive
            Me.EnforceConstraints = ds.EnforceConstraints
            Me.Merge(ds, false, System.Data.MissingSchemaAction.Add)
            Me.InitVars
        Else
            Me.ReadXmlSchema(New System.Xml.XmlTextReader(New System.IO.StringReader(strSchema)))
        End If
        Me.GetSerializationData(info, context)
        Dim schemaChangedHandler As System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        AddHandler MyBase.Tables.CollectionChanged, schemaChangedHandler
        AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.ComponentModel.Browsable(false),  _
     System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)>  _
    Public ReadOnly Property KameitenMasterKakakuTable() As KameitenMasterKakakuTableDataTable
        Get
            Return Me.tableKameitenMasterKakakuTable
        End Get
    End Property
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.ComponentModel.BrowsableAttribute(true),  _
     System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)>  _
    Public Overrides Property SchemaSerializationMode() As System.Data.SchemaSerializationMode
        Get
            Return Me._schemaSerializationMode
        End Get
        Set
            Me._schemaSerializationMode = value
        End Set
    End Property
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public Shadows ReadOnly Property Tables() As System.Data.DataTableCollection
        Get
            Return MyBase.Tables
        End Get
    End Property
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public Shadows ReadOnly Property Relations() As System.Data.DataRelationCollection
        Get
            Return MyBase.Relations
        End Get
    End Property
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Overrides Sub InitializeDerivedDataSet()
        Me.BeginInit
        Me.InitClass
        Me.EndInit
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Overrides Function Clone() As System.Data.DataSet
        Dim cln As KameitenKakakuDataSet = CType(MyBase.Clone,KameitenKakakuDataSet)
        cln.InitVars
        cln.SchemaSerializationMode = Me.SchemaSerializationMode
        Return cln
    End Function
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Overrides Function ShouldSerializeTables() As Boolean
        Return false
    End Function
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Overrides Function ShouldSerializeRelations() As Boolean
        Return false
    End Function
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Overrides Sub ReadXmlSerializable(ByVal reader As System.Xml.XmlReader)
        If (Me.DetermineSchemaSerializationMode(reader) = System.Data.SchemaSerializationMode.IncludeSchema) Then
            Me.Reset
            Dim ds As System.Data.DataSet = New System.Data.DataSet
            ds.ReadXml(reader)
            If (Not (ds.Tables("KameitenMasterKakakuTable")) Is Nothing) Then
                MyBase.Tables.Add(New KameitenMasterKakakuTableDataTable(ds.Tables("KameitenMasterKakakuTable")))
            End If
            Me.DataSetName = ds.DataSetName
            Me.Prefix = ds.Prefix
            Me.Namespace = ds.Namespace
            Me.Locale = ds.Locale
            Me.CaseSensitive = ds.CaseSensitive
            Me.EnforceConstraints = ds.EnforceConstraints
            Me.Merge(ds, false, System.Data.MissingSchemaAction.Add)
            Me.InitVars
        Else
            Me.ReadXml(reader)
            Me.InitVars
        End If
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Overrides Function GetSchemaSerializable() As System.Xml.Schema.XmlSchema
        Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream
        Me.WriteXmlSchema(New System.Xml.XmlTextWriter(stream, Nothing))
        stream.Position = 0
        Return System.Xml.Schema.XmlSchema.Read(New System.Xml.XmlTextReader(stream), Nothing)
    End Function
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Friend Overloads Sub InitVars()
        Me.InitVars(true)
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Friend Overloads Sub InitVars(ByVal initTable As Boolean)
        Me.tableKameitenMasterKakakuTable = CType(MyBase.Tables("KameitenMasterKakakuTable"),KameitenMasterKakakuTableDataTable)
        If (initTable = true) Then
            If (Not (Me.tableKameitenMasterKakakuTable) Is Nothing) Then
                Me.tableKameitenMasterKakakuTable.InitVars
            End If
        End If
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Sub InitClass()
        Me.DataSetName = "KameitenKakakuDataSet"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/KameitenKakakuDataSet.xsd"
        Me.EnforceConstraints = true
        Me.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        Me.tableKameitenMasterKakakuTable = New KameitenMasterKakakuTableDataTable
        MyBase.Tables.Add(Me.tableKameitenMasterKakakuTable)
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Function ShouldSerializeKameitenMasterKakakuTable() As Boolean
        Return false
    End Function
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Sub SchemaChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CollectionChangeEventArgs)
        If (e.Action = System.ComponentModel.CollectionChangeAction.Remove) Then
            Me.InitVars
        End If
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Shared Function GetTypedDataSetSchema(ByVal xs As System.Xml.Schema.XmlSchemaSet) As System.Xml.Schema.XmlSchemaComplexType
        Dim ds As KameitenKakakuDataSet = New KameitenKakakuDataSet
        Dim type As System.Xml.Schema.XmlSchemaComplexType = New System.Xml.Schema.XmlSchemaComplexType
        Dim sequence As System.Xml.Schema.XmlSchemaSequence = New System.Xml.Schema.XmlSchemaSequence
        xs.Add(ds.GetSchemaSerializable)
        Dim any As System.Xml.Schema.XmlSchemaAny = New System.Xml.Schema.XmlSchemaAny
        any.Namespace = ds.Namespace
        sequence.Items.Add(any)
        type.Particle = sequence
        Return type
    End Function
    
    Public Delegate Sub KameitenMasterKakakuTableRowChangeEventHandler(ByVal sender As Object, ByVal e As KameitenMasterKakakuTableRowChangeEvent)
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"),  _
     System.Serializable(),  _
     System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>  _
    Partial Public Class KameitenMasterKakakuTableDataTable
        Inherits System.Data.DataTable
        Implements System.Collections.IEnumerable
        
        Private columnkbn As System.Data.DataColumn
        
        Private columnkameiten_cd As System.Data.DataColumn
        
        Private columnkameiten_master_kakaku As System.Data.DataColumn
        
        Private columntatemono_youto As System.Data.DataColumn
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New()
            MyBase.New
            Me.TableName = "KameitenMasterKakakuTable"
            Me.BeginInit
            Me.InitClass
            Me.EndInit
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub New(ByVal table As System.Data.DataTable)
            MyBase.New
            Me.TableName = table.TableName
            If (table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                Me.CaseSensitive = table.CaseSensitive
            End If
            If (table.Locale.ToString <> table.DataSet.Locale.ToString) Then
                Me.Locale = table.Locale
            End If
            If (table.Namespace <> table.DataSet.Namespace) Then
                Me.Namespace = table.Namespace
            End If
            Me.Prefix = table.Prefix
            Me.MinimumCapacity = table.MinimumCapacity
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
            Me.InitVars
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property kbnColumn() As System.Data.DataColumn
            Get
                Return Me.columnkbn
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property kameiten_cdColumn() As System.Data.DataColumn
            Get
                Return Me.columnkameiten_cd
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property kameiten_master_kakakuColumn() As System.Data.DataColumn
            Get
                Return Me.columnkameiten_master_kakaku
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property tatemono_youtoColumn() As System.Data.DataColumn
            Get
                Return Me.columntatemono_youto
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         System.ComponentModel.Browsable(false)>  _
        Public ReadOnly Property Count() As Integer
            Get
                Return Me.Rows.Count
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Default ReadOnly Property Item(ByVal index As Integer) As KameitenMasterKakakuTableRow
            Get
                Return CType(Me.Rows(index),KameitenMasterKakakuTableRow)
            End Get
        End Property
        
        Public Event KameitenMasterKakakuTableRowChanging As KameitenMasterKakakuTableRowChangeEventHandler
        
        Public Event KameitenMasterKakakuTableRowChanged As KameitenMasterKakakuTableRowChangeEventHandler
        
        Public Event KameitenMasterKakakuTableRowDeleting As KameitenMasterKakakuTableRowChangeEventHandler
        
        Public Event KameitenMasterKakakuTableRowDeleted As KameitenMasterKakakuTableRowChangeEventHandler
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Sub AddKameitenMasterKakakuTableRow(ByVal row As KameitenMasterKakakuTableRow)
            Me.Rows.Add(row)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Function AddKameitenMasterKakakuTableRow(ByVal kbn As String, ByVal kameiten_cd As String, ByVal kameiten_master_kakaku As Integer, ByVal tatemono_youto As Integer) As KameitenMasterKakakuTableRow
            Dim rowKameitenMasterKakakuTableRow As KameitenMasterKakakuTableRow = CType(Me.NewRow,KameitenMasterKakakuTableRow)
            rowKameitenMasterKakakuTableRow.ItemArray = New Object() {kbn, kameiten_cd, kameiten_master_kakaku, tatemono_youto}
            Me.Rows.Add(rowKameitenMasterKakakuTableRow)
            Return rowKameitenMasterKakakuTableRow
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function FindBykameiten_cd(ByVal kameiten_cd As String) As KameitenMasterKakakuTableRow
            Return CType(Me.Rows.Find(New Object() {kameiten_cd}),KameitenMasterKakakuTableRow)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overridable Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overrides Function Clone() As System.Data.DataTable
            Dim cln As KameitenMasterKakakuTableDataTable = CType(MyBase.Clone,KameitenMasterKakakuTableDataTable)
            cln.InitVars
            Return cln
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function CreateInstance() As System.Data.DataTable
            Return New KameitenMasterKakakuTableDataTable
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub InitVars()
            Me.columnkbn = MyBase.Columns("kbn")
            Me.columnkameiten_cd = MyBase.Columns("kameiten_cd")
            Me.columnkameiten_master_kakaku = MyBase.Columns("kameiten_master_kakaku")
            Me.columntatemono_youto = MyBase.Columns("tatemono_youto")
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Private Sub InitClass()
            Me.columnkbn = New System.Data.DataColumn("kbn", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnkbn)
            Me.columnkameiten_cd = New System.Data.DataColumn("kameiten_cd", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnkameiten_cd)
            Me.columnkameiten_master_kakaku = New System.Data.DataColumn("kameiten_master_kakaku", GetType(Integer), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnkameiten_master_kakaku)
            Me.columntatemono_youto = New System.Data.DataColumn("tatemono_youto", GetType(Integer), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columntatemono_youto)
            Me.Constraints.Add(New System.Data.UniqueConstraint("Constraint1", New System.Data.DataColumn() {Me.columnkameiten_cd}, true))
            Me.columnkbn.AllowDBNull = false
            Me.columnkbn.MaxLength = 1
            Me.columnkameiten_cd.AllowDBNull = false
            Me.columnkameiten_cd.Unique = true
            Me.columnkameiten_cd.MaxLength = 5
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function NewKameitenMasterKakakuTableRow() As KameitenMasterKakakuTableRow
            Return CType(Me.NewRow,KameitenMasterKakakuTableRow)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function NewRowFromBuilder(ByVal builder As System.Data.DataRowBuilder) As System.Data.DataRow
            Return New KameitenMasterKakakuTableRow(builder)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(KameitenMasterKakakuTableRow)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanged(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.KameitenMasterKakakuTableRowChangedEvent) Is Nothing) Then
                RaiseEvent KameitenMasterKakakuTableRowChanged(Me, New KameitenMasterKakakuTableRowChangeEvent(CType(e.Row,KameitenMasterKakakuTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanging(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.KameitenMasterKakakuTableRowChangingEvent) Is Nothing) Then
                RaiseEvent KameitenMasterKakakuTableRowChanging(Me, New KameitenMasterKakakuTableRowChangeEvent(CType(e.Row,KameitenMasterKakakuTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleted(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.KameitenMasterKakakuTableRowDeletedEvent) Is Nothing) Then
                RaiseEvent KameitenMasterKakakuTableRowDeleted(Me, New KameitenMasterKakakuTableRowChangeEvent(CType(e.Row,KameitenMasterKakakuTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleting(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.KameitenMasterKakakuTableRowDeletingEvent) Is Nothing) Then
                RaiseEvent KameitenMasterKakakuTableRowDeleting(Me, New KameitenMasterKakakuTableRowChangeEvent(CType(e.Row,KameitenMasterKakakuTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub RemoveKameitenMasterKakakuTableRow(ByVal row As KameitenMasterKakakuTableRow)
            Me.Rows.Remove(row)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Shared Function GetTypedTableSchema(ByVal xs As System.Xml.Schema.XmlSchemaSet) As System.Xml.Schema.XmlSchemaComplexType
            Dim type As System.Xml.Schema.XmlSchemaComplexType = New System.Xml.Schema.XmlSchemaComplexType
            Dim sequence As System.Xml.Schema.XmlSchemaSequence = New System.Xml.Schema.XmlSchemaSequence
            Dim ds As KameitenKakakuDataSet = New KameitenKakakuDataSet
            xs.Add(ds.GetSchemaSerializable)
            Dim any1 As System.Xml.Schema.XmlSchemaAny = New System.Xml.Schema.XmlSchemaAny
            any1.Namespace = "http://www.w3.org/2001/XMLSchema"
            any1.MinOccurs = New Decimal(0)
            any1.MaxOccurs = Decimal.MaxValue
            any1.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax
            sequence.Items.Add(any1)
            Dim any2 As System.Xml.Schema.XmlSchemaAny = New System.Xml.Schema.XmlSchemaAny
            any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1"
            any2.MinOccurs = New Decimal(1)
            any2.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax
            sequence.Items.Add(any2)
            Dim attribute1 As System.Xml.Schema.XmlSchemaAttribute = New System.Xml.Schema.XmlSchemaAttribute
            attribute1.Name = "namespace"
            attribute1.FixedValue = ds.Namespace
            type.Attributes.Add(attribute1)
            Dim attribute2 As System.Xml.Schema.XmlSchemaAttribute = New System.Xml.Schema.XmlSchemaAttribute
            attribute2.Name = "tableTypeName"
            attribute2.FixedValue = "KameitenMasterKakakuTableDataTable"
            type.Attributes.Add(attribute2)
            type.Particle = sequence
            Return type
        End Function
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Partial Public Class KameitenMasterKakakuTableRow
        Inherits System.Data.DataRow
        
        Private tableKameitenMasterKakakuTable As KameitenMasterKakakuTableDataTable
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub New(ByVal rb As System.Data.DataRowBuilder)
            MyBase.New(rb)
            Me.tableKameitenMasterKakakuTable = CType(Me.Table,KameitenMasterKakakuTableDataTable)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property kbn() As String
            Get
                Return CType(Me(Me.tableKameitenMasterKakakuTable.kbnColumn),String)
            End Get
            Set
                Me(Me.tableKameitenMasterKakakuTable.kbnColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property kameiten_cd() As String
            Get
                Return CType(Me(Me.tableKameitenMasterKakakuTable.kameiten_cdColumn),String)
            End Get
            Set
                Me(Me.tableKameitenMasterKakakuTable.kameiten_cdColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property kameiten_master_kakaku() As Integer
            Get
                Try 
                    Return CType(Me(Me.tableKameitenMasterKakakuTable.kameiten_master_kakakuColumn),Integer)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'KameitenMasterKakakuTable' にある列 'kameiten_master_kakaku' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableKameitenMasterKakakuTable.kameiten_master_kakakuColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property tatemono_youto() As Integer
            Get
                Try 
                    Return CType(Me(Me.tableKameitenMasterKakakuTable.tatemono_youtoColumn),Integer)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'KameitenMasterKakakuTable' にある列 'tatemono_youto' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableKameitenMasterKakakuTable.tatemono_youtoColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Iskameiten_master_kakakuNull() As Boolean
            Return Me.IsNull(Me.tableKameitenMasterKakakuTable.kameiten_master_kakakuColumn)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Setkameiten_master_kakakuNull()
            Me(Me.tableKameitenMasterKakakuTable.kameiten_master_kakakuColumn) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Istatemono_youtoNull() As Boolean
            Return Me.IsNull(Me.tableKameitenMasterKakakuTable.tatemono_youtoColumn)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Settatemono_youtoNull()
            Me(Me.tableKameitenMasterKakakuTable.tatemono_youtoColumn) = System.Convert.DBNull
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Public Class KameitenMasterKakakuTableRowChangeEvent
        Inherits System.EventArgs
        
        Private eventRow As KameitenMasterKakakuTableRow
        
        Private eventAction As System.Data.DataRowAction
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New(ByVal row As KameitenMasterKakakuTableRow, ByVal action As System.Data.DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Row() As KameitenMasterKakakuTableRow
            Get
                Return Me.eventRow
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Action() As System.Data.DataRowAction
            Get
                Return Me.eventAction
            End Get
        End Property
    End Class
End Class
