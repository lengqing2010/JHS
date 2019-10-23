''' <summary>
''' 特別対応データレコードクラス
''' </summary>
''' <remarks>特別対応データの格納時に使用します</remarks>
<TableClassMap("t_tokubetu_taiou")> _
Public Class TokubetuTaiouRecord
    Inherits TokubetuTaiouRecordBase

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String = String.Empty
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分</returns>
    ''' <remarks></remarks>
    <TableMap("kbn", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overrides Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "保証書NO"
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String = String.Empty
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "特別対応コード"
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intTokubetuTaiouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特別対応コード</returns>
    ''' <remarks></remarks>
    <TableMap("tokubetu_taiou_cd", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=2)> _
    Public Overrides Property TokubetuTaiouCd() As Integer
        Get
            Return intTokubetuTaiouCd
        End Get
        Set(ByVal value As Integer)
            intTokubetuTaiouCd = value
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
    <TableMap("torikesi", IsKey:=False, IsUpdate:=True, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=1)> _
    Public Overrides Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "金額加算商品コード"
    ''' <summary>
    ''' 金額加算商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasanSyouhinCd As String = String.Empty
    ''' <summary>
    ''' 金額加算商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 金額加算商品コード</returns>
    ''' <remarks></remarks>
    <TableMap("kasan_syouhin_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overrides Property KasanSyouhinCd() As String
        Get
            Return strKasanSyouhinCd
        End Get
        Set(ByVal value As String)
            strKasanSyouhinCd = value
        End Set
    End Property
#End Region

#Region "実請求加算金額"
    ''' <summary>
    ''' 実請求加算金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKasanGaku As Integer = 0
    ''' <summary>
    ''' 実請求加算金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 実請求加算金額</returns>
    ''' <remarks></remarks>
    <TableMap("uri_kasan_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property UriKasanGaku() As Integer
        Get
            Return intUriKasanGaku
        End Get
        Set(ByVal value As Integer)
            intUriKasanGaku = value
        End Set
    End Property
#End Region

#Region "工務店請求加算金額"
    ''' <summary>
    ''' 工務店請求加算金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenKasanGaku As Integer = 0
    ''' <summary>
    ''' 工務店請求加算金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工務店請求加算金額</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_kasan_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
 Public Overrides Property KoumutenKasanGaku() As Integer
        Get
            Return intKoumutenKasanGaku
        End Get
        Set(ByVal value As Integer)
            intKoumutenKasanGaku = value
        End Set
    End Property
#End Region

#Region "金額加算商品コードOld"
    ''' <summary>
    ''' 金額加算商品コードOld
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasanSyouhinCdOld As String = String.Empty
    ''' <summary>
    ''' 金額加算商品コードOld
    ''' </summary>
    ''' <value></value>
    ''' <returns> 金額加算商品コードOld</returns>
    ''' <remarks></remarks>
    <TableMap("kasan_syouhin_cd_old", IsKey:=False, IsUpdate:=True, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overrides Property KasanSyouhinCdOld() As String
        Get
            Return strKasanSyouhinCdOld
        End Get
        Set(ByVal value As String)
            strKasanSyouhinCdOld = value
        End Set
    End Property
#End Region

#Region "工務店請求加算金額Old"
    ''' <summary>
    ''' 工務店請求加算金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenKasanGakuOld As Integer = 0
    ''' <summary>
    ''' 工務店請求加算金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工務店請求加算金額Old</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_kasan_gaku_old", IsKey:=False, IsUpdate:=True, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KoumutenKasanGakuOld() As Integer
        Get
            Return intKoumutenKasanGakuOld
        End Get
        Set(ByVal value As Integer)
            intKoumutenKasanGakuOld = value
        End Set
    End Property
#End Region

#Region "実請求加算金額Old"
    ''' <summary>
    ''' 実請求加算金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKasanGakuOld As Integer = 0
    ''' <summary>
    ''' 実請求加算金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns> 実請求加算金額Old</returns>
    ''' <remarks></remarks>
    <TableMap("uri_kasan_gaku_old", IsKey:=False, IsUpdate:=True, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property UriKasanGakuOld() As Integer
        Get
            Return intUriKasanGakuOld
        End Get
        Set(ByVal value As Integer)
            intUriKasanGakuOld = value
        End Set
    End Property
#End Region

#Region "更新フラグ"
    ''' <summary>
    ''' 更新フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intUpdFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 更新フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("upd_flg", IsKey:=False, IsUpdate:=True, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property UpdFlg() As Integer
        Get
            Return intUpdFlg
        End Get
        Set(ByVal value As Integer)
            intUpdFlg = value
        End Set
    End Property
#End Region

#Region "登録ログインユーザーID"
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String = String.Empty
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "登録日時"
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
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

#Region "各種メソッド"
#Region "チェック状況の判定"
    ''' <summary>
    ''' チェック状況の判定
    ''' </summary>
    ''' <remarks></remarks>
    Private blnChk As Boolean = False
    ''' <summary>
    ''' チェック状況の判定
    ''' </summary>
    ''' <value></value>
    ''' <returns>チェック状況の判定</returns>
    ''' <remarks></remarks>
    Public Overrides Property CheckJyky() As Boolean
        Get
            Return blnChk
        End Get
        Set(ByVal value As Boolean)
            blnChk = value
        End Set
    End Property
#End Region
#End Region

End Class