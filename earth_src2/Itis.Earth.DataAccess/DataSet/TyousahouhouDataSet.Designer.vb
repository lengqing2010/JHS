﻿'------------------------------------------------------------------------------
' <auto-generated>
'     このコードはツールによって生成されました。
'     ランタイム バージョン:2.0.50727.42
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
 System.Xml.Serialization.XmlRootAttribute("TyousahouhouDataSet"),  _
 System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")>  _
Partial Public Class TyousahouhouDataSet
    Inherits System.Data.DataSet
    
    Private tableTyousahouhouTable As TyousahouhouTableDataTable
    
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
            If (Not (ds.Tables("TyousahouhouTable")) Is Nothing) Then
                MyBase.Tables.Add(New TyousahouhouTableDataTable(ds.Tables("TyousahouhouTable")))
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
    Public ReadOnly Property TyousahouhouTable() As TyousahouhouTableDataTable
        Get
            Return Me.tableTyousahouhouTable
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
        Dim cln As TyousahouhouDataSet = CType(MyBase.Clone,TyousahouhouDataSet)
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
            If (Not (ds.Tables("TyousahouhouTable")) Is Nothing) Then
                MyBase.Tables.Add(New TyousahouhouTableDataTable(ds.Tables("TyousahouhouTable")))
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
        Me.tableTyousahouhouTable = CType(MyBase.Tables("TyousahouhouTable"),TyousahouhouTableDataTable)
        If (initTable = true) Then
            If (Not (Me.tableTyousahouhouTable) Is Nothing) Then
                Me.tableTyousahouhouTable.InitVars
            End If
        End If
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Sub InitClass()
        Me.DataSetName = "TyousahouhouDataSet"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/TyousahouhouDataSet.xsd"
        Me.EnforceConstraints = true
        Me.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        Me.tableTyousahouhouTable = New TyousahouhouTableDataTable
        MyBase.Tables.Add(Me.tableTyousahouhouTable)
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Function ShouldSerializeTyousahouhouTable() As Boolean
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
        Dim ds As TyousahouhouDataSet = New TyousahouhouDataSet
        Dim type As System.Xml.Schema.XmlSchemaComplexType = New System.Xml.Schema.XmlSchemaComplexType
        Dim sequence As System.Xml.Schema.XmlSchemaSequence = New System.Xml.Schema.XmlSchemaSequence
        xs.Add(ds.GetSchemaSerializable)
        Dim any As System.Xml.Schema.XmlSchemaAny = New System.Xml.Schema.XmlSchemaAny
        any.Namespace = ds.Namespace
        sequence.Items.Add(any)
        type.Particle = sequence
        Return type
    End Function
    
    Public Delegate Sub TyousahouhouTableRowChangeEventHandler(ByVal sender As Object, ByVal e As TyousahouhouTableRowChangeEvent)
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"),  _
     System.Serializable(),  _
     System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>  _
    Partial Public Class TyousahouhouTableDataTable
        Inherits System.Data.DataTable
        Implements System.Collections.IEnumerable
        
        Private columntys_houhou_no As System.Data.DataColumn
        
        Private columntys_houhou_mei_ryaku As System.Data.DataColumn
        
        Private columntys_houhou_mei As System.Data.DataColumn
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New()
            MyBase.New
            Me.TableName = "TyousahouhouTable"
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
        Public ReadOnly Property tys_houhou_noColumn() As System.Data.DataColumn
            Get
                Return Me.columntys_houhou_no
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property tys_houhou_mei_ryakuColumn() As System.Data.DataColumn
            Get
                Return Me.columntys_houhou_mei_ryaku
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property tys_houhou_meiColumn() As System.Data.DataColumn
            Get
                Return Me.columntys_houhou_mei
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
        Public Default ReadOnly Property Item(ByVal index As Integer) As TyousahouhouTableRow
            Get
                Return CType(Me.Rows(index),TyousahouhouTableRow)
            End Get
        End Property
        
        Public Event TyousahouhouTableRowChanging As TyousahouhouTableRowChangeEventHandler
        
        Public Event TyousahouhouTableRowChanged As TyousahouhouTableRowChangeEventHandler
        
        Public Event TyousahouhouTableRowDeleting As TyousahouhouTableRowChangeEventHandler
        
        Public Event TyousahouhouTableRowDeleted As TyousahouhouTableRowChangeEventHandler
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Sub AddTyousahouhouTableRow(ByVal row As TyousahouhouTableRow)
            Me.Rows.Add(row)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Function AddTyousahouhouTableRow(ByVal tys_houhou_no As Integer, ByVal tys_houhou_mei_ryaku As String, ByVal tys_houhou_mei As String) As TyousahouhouTableRow
            Dim rowTyousahouhouTableRow As TyousahouhouTableRow = CType(Me.NewRow,TyousahouhouTableRow)
            rowTyousahouhouTableRow.ItemArray = New Object() {tys_houhou_no, tys_houhou_mei_ryaku, tys_houhou_mei}
            Me.Rows.Add(rowTyousahouhouTableRow)
            Return rowTyousahouhouTableRow
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overridable Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overrides Function Clone() As System.Data.DataTable
            Dim cln As TyousahouhouTableDataTable = CType(MyBase.Clone,TyousahouhouTableDataTable)
            cln.InitVars
            Return cln
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function CreateInstance() As System.Data.DataTable
            Return New TyousahouhouTableDataTable
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub InitVars()
            Me.columntys_houhou_no = MyBase.Columns("tys_houhou_no")
            Me.columntys_houhou_mei_ryaku = MyBase.Columns("tys_houhou_mei_ryaku")
            Me.columntys_houhou_mei = MyBase.Columns("tys_houhou_mei")
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Private Sub InitClass()
            Me.columntys_houhou_no = New System.Data.DataColumn("tys_houhou_no", GetType(Integer), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columntys_houhou_no)
            Me.columntys_houhou_mei_ryaku = New System.Data.DataColumn("tys_houhou_mei_ryaku", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columntys_houhou_mei_ryaku)
            Me.columntys_houhou_mei = New System.Data.DataColumn("tys_houhou_mei", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columntys_houhou_mei)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function NewTyousahouhouTableRow() As TyousahouhouTableRow
            Return CType(Me.NewRow,TyousahouhouTableRow)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function NewRowFromBuilder(ByVal builder As System.Data.DataRowBuilder) As System.Data.DataRow
            Return New TyousahouhouTableRow(builder)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(TyousahouhouTableRow)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanged(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.TyousahouhouTableRowChangedEvent) Is Nothing) Then
                RaiseEvent TyousahouhouTableRowChanged(Me, New TyousahouhouTableRowChangeEvent(CType(e.Row,TyousahouhouTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanging(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.TyousahouhouTableRowChangingEvent) Is Nothing) Then
                RaiseEvent TyousahouhouTableRowChanging(Me, New TyousahouhouTableRowChangeEvent(CType(e.Row,TyousahouhouTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleted(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.TyousahouhouTableRowDeletedEvent) Is Nothing) Then
                RaiseEvent TyousahouhouTableRowDeleted(Me, New TyousahouhouTableRowChangeEvent(CType(e.Row,TyousahouhouTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleting(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.TyousahouhouTableRowDeletingEvent) Is Nothing) Then
                RaiseEvent TyousahouhouTableRowDeleting(Me, New TyousahouhouTableRowChangeEvent(CType(e.Row,TyousahouhouTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub RemoveTyousahouhouTableRow(ByVal row As TyousahouhouTableRow)
            Me.Rows.Remove(row)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Shared Function GetTypedTableSchema(ByVal xs As System.Xml.Schema.XmlSchemaSet) As System.Xml.Schema.XmlSchemaComplexType
            Dim type As System.Xml.Schema.XmlSchemaComplexType = New System.Xml.Schema.XmlSchemaComplexType
            Dim sequence As System.Xml.Schema.XmlSchemaSequence = New System.Xml.Schema.XmlSchemaSequence
            Dim ds As TyousahouhouDataSet = New TyousahouhouDataSet
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
            attribute2.FixedValue = "TyousahouhouTableDataTable"
            type.Attributes.Add(attribute2)
            type.Particle = sequence
            Return type
        End Function
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Partial Public Class TyousahouhouTableRow
        Inherits System.Data.DataRow
        
        Private tableTyousahouhouTable As TyousahouhouTableDataTable
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub New(ByVal rb As System.Data.DataRowBuilder)
            MyBase.New(rb)
            Me.tableTyousahouhouTable = CType(Me.Table,TyousahouhouTableDataTable)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property tys_houhou_no() As Integer
            Get
                Try 
                    Return CType(Me(Me.tableTyousahouhouTable.tys_houhou_noColumn),Integer)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'TyousahouhouTable' にある列 'tys_houhou_no' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableTyousahouhouTable.tys_houhou_noColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property tys_houhou_mei_ryaku() As String
            Get
                Try 
                    Return CType(Me(Me.tableTyousahouhouTable.tys_houhou_mei_ryakuColumn),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'TyousahouhouTable' にある列 'tys_houhou_mei_ryaku' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableTyousahouhouTable.tys_houhou_mei_ryakuColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property tys_houhou_mei() As String
            Get
                Try 
                    Return CType(Me(Me.tableTyousahouhouTable.tys_houhou_meiColumn),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'TyousahouhouTable' にある列 'tys_houhou_mei' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableTyousahouhouTable.tys_houhou_meiColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Istys_houhou_noNull() As Boolean
            Return Me.IsNull(Me.tableTyousahouhouTable.tys_houhou_noColumn)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Settys_houhou_noNull()
            Me(Me.tableTyousahouhouTable.tys_houhou_noColumn) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Istys_houhou_mei_ryakuNull() As Boolean
            Return Me.IsNull(Me.tableTyousahouhouTable.tys_houhou_mei_ryakuColumn)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Settys_houhou_mei_ryakuNull()
            Me(Me.tableTyousahouhouTable.tys_houhou_mei_ryakuColumn) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Istys_houhou_meiNull() As Boolean
            Return Me.IsNull(Me.tableTyousahouhouTable.tys_houhou_meiColumn)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Settys_houhou_meiNull()
            Me(Me.tableTyousahouhouTable.tys_houhou_meiColumn) = System.Convert.DBNull
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Public Class TyousahouhouTableRowChangeEvent
        Inherits System.EventArgs
        
        Private eventRow As TyousahouhouTableRow
        
        Private eventAction As System.Data.DataRowAction
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New(ByVal row As TyousahouhouTableRow, ByVal action As System.Data.DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Row() As TyousahouhouTableRow
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
