Imports Itis.ApplicationBlocks.ExceptionManagement
''' <summary>
''' 保証画面用の地盤データレコードクラスです
''' </summary>
''' <remarks>地盤テーブルの全レコードカラムに加え、邸別データを保持してます<br/>
'''          商品コード１の邸別請求レコード：TeibetuSeikyuuRecord<br/>
'''          商品コード２の邸別請求レコード：Dictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          商品コード３の邸別請求レコード：Dictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          追加工事の邸別請求レコード　　：TeibetuSeikyuuRecord<br/>
'''          改良工事の邸別請求レコード　　：TeibetuSeikyuuRecord<br/>
'''          調査報告書の邸別請求レコード　：TeibetuSeikyuuRecord<br/>
'''          工事報告書の邸別請求レコード　：TeibetuSeikyuuRecord<br/>
'''          保証書の邸別請求レコード　　　：TeibetuSeikyuuRecord<br/>
'''          解約払戻の邸別請求レコード　　：TeibetuSeikyuuRecord<br/>
'''          上記以外の邸別請求レコード    ：List(TeibetuSeikyuuRecord)<br/>
'''          邸別入金レコード              ：Dictionary(Of String, TeibetuNyuukinRecord)<br/>
''' </remarks>
<TableClassMap("t_jiban")> _
Public Class JibanRecordHosyou
    Inherits JibanRecordBase

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

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
    <TableMap("kbn", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    Private strHosyousyoNo As String
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "ﾃﾞｰﾀ破棄種別"
    ''' <summary>
    ''' ﾃﾞｰﾀ破棄種別
    ''' </summary>
    ''' <remarks></remarks>
    Private intDataHakiSyubetu As Integer
    ''' <summary>
    ''' ﾃﾞｰﾀ破棄種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> ﾃﾞｰﾀ破棄種別</returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_syubetu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DataHakiSyubetu() As Integer
        Get
            Return intDataHakiSyubetu
        End Get
        Set(ByVal value As Integer)
            intDataHakiSyubetu = value
        End Set
    End Property
#End Region

#Region "ﾃﾞｰﾀ破棄日"
    ''' <summary>
    ''' ﾃﾞｰﾀ破棄日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDataHakiDate As DateTime
    ''' <summary>
    ''' ﾃﾞｰﾀ破棄日
    ''' </summary>
    ''' <value></value>
    ''' <returns> ﾃﾞｰﾀ破棄日</returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property DataHakiDate() As DateTime
        Get
            Return dateDataHakiDate
        End Get
        Set(ByVal value As DateTime)
            dateDataHakiDate = value
        End Set
    End Property
#End Region

#Region "施主名"
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 施主名</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Overrides Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "物件住所1"
    ''' <summary>
    ''' 物件住所1
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo1 As String
    ''' <summary>
    ''' 物件住所1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件住所1</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo1", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overrides Property BukkenJyuusyo1() As String
        Get
            Return strBukkenJyuusyo1
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "物件住所2"
    ''' <summary>
    ''' 物件住所2
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo2 As String
    ''' <summary>
    ''' 物件住所2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件住所2</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo2", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overrides Property BukkenJyuusyo2() As String
        Get
            Return strBukkenJyuusyo2
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "物件住所3"
    ''' <summary>
    ''' 物件住所3
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo3 As String
    ''' <summary>
    ''' 物件住所3
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件住所3</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo3", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=54)> _
    Public Overrides Property BukkenJyuusyo3() As String
        Get
            Return strBukkenJyuusyo3
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo3 = value
        End Set
    End Property
#End Region

#Region "加盟店ｺｰﾄﾞ"
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "備考"
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <value></value>
    ''' <returns> 備考</returns>
    ''' <remarks></remarks>
    <TableMap("bikou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "備考2"
    ''' <summary>
    ''' 備考2
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou2 As String
    ''' <summary>
    ''' 備考2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 備考</returns>
    ''' <remarks></remarks>
    <TableMap("bikou2", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=512)> _
    Public Overrides Property Bikou2() As String
        Get
            Return strBikou2
        End Get
        Set(ByVal value As String)
            strBikou2 = value
        End Set
    End Property
#End Region

#Region "契約NO"
    ''' <summary>
    ''' 契約NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiyakuNo As String
    ''' <summary>
    ''' 契約NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 契約NO</returns>
    ''' <remarks></remarks>
    <TableMap("keiyaku_no", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
        End Set
    End Property
#End Region

#Region "基礎報告書有無"
    ''' <summary>
    ''' 基礎報告書有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsHkksUmu As Integer
    ''' <summary>
    ''' 基礎報告書有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎報告書有無</returns>
    ''' <remarks></remarks>
    <TableMap("ks_hkks_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsHkksUmu() As Integer
        Get
            Return intKsHkksUmu
        End Get
        Set(ByVal value As Integer)
            intKsHkksUmu = value
        End Set
    End Property
#End Region

#Region "基礎工事完了報告書着日"
    ''' <summary>
    ''' 基礎工事完了報告書着日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsKojKanryHkksTykDate As DateTime
    ''' <summary>
    ''' 基礎工事完了報告書着日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎工事完了報告書着日</returns>
    ''' <remarks></remarks>
    <TableMap("ks_koj_kanry_hkks_tyk_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KsKojKanryHkksTykDate() As DateTime
        Get
            Return dateKsKojKanryHkksTykDate
        End Get
        Set(ByVal value As DateTime)
            dateKsKojKanryHkksTykDate = value
        End Set
    End Property
#End Region

#Region "保証書発行状況"
    ''' <summary>
    ''' 保証書発行状況
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakJyky As Integer
    ''' <summary>
    ''' 保証書発行状況
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行状況</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_jyky", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyousyoHakJyky() As Integer
        Get
            Return intHosyousyoHakJyky
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakJyky = value
        End Set
    End Property
#End Region

#Region "保証書発行状況設定日"
    ''' <summary>
    ''' 保証書発行状況設定日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakJykySetteiDate As DateTime
    ''' <summary>
    ''' 保証書発行状況設定日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行状況設定日</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_jyky_settei_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakJykySetteiDate() As DateTime
        Get
            Return dateHosyousyoHakJykySetteiDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakJykySetteiDate = value
        End Set
    End Property
#End Region

#Region "保証書発行日"
    ''' <summary>
    ''' 保証書発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakDate As DateTime
    ''' <summary>
    ''' 保証書発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行日</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakDate() As DateTime
        Get
            Return dateHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakDate = value
        End Set
    End Property
#End Region

#Region "保証有無"
    ''' <summary>
    ''' 保証有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouUmu As Integer
    ''' <summary>
    ''' 保証有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証有無</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyouUmu() As Integer
        Get
            Return intHosyouUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouUmu = value
        End Set
    End Property
#End Region

#Region "保証開始日"
    ''' <summary>
    ''' 保証開始日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyouKaisiDate As DateTime
    ''' <summary>
    ''' 保証開始日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証開始日</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kaisi_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyouKaisiDate() As DateTime
        Get
            Return dateHosyouKaisiDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyouKaisiDate = value
        End Set
    End Property
#End Region

#Region "保証期間"
    ''' <summary>
    ''' 保証期間
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikan As Integer
    ''' <summary>
    ''' 保証期間
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証期間</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kikan", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyouKikan() As Integer
        Get
            Return intHosyouKikan
        End Get
        Set(ByVal value As Integer)
            intHosyouKikan = value
        End Set
    End Property
#End Region

#Region "保証なし理由"
    ''' <summary>
    ''' 保証なし理由
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyouNasiRiyuu As String
    ''' <summary>
    ''' 保証なし理由
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証なし理由</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_nasi_riyuu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overrides Property HosyouNasiRiyuu() As String
        Get
            Return strHosyouNasiRiyuu
        End Get
        Set(ByVal value As String)
            strHosyouNasiRiyuu = value
        End Set
    End Property
#End Region

#Region "保証商品有無"
    ''' <summary>
    ''' 保証商品有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouSyouhinUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証商品有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証商品有無</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_syouhin_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyouSyouhinUmu() As Integer
        Get
            Return intHosyouSyouhinUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouSyouhinUmu = value
        End Set
    End Property
#End Region

#Region "保険会社"
    ''' <summary>
    ''' 保険会社
    ''' </summary>
    ''' <remarks></remarks>
    Private intHokenKaisya As Integer
    ''' <summary>
    ''' 保険会社
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保険会社</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_kaisya", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HokenKaisya() As Integer
        Get
            Return intHokenKaisya
        End Get
        Set(ByVal value As Integer)
            intHokenKaisya = value
        End Set
    End Property
#End Region

#Region "保険申請月"
    ''' <summary>
    ''' 保険申請月
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHokenSinseiTuki As DateTime
    ''' <summary>
    ''' 保険申請月
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保険申請月</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_sinsei_tuki", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HokenSinseiTuki() As DateTime
        Get
            Return dateHokenSinseiTuki
        End Get
        Set(ByVal value As DateTime)
            dateHokenSinseiTuki = value
        End Set
    End Property
#End Region

#Region "保証書再発行日"
    ''' <summary>
    ''' 保証書再発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoSaihakDate As DateTime
    ''' <summary>
    ''' 保証書再発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書再発行日</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_saihak_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoSaihakDate() As DateTime
        Get
            Return dateHosyousyoSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDate = value
        End Set
    End Property
#End Region

#Region "保証書発行依頼書有無"
    ''' <summary>
    ''' 保証書発行依頼書有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakIraisyoUmu As Integer
    ''' <summary>
    ''' 保証書発行依頼書有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行依頼書有無</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_iraisyo_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyousyoHakIraisyoUmu() As Integer
        Get
            Return intHosyousyoHakIraisyoUmu
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakIraisyoUmu = value
        End Set
    End Property
#End Region

#Region "保証書発行依頼書着日"
    ''' <summary>
    ''' 保証書発行依頼書着日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakIraisyoTykDate As DateTime
    ''' <summary>
    ''' 保証書発行依頼書着日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行依頼書着日</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_iraisyo_tyk_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakIraisyoTykDate() As DateTime
        Get
            Return dateHosyousyoHakIraisyoTykDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakIraisyoTykDate = value
        End Set
    End Property
#End Region

#Region "保険申請区分"
    ''' <summary>
    ''' 保険申請区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intHokenSinseiKbn As Integer
    ''' <summary>
    ''' 保険申請区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保険申請区分</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_sinsei_kbn", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HokenSinseiKbn() As Integer
        Get
            Return intHokenSinseiKbn
        End Get
        Set(ByVal value As Integer)
            intHokenSinseiKbn = value
        End Set
    End Property
#End Region

#Region "更新ﾛｸﾞｲﾝﾕｰｻﾞｰID"
    ''' <summary>
    ''' 更新ﾛｸﾞｲﾝﾕｰｻﾞｰID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' 更新ﾛｸﾞｲﾝﾕｰｻﾞｰID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ﾛｸﾞｲﾝﾕｰｻﾞｰID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks>排他制御を行う為、検索時の更新日付を設定してください<br/>
    '''          更新時の日付はシステム日付が設定されます</remarks>
    <TableMap("upd_datetime", IsKey:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "調査立会者"
    ''' <summary>
    ''' 調査立会者
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaTachiai As String
    ''' <summary>
    ''' 調査立会者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査立会者</returns>
    ''' <remarks></remarks>
    Public Property ChousaTachiai() As String
        Get
            Return strChousaTachiai
        End Get
        Set(ByVal value As String)
            strChousaTachiai = value
        End Set
    End Property
#End Region

#Region "更新者"
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新者</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

#Region "付保証明書FLG"
    ''' <summary>
    ''' 付保証明書FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intFuhoSyoumeisyoFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 付保証明書FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>付保証明書FLG</returns>
    ''' <remarks></remarks>
    <TableMap("fuho_syoumeisyo_flg", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property FuhoSyoumeisyoFlg() As Integer
        Get
            Return intFuhoSyoumeisyoFlg
        End Get
        Set(ByVal value As Integer)
            intFuhoSyoumeisyoFlg = value
        End Set
    End Property
#End Region

#Region "変更予定加盟店コード"
    ''' <summary>
    ''' 変更予定加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strHenkouYoteiKameitenCd As String
    ''' <summary>
    ''' 変更予定加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 変更予定加盟店コード</returns>
    ''' <remarks></remarks>
    <TableMap("henkou_yotei_kameiten_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property HenkouYoteiKameitenCd() As String
        Get
            Return strHenkouYoteiKameitenCd
        End Get
        Set(ByVal value As String)
            strHenkouYoteiKameitenCd = value
        End Set
    End Property
#End Region

#Region "保証なし理由コード"
    ''' <summary>
    ''' 保証なし理由コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyouNasiRiyuuCd As String
    ''' <summary>
    ''' 保証なし理由コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証なし理由コード</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_nasi_riyuu_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property HosyouNasiRiyuuCd() As String
        Get
            Return strHosyouNasiRiyuuCd
        End Get
        Set(ByVal value As String)
            strHosyouNasiRiyuuCd = value
        End Set
    End Property
#End Region

#Region "発行依頼受付日時"
    ''' <summary>
    ''' 発行依頼受付日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiUkeDatetime As DateTime
    ''' <summary>
    ''' 発行依頼受付日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼受付日時</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_uke_datetime", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HakIraiUkeDatetime() As DateTime
        Get
            Return dateHakIraiUkeDatetime
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiUkeDatetime = value
        End Set
    End Property
#End Region


#Region "発行依頼キャンセル日時"
    ''' <summary>
    ''' 発行依頼キャンセル日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiCanDatetime As DateTime
    ''' <summary>
    ''' 発行依頼キャンセル日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼キャンセル日時</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_can_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HakIraiCanDatetime() As DateTime
        Get
            Return dateHakIraiCanDatetime
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiCanDatetime = value
        End Set
    End Property
#End Region


#Region "発行依頼その他情報"
    ''' <summary>
    ''' 発行依頼その他情報
    ''' </summary>
    ''' <remarks></remarks>
    Private strIraiSonota As String
    ''' <summary>
    ''' 発行依頼その他情報
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼その他情報</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_sonota", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=255)> _
    Public Property IraiSonota() As String
        Get
            Return strIraiSonota
        End Get
        Set(ByVal value As String)
            strIraiSonota = value
        End Set
    End Property
#End Region
End Class