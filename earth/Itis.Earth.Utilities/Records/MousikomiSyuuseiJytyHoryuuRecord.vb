''' <summary>
''' 申込修正受注保留レコードクラス/FC申込修正画面
''' </summary>
''' <remarks>FC申込データの保留時に使用します(受注保留時用)</remarks>
<TableClassMap("MousikomiIF")> _
Public Class MousikomiSyuuseiJytyHoryuuRecord
    Inherits MousikomiRecord

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "ステータス"
    ''' <summary>
    ''' ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private strStatus As String = String.Empty
    ''' <summary>
    ''' ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns> ステータス</returns>
    ''' <remarks></remarks>
    <TableMap("status", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=3)> _
    Public Overrides Property Status() As String
        Get
            Return strStatus
        End Get
        Set(ByVal value As String)
            strStatus = value
        End Set
    End Property
#End Region

#Region "更新者"
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String = String.Empty
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新者</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "更新ログインユーザーID"
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String = String.Empty
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

End Class
