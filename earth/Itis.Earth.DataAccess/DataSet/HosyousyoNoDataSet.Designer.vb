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
 System.Xml.Serialization.XmlRootAttribute("HosyousyoNoDataSet"),  _
 System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")>  _
Partial Public Class HosyousyoNoDataSet
    Inherits System.Data.DataSet
    
    Private tableHosyousyoNoTable As HosyousyoNoTableDataTable
    
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
            If (Not (ds.Tables("HosyousyoNoTable")) Is Nothing) Then
                MyBase.Tables.Add(New HosyousyoNoTableDataTable(ds.Tables("HosyousyoNoTable")))
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
    Public ReadOnly Property HosyousyoNoTable() As HosyousyoNoTableDataTable
        Get
            Return Me.tableHosyousyoNoTable
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
        Dim cln As HosyousyoNoDataSet = CType(MyBase.Clone,HosyousyoNoDataSet)
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
            If (Not (ds.Tables("HosyousyoNoTable")) Is Nothing) Then
                MyBase.Tables.Add(New HosyousyoNoTableDataTable(ds.Tables("HosyousyoNoTable")))
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
        Me.tableHosyousyoNoTable = CType(MyBase.Tables("HosyousyoNoTable"),HosyousyoNoTableDataTable)
        If (initTable = true) Then
            If (Not (Me.tableHosyousyoNoTable) Is Nothing) Then
                Me.tableHosyousyoNoTable.InitVars
            End If
        End If
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Sub InitClass()
        Me.DataSetName = "HosyousyoNoDataSet"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/HosyousyoNoDataSet.xsd"
        Me.EnforceConstraints = true
        Me.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        Me.tableHosyousyoNoTable = New HosyousyoNoTableDataTable
        MyBase.Tables.Add(Me.tableHosyousyoNoTable)
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Function ShouldSerializeHosyousyoNoTable() As Boolean
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
        Dim ds As HosyousyoNoDataSet = New HosyousyoNoDataSet
        Dim type As System.Xml.Schema.XmlSchemaComplexType = New System.Xml.Schema.XmlSchemaComplexType
        Dim sequence As System.Xml.Schema.XmlSchemaSequence = New System.Xml.Schema.XmlSchemaSequence
        xs.Add(ds.GetSchemaSerializable)
        Dim any As System.Xml.Schema.XmlSchemaAny = New System.Xml.Schema.XmlSchemaAny
        any.Namespace = ds.Namespace
        sequence.Items.Add(any)
        type.Particle = sequence
        Return type
    End Function
    
    Public Delegate Sub HosyousyoNoTableRowChangeEventHandler(ByVal sender As Object, ByVal e As HosyousyoNoTableRowChangeEvent)
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"),  _
     System.Serializable(),  _
     System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>  _
    Partial Public Class HosyousyoNoTableDataTable
        Inherits System.Data.DataTable
        Implements System.Collections.IEnumerable
        
        Private columnkbn As System.Data.DataColumn
        
        Private columnnengetu As System.Data.DataColumn
        
        Private columnsaisyuu_no As System.Data.DataColumn
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New()
            MyBase.New
            Me.TableName = "HosyousyoNoTable"
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
        Public ReadOnly Property nengetuColumn() As System.Data.DataColumn
            Get
                Return Me.columnnengetu
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property saisyuu_noColumn() As System.Data.DataColumn
            Get
                Return Me.columnsaisyuu_no
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
        Public Default ReadOnly Property Item(ByVal index As Integer) As HosyousyoNoTableRow
            Get
                Return CType(Me.Rows(index),HosyousyoNoTableRow)
            End Get
        End Property
        
        Public Event HosyousyoNoTableRowChanging As HosyousyoNoTableRowChangeEventHandler
        
        Public Event HosyousyoNoTableRowChanged As HosyousyoNoTableRowChangeEventHandler
        
        Public Event HosyousyoNoTableRowDeleting As HosyousyoNoTableRowChangeEventHandler
        
        Public Event HosyousyoNoTableRowDeleted As HosyousyoNoTableRowChangeEventHandler
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Sub AddHosyousyoNoTableRow(ByVal row As HosyousyoNoTableRow)
            Me.Rows.Add(row)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Function AddHosyousyoNoTableRow(ByVal kbn As String, ByVal nengetu As String, ByVal saisyuu_no As Integer) As HosyousyoNoTableRow
            Dim rowHosyousyoNoTableRow As HosyousyoNoTableRow = CType(Me.NewRow,HosyousyoNoTableRow)
            rowHosyousyoNoTableRow.ItemArray = New Object() {kbn, nengetu, saisyuu_no}
            Me.Rows.Add(rowHosyousyoNoTableRow)
            Return rowHosyousyoNoTableRow
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function FindBykbnnengetu(ByVal kbn As String, ByVal nengetu As String) As HosyousyoNoTableRow
            Return CType(Me.Rows.Find(New Object() {kbn, nengetu}),HosyousyoNoTableRow)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overridable Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overrides Function Clone() As System.Data.DataTable
            Dim cln As HosyousyoNoTableDataTable = CType(MyBase.Clone,HosyousyoNoTableDataTable)
            cln.InitVars
            Return cln
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function CreateInstance() As System.Data.DataTable
            Return New HosyousyoNoTableDataTable
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub InitVars()
            Me.columnkbn = MyBase.Columns("kbn")
            Me.columnnengetu = MyBase.Columns("nengetu")
            Me.columnsaisyuu_no = MyBase.Columns("saisyuu_no")
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Private Sub InitClass()
            Me.columnkbn = New System.Data.DataColumn("kbn", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnkbn)
            Me.columnnengetu = New System.Data.DataColumn("nengetu", GetType(String), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnnengetu)
            Me.columnsaisyuu_no = New System.Data.DataColumn("saisyuu_no", GetType(Integer), Nothing, System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnsaisyuu_no)
            Me.Constraints.Add(New System.Data.UniqueConstraint("Constraint1", New System.Data.DataColumn() {Me.columnkbn, Me.columnnengetu}, true))
            Me.columnkbn.AllowDBNull = false
            Me.columnkbn.MaxLength = 1
            Me.columnnengetu.AllowDBNull = false
            Me.columnnengetu.MaxLength = 6
            Me.columnsaisyuu_no.AllowDBNull = false
            Me.columnsaisyuu_no.DefaultValue = CType(0,Integer)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function NewHosyousyoNoTableRow() As HosyousyoNoTableRow
            Return CType(Me.NewRow,HosyousyoNoTableRow)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function NewRowFromBuilder(ByVal builder As System.Data.DataRowBuilder) As System.Data.DataRow
            Return New HosyousyoNoTableRow(builder)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(HosyousyoNoTableRow)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanged(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.HosyousyoNoTableRowChangedEvent) Is Nothing) Then
                RaiseEvent HosyousyoNoTableRowChanged(Me, New HosyousyoNoTableRowChangeEvent(CType(e.Row,HosyousyoNoTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanging(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.HosyousyoNoTableRowChangingEvent) Is Nothing) Then
                RaiseEvent HosyousyoNoTableRowChanging(Me, New HosyousyoNoTableRowChangeEvent(CType(e.Row,HosyousyoNoTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleted(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.HosyousyoNoTableRowDeletedEvent) Is Nothing) Then
                RaiseEvent HosyousyoNoTableRowDeleted(Me, New HosyousyoNoTableRowChangeEvent(CType(e.Row,HosyousyoNoTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleting(ByVal e As System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.HosyousyoNoTableRowDeletingEvent) Is Nothing) Then
                RaiseEvent HosyousyoNoTableRowDeleting(Me, New HosyousyoNoTableRowChangeEvent(CType(e.Row,HosyousyoNoTableRow), e.Action))
            End If
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub RemoveHosyousyoNoTableRow(ByVal row As HosyousyoNoTableRow)
            Me.Rows.Remove(row)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Shared Function GetTypedTableSchema(ByVal xs As System.Xml.Schema.XmlSchemaSet) As System.Xml.Schema.XmlSchemaComplexType
            Dim type As System.Xml.Schema.XmlSchemaComplexType = New System.Xml.Schema.XmlSchemaComplexType
            Dim sequence As System.Xml.Schema.XmlSchemaSequence = New System.Xml.Schema.XmlSchemaSequence
            Dim ds As HosyousyoNoDataSet = New HosyousyoNoDataSet
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
            attribute2.FixedValue = "HosyousyoNoTableDataTable"
            type.Attributes.Add(attribute2)
            type.Particle = sequence
            Return type
        End Function
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Partial Public Class HosyousyoNoTableRow
        Inherits System.Data.DataRow
        
        Private tableHosyousyoNoTable As HosyousyoNoTableDataTable
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub New(ByVal rb As System.Data.DataRowBuilder)
            MyBase.New(rb)
            Me.tableHosyousyoNoTable = CType(Me.Table,HosyousyoNoTableDataTable)
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property kbn() As String
            Get
                Return CType(Me(Me.tableHosyousyoNoTable.kbnColumn),String)
            End Get
            Set
                Me(Me.tableHosyousyoNoTable.kbnColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property nengetu() As String
            Get
                Return CType(Me(Me.tableHosyousyoNoTable.nengetuColumn),String)
            End Get
            Set
                Me(Me.tableHosyousyoNoTable.nengetuColumn) = value
            End Set
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property saisyuu_no() As Integer
            Get
                Return CType(Me(Me.tableHosyousyoNoTable.saisyuu_noColumn),Integer)
            End Get
            Set
                Me(Me.tableHosyousyoNoTable.saisyuu_noColumn) = value
            End Set
        End Property
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Public Class HosyousyoNoTableRowChangeEvent
        Inherits System.EventArgs
        
        Private eventRow As HosyousyoNoTableRow
        
        Private eventAction As System.Data.DataRowAction
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New(ByVal row As HosyousyoNoTableRow, ByVal action As System.Data.DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Row() As HosyousyoNoTableRow
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
