''' <summary>
''' レコードクラスに割り当てるテーブル名の属性クラス
''' </summary>
''' <remarks></remarks>
<AttributeUsage(AttributeTargets.Class)> _
Public Class TableClassMapAttribute
    Inherits Attribute

    Private _tableName As String

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="name"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal name As String)
        _tableName = name
    End Sub

    ''' <summary>
    ''' テーブル項目名
    ''' </summary>
    ''' <value>テーブル項目名</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TableName() As String
        Get
            Return _tableName
        End Get
    End Property
End Class