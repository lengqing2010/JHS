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
 System.Xml.Serialization.XmlRootAttribute("YosinJyouhouDetailsDataSet"),  _
 System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")>  _
Partial Public Class YosinJyouhouDetailsDataSet
    Inherits System.Data.DataSet
    
    Private tableYosinKanriInfoTable As YosinKanriInfoTableDataTable
    
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
            If (Not (ds.Tables("YosinKanriInfoTable")) Is Nothing) Then
                MyBase.Tables.Add(New YosinKanriInfoTableDataTable(ds.Tables("YosinKanriInfoTable")))
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
    Public ReadOnly Property YosinKanriInfoTable() As YosinKanriInfoTableDataTable
        Get
            Return Me.tableYosinKanriInfoTable
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
        Dim cln As YosinJyouhouDetailsDataSet = CType(MyBase.Clone,YosinJyouhouDetailsDataSet)
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
            If (Not (ds.Tables("YosinKanriInfoTable")) Is Nothing) Then
                MyBase.Tables.Add(New YosinKanriInfoTableDataTable(ds.Tables("YosinKanriInfoTable")))
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
        Me.tableYosinKanriInfoTable = CType(MyBase.Tables("YosinKanriInfoTable"),YosinKanriInfoTableDataTable)
        If (initTable = true) Then
            If (Not (Me.tableYosinKanriInfoTable) Is Nothing) Then
                Me.tableYosinKanriInfoTable.InitVars
            End If
        End If
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Sub InitClass()
        Me.DataSetName = "YosinJyouhouDetailsDataSet"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/YosinJyouhouDetailsDataSet.xsd"
        Me.EnforceConstraints = true
        Me.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        Me.tableYosinKanriInfoTable = New YosinKanriInfoTableDataTable
        MyBase.Tables.Add(Me.tableYosinKanriInfoTable)
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Function ShouldSerializeYosinKanriInfoTable() As Boolean
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
        Dim ds As YosinJyouhouDetailsDataSet = New YosinJyouhouDetailsDataSet
        Dim type As System.Xml.Schema.XmlSchemaComplexType = New System.Xml.Schema.XmlSchemaComplexType
        Dim sequence As System.Xml.Schema.XmlSchemaSequence = New System.Xml.Schema.XmlSchemaSequence
        xs.Add(ds.GetSchemaSerializable)
        Dim any As System.Xml.Schema.XmlSchemaAny = New System.Xml.Schema.XmlSchemaAny
        any.Namespace = ds.Namespace
        sequence.Items.Add(any)
        type.Particle = sequence
        Return type
    End Function
    
    Public Delegate Sub YosinKanriInfoTableRowChangeEventHandler(ByVal sender As Object, ByVal e As YosinKanriInfoTableRowChangeEvent)
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"),  _
     System.Serializable(),  _
     System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>  _
    Partial Public Class YosinKanriInfoTableDataTable
        Inherits System.Data.DataTable
        Implements System.Collections.IEnumerable
        
        Private columnnayose_cd As System.Data.DataColumn
        
        Private columnnayose_mei1 As System.Data.DataColumn
        
        Private columnnayose_mei2 As System.Data.DataColumn
        
        Private columnnayose_kana1 As System.Data.DataColumn
        
        Private columnnayose_kana2 As System.Data.DataColumn
        
        Private columnnayose_yosin_gaku As System.Data.DataColumn
        
        Private columntodouhuken_cd As System.Data.DataColumn
        
        Private columnkeikoku_jyoukyou As System.Data.DataColumn
        
        Private columnmail_datetime As System.Data.DataColumn
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New()
            MyBase.New
            Me.TableName = "YosinKanriInfoTable"
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
        Public ReadOnly Property nayose_cdColumn() As System.Data.DataColumn
            Get
                Return Me.columnnayose_cd
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property nayose_mei1Column() As System.Data.DataColumn
            Get
                Return Me.columnnayose_mei1
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property nayose_mei2Column() As System.Data.DataColumn
            Get
                Return Me.columnnayose_mei2
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property nayose_kana1Column() As System.Data.DataColumn
            Get
                Return Me.columnnayose_kana1
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property nayose_kana2Column() As System.Data.DataColumn
            Get
                Return Me.columnnayose_kana2
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property nayose_yosin_gakuColumn() As System.Data.DataColumn
            Get
                Return Me.columnnayose_yosin_gaku
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property todouhuken_cdColumn() As System.Data.DataColumn
            Get
                Return Me.columntodouhuken_cd
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property keikoku_jyoukyouColumn() As System.Data.DataColumn
            Get
                Return Me.columnkeikoku_jyoukyou
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property mail_datetimeColumn() As System.Data.DataColumn
            Get
                Return Me.columnmail_datetime
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
        Public Default ReadOnly Property Item(ByVal index As Integer) As YosinKanriInfoTableRow
            Get
                Return CType(Me.Rows(index),YosinKanriInfoTableRow)
            End Get
        End Property
        
        Public Event YosinKanriInfoTableRowChanging As YosinKanriInfoTableRowChangeEventHandler
        
        Public Event YosinKanriInfoTableRowChanged As YosinKanriInfoTableRowChangeEventHandler
        
        Public Event YosinKanriInfoTableRowDeleting As YosinKanriInfoTableRowChangeEventHandler
        
        Public Event YosinKanriInfoTableRowDeleted As YosinKanriInfoTableRowChangeEventHandler
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Sub AddYosinKanriInfoTableRow(ByVal row As YosinKanriInfoTableRow)
            Me.Rows.Add(row)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Function AddYosinKanriInfoTableRow(ByVal nayose_cd As String, ByVal nayose_mei1 As String, ByVal nayose_mei2 As String, ByVal nayose_kana1 As String, ByVal nayose_kana2 As String, ByVal nayose_yosin_gaku As String, ByVal todouhuken_cd As String, ByVal keikoku_jyoukyou As String, ByVal mail_datetime As String) As YosinKanriInfoTableRow
            Dim rowYosinKanriInfoTableRow As YosinKanriInfoTableRow = CType(Me.NewRow,YosinKanriInfoTableRow)
            rowYosinKanriInfoTableRow.ItemArray = New Object() {nayose_cd, nayose_mei1, nayose_mei2, nayose_kana1, nayose_kana2, nayose_yosin_gaku, todouhuken_cd, keikoku_jyoukyou, mail_datetime}
            Me.Rows.Add(rowYosinKanriInfoTableRow)
            Return rowYosinKanriInfoTableRow
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overridable Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overrides Function Clone() As System.Data.DataTable
            Dim cln As YosinKanriInfoTableDataTable = CType(MyBase.Clone,YosinKanriInfoTableDataTable)
            cln.InitVars
            Return cln
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function CreateInstance() As System.Data.DataTable
            Return New YosinKanriInfoTableDataTable
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub InitVars()
            Me.columnnayose_cd = MyBase.Columns("nayose_cd")
            Me.columnnayose_mei1 = MyBase.Columns("nayose_mei1")
            Me.columnnayose_mei2 = MyBase.Columns("nayose_mei2")
            Me.columnnayose_kana1 = MyBase.Columns("nayose_kana1")
            Me.columnnayose_kana2 = MyBase.Columns("nayose_kana2")
            Me.columnnayose_yosin_gaku = MyBase.Columns("nayose_yosin_gaku")
            Me.columntodouhuken_cd = MyBase.Columns("todouhuken_cd")
            Me.columnkeikoku_jyoukyou = MyBase.Columns("keikoku_jyoukyou")
            Me.columnmail_datetime = MyBase.Columns("mail_datetime")
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Private Sub InitClass()
            Me.columnnayose_cd = New System.Data.DataColumn("nayose_cd", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnnayose_cd)
            Me.columnnayose_mei1 = New System.Data.DataColumn("nayose_mei1", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnnayose_mei1)
            Me.columnnayose_mei2 = New System.Data.DataColumn("nayose_mei2", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnnayose_mei2)
            Me.columnnayose_kana1 = New System.Data.DataColumn("nayose_kana1", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnnayose_kana1)
            Me.columnnayose_kana2 = New System.Data.DataColumn("nayose_kana2", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnnayose_kana2)
            Me.columnnayose_yosin_gaku = New System.Data.DataColumn("nayose_yosin_gaku", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnnayose_yosin_gaku)
            Me.columntodouhuken_cd = New System.Data.DataColumn("todouhuken_cd", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columntodouhuken_cd)
            Me.columnkeikoku_jyoukyou = New System.Data.DataColumn("keikoku_jyoukyou", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnkeikoku_jyoukyou)
            Me.columnmail_datetime = New System.Data.DataColumn("mail_datetime", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnmail_datetime)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function NewYosinKanriInfoTableRow() As YosinKanriInfoTableRow
            Return CType(Me.NewRow,YosinKanriInfoTableRow)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function NewRowFromBuilder(ByVal builder As System.Data.DataRowBuilder) As System.Data.DataRow
            Return New YosinKanriInfoTableRow(builder)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(YosinKanriInfoTableRow)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanged(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.YosinKanriInfoTableRowChangedEvent) Is Nothing) Then
                RaiseEvent YosinKanriInfoTableRowChanged(Me, New YosinKanriInfoTableRowChangeEvent(CType(e.Row,YosinKanriInfoTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanging(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.YosinKanriInfoTableRowChangingEvent) Is Nothing) Then
                RaiseEvent YosinKanriInfoTableRowChanging(Me, New YosinKanriInfoTableRowChangeEvent(CType(e.Row,YosinKanriInfoTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleted(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.YosinKanriInfoTableRowDeletedEvent) Is Nothing) Then
                RaiseEvent YosinKanriInfoTableRowDeleted(Me, New YosinKanriInfoTableRowChangeEvent(CType(e.Row,YosinKanriInfoTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleting(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.YosinKanriInfoTableRowDeletingEvent) Is Nothing) Then
                RaiseEvent YosinKanriInfoTableRowDeleting(Me, New YosinKanriInfoTableRowChangeEvent(CType(e.Row,YosinKanriInfoTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub RemoveYosinKanriInfoTableRow(ByVal row As YosinKanriInfoTableRow)
            Me.Rows.Remove(row)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Shared Function GetTypedTableSchema(ByVal xs As System.Xml.Schema.XmlSchemaSet) As System.Xml.Schema.XmlSchemaComplexType
            Dim type As System.Xml.Schema.XmlSchemaComplexType = New System.Xml.Schema.XmlSchemaComplexType
            Dim sequence As System.Xml.Schema.XmlSchemaSequence = New System.Xml.Schema.XmlSchemaSequence
            Dim ds As YosinJyouhouDetailsDataSet = New YosinJyouhouDetailsDataSet
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
            attribute2.FixedValue = "YosinKanriInfoTableDataTable"
            type.Attributes.Add(attribute2)
            type.Particle = sequence
            Return type
        End Function
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Partial Public Class YosinKanriInfoTableRow
        Inherits System.Data.DataRow
        
        Private tableYosinKanriInfoTable As YosinKanriInfoTableDataTable
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub New(ByVal rb As System.Data.DataRowBuilder)
            MyBase.New(rb)
            Me.tableYosinKanriInfoTable = CType(Me.Table,YosinKanriInfoTableDataTable)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property nayose_cd() As String
            Get
                Try 
                    Return CType(Me(Me.tableYosinKanriInfoTable.nayose_cdColumn),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'YosinKanriInfoTable' にある列 'nayose_cd' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableYosinKanriInfoTable.nayose_cdColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property nayose_mei1() As String
            Get
                Try 
                    Return CType(Me(Me.tableYosinKanriInfoTable.nayose_mei1Column),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'YosinKanriInfoTable' にある列 'nayose_mei1' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableYosinKanriInfoTable.nayose_mei1Column) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property nayose_mei2() As String
            Get
                Try 
                    Return CType(Me(Me.tableYosinKanriInfoTable.nayose_mei2Column),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'YosinKanriInfoTable' にある列 'nayose_mei2' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableYosinKanriInfoTable.nayose_mei2Column) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property nayose_kana1() As String
            Get
                Try 
                    Return CType(Me(Me.tableYosinKanriInfoTable.nayose_kana1Column),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'YosinKanriInfoTable' にある列 'nayose_kana1' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableYosinKanriInfoTable.nayose_kana1Column) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property nayose_kana2() As String
            Get
                Try 
                    Return CType(Me(Me.tableYosinKanriInfoTable.nayose_kana2Column),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'YosinKanriInfoTable' にある列 'nayose_kana2' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableYosinKanriInfoTable.nayose_kana2Column) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property nayose_yosin_gaku() As String
            Get
                Try 
                    Return CType(Me(Me.tableYosinKanriInfoTable.nayose_yosin_gakuColumn),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'YosinKanriInfoTable' にある列 'nayose_yosin_gaku' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableYosinKanriInfoTable.nayose_yosin_gakuColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property todouhuken_cd() As String
            Get
                Try 
                    Return CType(Me(Me.tableYosinKanriInfoTable.todouhuken_cdColumn),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'YosinKanriInfoTable' にある列 'todouhuken_cd' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableYosinKanriInfoTable.todouhuken_cdColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property keikoku_jyoukyou() As String
            Get
                Try 
                    Return CType(Me(Me.tableYosinKanriInfoTable.keikoku_jyoukyouColumn),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'YosinKanriInfoTable' にある列 'keikoku_jyoukyou' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableYosinKanriInfoTable.keikoku_jyoukyouColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property mail_datetime() As String
            Get
                Try 
                    Return CType(Me(Me.tableYosinKanriInfoTable.mail_datetimeColumn),String)
                Catch e As System.InvalidCastException
                    Throw New System.Data.StrongTypingException("テーブル 'YosinKanriInfoTable' にある列 'mail_datetime' の値は DBNull です。", e)
                End Try
            End Get
            Set
                Me(Me.tableYosinKanriInfoTable.mail_datetimeColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Isnayose_cdNull() As Boolean
            Return Me.IsNull(Me.tableYosinKanriInfoTable.nayose_cdColumn)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Setnayose_cdNull()
            Me(Me.tableYosinKanriInfoTable.nayose_cdColumn) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Isnayose_mei1Null() As Boolean
            Return Me.IsNull(Me.tableYosinKanriInfoTable.nayose_mei1Column)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Setnayose_mei1Null()
            Me(Me.tableYosinKanriInfoTable.nayose_mei1Column) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Isnayose_mei2Null() As Boolean
            Return Me.IsNull(Me.tableYosinKanriInfoTable.nayose_mei2Column)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Setnayose_mei2Null()
            Me(Me.tableYosinKanriInfoTable.nayose_mei2Column) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Isnayose_kana1Null() As Boolean
            Return Me.IsNull(Me.tableYosinKanriInfoTable.nayose_kana1Column)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Setnayose_kana1Null()
            Me(Me.tableYosinKanriInfoTable.nayose_kana1Column) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Isnayose_kana2Null() As Boolean
            Return Me.IsNull(Me.tableYosinKanriInfoTable.nayose_kana2Column)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Setnayose_kana2Null()
            Me(Me.tableYosinKanriInfoTable.nayose_kana2Column) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Isnayose_yosin_gakuNull() As Boolean
            Return Me.IsNull(Me.tableYosinKanriInfoTable.nayose_yosin_gakuColumn)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Setnayose_yosin_gakuNull()
            Me(Me.tableYosinKanriInfoTable.nayose_yosin_gakuColumn) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Istodouhuken_cdNull() As Boolean
            Return Me.IsNull(Me.tableYosinKanriInfoTable.todouhuken_cdColumn)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Settodouhuken_cdNull()
            Me(Me.tableYosinKanriInfoTable.todouhuken_cdColumn) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Iskeikoku_jyoukyouNull() As Boolean
            Return Me.IsNull(Me.tableYosinKanriInfoTable.keikoku_jyoukyouColumn)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Setkeikoku_jyoukyouNull()
            Me(Me.tableYosinKanriInfoTable.keikoku_jyoukyouColumn) = System.Convert.DBNull
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function Ismail_datetimeNull() As Boolean
            Return Me.IsNull(Me.tableYosinKanriInfoTable.mail_datetimeColumn)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub Setmail_datetimeNull()
            Me(Me.tableYosinKanriInfoTable.mail_datetimeColumn) = System.Convert.DBNull
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Public Class YosinKanriInfoTableRowChangeEvent
        Inherits System.EventArgs
        
        Private eventRow As YosinKanriInfoTableRow
        
        Private eventAction As System.Data.DataRowAction
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New(ByVal row As YosinKanriInfoTableRow, ByVal action As System.Data.DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Row() As YosinKanriInfoTableRow
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
