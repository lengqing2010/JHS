''' <summary>
''' 特別対応データレコードクラス
''' </summary>
''' <remarks>特別対応データの格納時に使用します</remarks>
<TableClassMap("t_tokubetu_taiou")> _
Public Class TokubetuTaiouKkkSyoriRecord
    Inherits TokubetuTaiouRecordBase

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
    <TableMap("torikesi", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=1)> _
    Public Overrides Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "価格処理フラグ"
    ''' <summary>
    ''' 価格処理フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKkkSyoriFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 価格処理フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 価格処理フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("kkk_syori_flg", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KkkSyoriFlg() As Integer
        Get
            Return intKkkSyoriFlg
        End Get
        Set(ByVal value As Integer)
            intKkkSyoriFlg = value
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

End Class