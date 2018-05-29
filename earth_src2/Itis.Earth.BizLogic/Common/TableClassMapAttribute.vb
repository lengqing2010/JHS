''' <summary>
''' レコードクラスに割り当てるテーブル名の属性クラス
''' </summary>
''' <remarks></remarks>
<AttributeUsage(AttributeTargets.Class)> _
Public Class TableClassMapAttribute
    Inherits Attribute

    Private table_name As String

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="name"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal name As String)
        table_name = name
    End Sub

    ''' <summary>
    ''' テーブル項目名
    ''' </summary>
    ''' <value>テーブル項目名</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TableName() As String
        Get
            Return table_name
        End Get
    End Property
End Class