''' <summary>
''' 請求書締め日履歴データレコードクラス/請求書締め日履歴照会画面
''' </summary>
''' <remarks>請求書締め日履歴データの修正時に使用します</remarks>
<TableClassMap("t_seikyuusyo_sime_date_rireki")> _
Public Class SeikyuuSimeDateRirekiSyuuseiRecord
    Inherits SeikyuuSimeDateRirekiRecord

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "履歴NO"
    ''' <summary>
    ''' 履歴NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intRirekiNo As Integer = Integer.MinValue
    ''' <summary>
    ''' 履歴NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 履歴NO</returns>
    ''' <remarks></remarks>
    <TableMap("rireki_no", IsKey:=False, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property RirekiNo() As Integer
        Get
            Return intRirekiNo
        End Get
        Set(ByVal value As Integer)
            intRirekiNo = value
        End Set
    End Property
#End Region

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi", IsKey:=False, IsInsert:=False, IsUpdate:=True, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
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
    <TableMap("upd_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=True, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
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
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=True, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

End Class
