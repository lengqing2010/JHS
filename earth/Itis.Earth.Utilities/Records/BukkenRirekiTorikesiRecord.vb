''' <summary>
''' 物件履歴テーブルの更新用レコードクラスです
''' </summary>
''' <remarks>物件履歴画面－取消処理用</remarks>
<TableClassMap("t_bukken_rireki")> _
Public Class BukkenRirekiTorikesiRecord
#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分</returns>
    ''' <remarks></remarks>
    <TableMap("kbn", IsKey:=True, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property Kbn() As String
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
    Private strHosyousyoNo As String
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no", IsKey:=True, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "履歴種別"
    ''' <summary>
    ''' 履歴種別
    ''' </summary>
    ''' <remarks></remarks>
    Private strRirekiSyubetu As String
    ''' <summary>
    ''' 履歴種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 履歴種別</returns>
    ''' <remarks></remarks>
    <TableMap("rireki_syubetu", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property RirekiSyubetu() As String
        Get
            Return strRirekiSyubetu
        End Get
        Set(ByVal value As String)
            strRirekiSyubetu = value
        End Set
    End Property
#End Region

#Region "履歴NO"
    ''' <summary>
    ''' 履歴NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intRirekiNo As Integer
    ''' <summary>
    ''' 履歴NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 履歴NO</returns>
    ''' <remarks></remarks>
    <TableMap("rireki_no", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property RirekiNo() As Integer
        Get
            Return intRirekiNo
        End Get
        Set(ByVal value As Integer)
            intRirekiNo = value
        End Set
    End Property
#End Region

#Region "入力NO"
    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuuryokuNo As Integer
    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入力NO</returns>
    ''' <remarks></remarks>
    <TableMap("nyuuryoku_no", IsKey:=True, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property NyuuryokuNo() As Integer
        Get
            Return intNyuuryokuNo
        End Get
        Set(ByVal value As Integer)
            intNyuuryokuNo = value
        End Set
    End Property
#End Region

#Region "内容"
    ''' <summary>
    ''' 内容
    ''' </summary>
    ''' <remarks></remarks>
    Private strNaiyou As String
    ''' <summary>
    ''' 内容
    ''' </summary>
    ''' <value></value>
    ''' <returns> 内容</returns>
    ''' <remarks></remarks>
    <TableMap("naiyou", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=512)> _
    Public Overridable Property Naiyou() As String
        Get
            Return strNaiyou
        End Get
        Set(ByVal value As String)
            strNaiyou = value
        End Set
    End Property
#End Region

#Region "汎用日付"
    ''' <summary>
    ''' 汎用日付
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHanyouDate As DateTime
    ''' <summary>
    ''' 汎用日付
    ''' </summary>
    ''' <value></value>
    ''' <returns> 汎用日付</returns>
    ''' <remarks></remarks>
    <TableMap("hanyou_date", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HanyouDate() As DateTime
        Get
            Return dateHanyouDate
        End Get
        Set(ByVal value As DateTime)
            dateHanyouDate = value
        End Set
    End Property
#End Region

#Region "汎用コード"
    ''' <summary>
    ''' 汎用コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strHanyouCd As String
    ''' <summary>
    ''' 汎用コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 汎用コード</returns>
    ''' <remarks></remarks>
    <TableMap("hanyou_cd", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overridable Property HanyouCd() As String
        Get
            Return strHanyouCd
        End Get
        Set(ByVal value As String)
            strHanyouCd = value
        End Set
    End Property
#End Region

#Region "管理日付"
    ''' <summary>
    ''' 管理日付
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKanriDate As DateTime
    ''' <summary>
    ''' 管理日付
    ''' </summary>
    ''' <value></value>
    ''' <returns> 管理日付</returns>
    ''' <remarks></remarks>
    <TableMap("kanri_date", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property KanriDate() As DateTime
        Get
            Return dateKanriDate
        End Get
        Set(ByVal value As DateTime)
            dateKanriDate = value
        End Set
    End Property
#End Region

#Region "管理コード"
    ''' <summary>
    ''' 管理コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKanriCd As String
    ''' <summary>
    ''' 管理コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 管理コード</returns>
    ''' <remarks></remarks>
    <TableMap("kanri_cd", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property KanriCd() As String
        Get
            Return strKanriCd
        End Get
        Set(ByVal value As String)
            strKanriCd = value
        End Set
    End Property
#End Region

#Region "変更可否フラグ"
    ''' <summary>
    ''' 変更可否フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intHenkouKahiFlg As Integer
    ''' <summary>
    ''' 変更可否フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 変更可否フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("henkou_kahi_flg", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HenkouKahiFlg() As Integer
        Get
            Return intHenkouKahiFlg
        End Get
        Set(ByVal value As Integer)
            intHenkouKahiFlg = value
        End Set
    End Property
#End Region

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "登録ログインユーザーID"
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property AddLoginUserId() As String
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
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property AddDatetime() As DateTime
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
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property UpdLoginUserId() As String
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
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks>入力NOを-1で初期化</remarks>
    Sub New()
        intNyuuryokuNo = -1
    End Sub
End Class
