''' <summary>
''' 請求書締め日履歴データレコードクラス/請求書締め日履歴照会画面
''' </summary>
''' <remarks>請求書締め日履歴データの格納時に使用します</remarks>
<TableClassMap("t_seikyuusyo_sime_date_rireki")> _
Public Class SeikyuuSimeDateRirekiRecord

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
    Public Overridable Property RirekiNo() As Integer
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
    <TableMap("torikesi", IsKey:=False, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "請求先コード"
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String = String.Empty
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd", IsKey:=True, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property SeikyuuSakiCd() As String
        Get
            Return strSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "請求先枝番"
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String = String.Empty
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc", IsKey:=True, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "請求先区分"
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String = String.Empty
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn", IsKey:=True, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overridable Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "請求書発行年月"
    ''' <summary>
    ''' 請求書発行年月
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoHakNengetu As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 請求書発行年月
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書発行年月</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_hak_nengetu", IsKey:=True, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property SeikyuusyoHakNengetu() As DateTime
        Get
            Return dateSeikyuusyoHakNengetu
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoHakNengetu = value
        End Set
    End Property
#End Region

#Region "請求締め日"
    ''' <summary>
    ''' 請求締め日
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSimeDate As String = String.Empty
    ''' <summary>
    ''' 請求締め日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求締め日</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_sime_date", IsKey:=True, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property SeikyuuSimeDate() As String
        Get
            Return strSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "全対象フラグ"
    ''' <summary>
    ''' 全対象フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intZenTaisyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 全対象フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 全対象フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("zen_taisyou_flg", IsKey:=False, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property ZenTaisyouFlg() As Integer
        Get
            Return intZenTaisyouFlg
        End Get
        Set(ByVal value As Integer)
            intZenTaisyouFlg = value
        End Set
    End Property
#End Region

#Region "請求書NO"
    ''' <summary>
    ''' 請求書NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusyoNo As String = String.Empty
    ''' <summary>
    ''' 請求書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書NO</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_no", IsKey:=False, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=15)> _
    Public Overridable Property SeikyuusyoNo() As String
        Get
            Return strSeikyuusyoNo
        End Get
        Set(ByVal value As String)
            strSeikyuusyoNo = value
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
    <TableMap("add_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property AddLoginUserId() As String
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
    <TableMap("add_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property AddDatetime() As DateTime
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
    <TableMap("upd_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property UpdLoginUserId() As String
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
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "請求先情報VIEWの項目"

#Region "請求先名"
    ''' <summary>
    ''' 請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei As String = String.Empty
    ''' <summary>
    ''' 請求先名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先名</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei")> _
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "請求先名2"
    ''' <summary>
    ''' 請求先名2
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei2 As String = String.Empty
    ''' <summary>
    ''' 請求先名2
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei2")> _
    Public Property SeikyuuSakiMei2() As String
        Get
            Return strSeikyuuSakiMei2
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei2 = value
        End Set
    End Property
#End Region

#End Region

#Region "最終請求書フラグ"
    ''' <summary>
    ''' 最終請求書フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intMaxRirekiNo As Integer = Integer.MinValue
    ''' <summary>
    ''' 最終請求書フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 最終請求書フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("max_rireki_no_flg")> _
    Public Property MaxRirekiNo() As Integer
        Get
            Return intMaxRirekiNo
        End Get
        Set(ByVal value As Integer)
            intMaxRirekiNo = value
        End Set
    End Property
#End Region

#Region "請求鑑テーブルの項目"

#Region "今回御請求金額"
    ''' <summary>
    ''' 今回御請求金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intKonkaiGoseikyuuGaku As Integer = 0
    ''' <summary>
    ''' 今回御請求金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("konkai_goseikyuu_gaku")> _
    Public Overridable Property KonkaiGoseikyuuGaku() As Integer
        Get
            Return intKonkaiGoseikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intKonkaiGoseikyuuGaku = value
        End Set
    End Property
#End Region

#Region "更新ログインユーザーID"
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkUpdLoginUserId As String = String.Empty
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("sk_upd_login_user_id")> _
    Public Overridable Property SkUpdLoginUserId() As String
        Get
            Return strSkUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strSkUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSkUpdDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("sk_upd_datetime")> _
    Public Overridable Property SkUpdDatetime() As DateTime
        Get
            Return dateSkUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateSkUpdDatetime = value
        End Set
    End Property
#End Region

#End Region

#Region "請求書発行日(画面表示用)"
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoHakDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 請求書発行日(画面表示用)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書発行日</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_hak_date")> _
    Public Overridable Property SeikyuusyoHakDate() As DateTime
        Get
            Return dateSeikyuusyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoHakDate = value
        End Set
    End Property
#End Region

End Class