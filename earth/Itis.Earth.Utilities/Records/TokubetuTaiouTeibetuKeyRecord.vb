''' <summary>
''' 特別対応データレコードクラス
''' </summary>
''' <remarks>特別対応データの格納時に使用します</remarks>
<TableClassMap("t_tokubetu_taiou")> _
Public Class TokubetuTaiouTeibetuKeyRecord
    Inherits TokubetuTaiouRecordBase

#Region "分類コード"
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String = String.Empty
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 分類コード</returns>
    ''' <remarks></remarks>
    <TableMap("bunrui_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overrides Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "画面表示NO"
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intGamenHyoujiNo As Integer = 0
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 画面表示NO</returns>
    ''' <remarks></remarks>
    <TableMap("gamen_hyouji_no", IsKey:=False, IsUpdate:=True, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property GamenHyoujiNo() As Integer
        Get
            Return intGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            intGamenHyoujiNo = value
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
    <TableMap("kasan_syouhin_cd_old", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
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
    Private intKoumutenKasanGakuOld As Integer = Integer.MinValue
    ''' <summary>
    ''' 工務店請求加算金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工務店請求加算金額Old</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_kasan_gaku_old", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    Private intUriKasanGakuOld As Integer = Integer.MinValue
    ''' <summary>
    ''' 実請求加算金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns> 実請求加算金額Old</returns>
    ''' <remarks></remarks>
    <TableMap("uri_kasan_gaku_old", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("upd_flg", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property UpdFlg() As Integer
        Get
            Return intUpdFlg
        End Get
        Set(ByVal value As Integer)
            intUpdFlg = value
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