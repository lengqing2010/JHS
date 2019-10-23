''' <summary>
''' 品質保証書状況検索のレコードクラスです
''' </summary>
''' <remarks></remarks>
Public Class HinsituHosyousyoJyoukyouSearchRecord

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
    <TableMap("kbn")> _
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
    <TableMap("hosyousyo_no")> _
    Public Property HosyousyoNo() As String
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
    Private strDataHakiSyubetu As String
    ''' <summary>
    ''' ﾃﾞｰﾀ破棄種別
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_syubetu")> _
    Public Property DataHakiSyubetu() As String
        Get
            Return strDataHakiSyubetu
        End Get
        Set(ByVal value As String)
            strDataHakiSyubetu = value
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
    <TableMap("sesyu_mei")> _
    Public Property SesyuMei() As String
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
    <TableMap("bukken_jyuusyo1")> _
    Public Property BukkenJyuusyo1() As String
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
    <TableMap("bukken_jyuusyo2")> _
    Public Property BukkenJyuusyo2() As String
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
    <TableMap("bukken_jyuusyo3")> _
    Public Property BukkenJyuusyo3() As String
        Get
            Return strBukkenJyuusyo3
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo3 = value
        End Set
    End Property
#End Region

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "加盟店取消"
    ''' <summary>
    ''' 加盟店取消
    ''' </summary>
    ''' <remarks></remarks>
    Private intKtTorikesi As Integer
    ''' <summary>
    ''' 加盟店取消
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店取消</returns>
    ''' <remarks></remarks>
    <TableMap("kt_torikesi")> _
    Public Property KtTorikesi() As Integer
        Get
            Return intKtTorikesi
        End Get
        Set(ByVal value As Integer)
            intKtTorikesi = value
        End Set
    End Property
#End Region

#Region "加盟店取消理由"
    ''' <summary>
    ''' 加盟店取消理由
    ''' </summary>
    ''' <remarks></remarks>
    Private strKtTorikesiRiyuu As String
    ''' <summary>
    ''' 加盟店取消理由
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店取消理由</returns>
    ''' <remarks></remarks>
    <TableMap("kt_torikesi_riyuu")> _
    Public Property KtTorikesiRiyuu() As String
        Get
            Return strKtTorikesiRiyuu
        End Get
        Set(ByVal value As String)
            strKtTorikesiRiyuu = value
        End Set
    End Property
#End Region

#Region "加盟店名1"
    ''' <summary>
    ''' 加盟店名1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei1 As String
    ''' <summary>
    ''' 加盟店名1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店名1</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei1")> _
    Public Property KameitenMei1() As String
        Get
            Return strKameitenMei1
        End Get
        Set(ByVal value As String)
            strKameitenMei1 = value
        End Set
    End Property
#End Region

#Region "店名カナ1"
    ''' <summary>
    ''' 店名カナ1
    ''' </summary>
    ''' <remarks></remarks>
    Private strTenmeiKana1 As String
    ''' <summary>
    ''' 店名カナ1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 店名カナ1</returns>
    ''' <remarks></remarks>
    <TableMap("tenmei_kana1")> _
    Public Property TenmeiKana1() As String
        Get
            Return strTenmeiKana1
        End Get
        Set(ByVal value As String)
            strTenmeiKana1 = value
        End Set
    End Property
#End Region

#Region "発行依頼日時"
    ''' <summary>
    ''' 発行依頼日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiTime As DateTime
    ''' <summary>
    ''' 発行依頼日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼日時</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_time")> _
    Public Property HakIraiTime() As DateTime
        Get
            Return dateHakIraiTime
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiTime = value
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
    <TableMap("hosyousyo_hak_iraisyo_tyk_date")> _
    Public Property HosyousyoHakIraisyoTykDate() As DateTime
        Get
            Return dateHosyousyoHakIraisyoTykDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakIraisyoTykDate = value
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
    <TableMap("hosyousyo_hak_date")> _
    Public Property HosyousyoHakDate() As DateTime
        Get
            Return dateHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakDate = value
        End Set
    End Property
#End Region

#Region "保証書開始日"
    ''' <summary>
    ''' 保証書開始日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyouKaisiDate As DateTime
    ''' <summary>
    ''' 保証書開始日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書開始日</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kaisi_date")> _
    Public Property HosyouKaisiDate() As DateTime
        Get
            Return dateHosyouKaisiDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyouKaisiDate = value
        End Set
    End Property
#End Region

#Region "基礎仕様1"
    ''' <summary>
    ''' 基礎仕様1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKsSiyou1 As String
    ''' <summary>
    ''' 基礎仕様1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎仕様1</returns>
    ''' <remarks></remarks>
    <TableMap("ks_siyou_1")> _
    Public Property KsSiyou1() As String
        Get
            Return strKsSiyou1
        End Get
        Set(ByVal value As String)
            strKsSiyou1 = value
        End Set
    End Property
#End Region

#Region "基礎仕様2"
    ''' <summary>
    ''' 基礎仕様2
    ''' </summary>
    ''' <remarks></remarks>
    Private strKsSiyou2 As String
    ''' <summary>
    ''' 基礎仕様2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎仕様2</returns>
    ''' <remarks></remarks>
    <TableMap("ks_siyou_2")> _
    Public Property KsSiyou2() As String
        Get
            Return strKsSiyou2
        End Get
        Set(ByVal value As String)
            strKsSiyou2 = value
        End Set
    End Property
#End Region

#Region "基礎仕様接続詞"
    ''' <summary>
    ''' 基礎仕様接続詞
    ''' </summary>
    ''' <remarks></remarks>
    Private strKsSiyouSetuzokusi As String
    ''' <summary>
    ''' 基礎仕様接続詞
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("ks_siyou_setuzokusi")> _
    Public Property KsSiyouSetuzokusi() As String
        Get
            Return strKsSiyouSetuzokusi
        End Get
        Set(ByVal value As String)
            strKsSiyouSetuzokusi = value
        End Set
    End Property
#End Region

#Region "工事報告書受理日"
    ''' <summary>
    ''' 工事報告書受理日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojHkksJuriDate As DateTime
    ''' <summary>
    ''' 工事報告書受理日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事報告書受理日</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_juri_date")> _
    Public Property KojHkksJuriDate() As DateTime
        Get
            Return dateKojHkksJuriDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksJuriDate = value
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
    <TableMap("hosyou_nasi_riyuu")> _
    Public Property HosyouNasiRiyuu() As String
        Get
            Return strHosyouNasiRiyuu
        End Get
        Set(ByVal value As String)
            strHosyouNasiRiyuu = value
        End Set
    End Property
#End Region

#Region "表示名"
    ''' <summary>
    ''' 表示名
    ''' </summary>
    ''' <remarks></remarks>
    Private strDisplayName As String
    ''' <summary>
    ''' 表示名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 表示名</returns>
    ''' <remarks></remarks>
    <TableMap("DisplayName")> _
    Public Property DisplayName() As String
        Get
            Return strDisplayName
        End Get
        Set(ByVal value As String)
            strDisplayName = value
        End Set
    End Property
#End Region

#Region "保証書発行有無"
    ''' <summary>
    ''' 保証書発行有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証書発行有無
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_umu")> _
    Public Property HosyousyoHakUmu() As Integer
        Get
            Return intHosyousyoHakUmu
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakUmu = value
        End Set
    End Property
#End Region

#Region "保証期間(加盟店)"
    ''' <summary>
    ''' 保証期間(加盟店)
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikanMK As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証期間(加盟店)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kikan_MK")> _
    Public Property HosyouKikanMK() As Integer
        Get
            Return intHosyouKikanMK
        End Get
        Set(ByVal value As Integer)
            intHosyouKikanMK = value
        End Set
    End Property
#End Region

#Region "保証期間(地盤)"
    ''' <summary>
    ''' 保証期間(地盤)
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikanTJ As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証期間(地盤)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kikan_TJ")> _
    Public Property HosyouKikanTJ() As Integer
        Get
            Return intHosyouKikanTJ
        End Get
        Set(ByVal value As Integer)
            intHosyouKikanTJ = value
        End Set
    End Property
#End Region

#Region "依頼日"
    ''' <summary>
    ''' 依頼日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDate As DateTime
    ''' <summary>
    ''' 依頼日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼日</returns>
    ''' <remarks></remarks>
    <TableMap("irai_date")> _
    Public Property IraiDate() As DateTime
        Get
            Return dateIraiDate
        End Get
        Set(ByVal value As DateTime)
            dateIraiDate = value
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
    <TableMap("bikou")> _
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
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
    <TableMap("upd_datetime")> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "注意事項種別"
    ''' <summary>
    ''' 注意事項種別
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyuuijikouSyubetu As Integer
    ''' <summary>
    ''' 注意事項種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 注意事項種別</returns>
    ''' <remarks></remarks>
    <TableMap("tyuuijikou_syubetu")> _
    Public Property TyuuijikouSyubetu() As Integer
        Get
            Return intTyuuijikouSyubetu
        End Get
        Set(ByVal value As Integer)
            intTyuuijikouSyubetu = value
        End Set
    End Property
#End Region

#Region "発行依頼引渡日"
    ''' <summary>
    ''' 発行依頼引渡日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiHwDate As DateTime
    ''' <summary>
    ''' 発行依頼引渡日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼引渡日</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_hw_date")> _
    Public Property HakIraiHwDate() As DateTime
        Get
            Return dateHakIraiHwDate
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiHwDate = value
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
    <TableMap("hosyousyo_saihak_date")> _
    Public Property HosyousyoSaihakDate() As DateTime
        Get
            Return dateHosyousyoSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDate = value
        End Set
    End Property
#End Region

#Region "依頼物件名称"
    ''' <summary>
    ''' 依頼物件名称
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknName As String
    ''' <summary>
    ''' 依頼物件名称
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼物件名称</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_name")> _
    Public Property HakIraiBknName() As String
        Get
            Return strHakIraiBknName
        End Get
        Set(ByVal value As String)
            strHakIraiBknName = value
        End Set
    End Property
#End Region

#Region "依頼物件所在地1"
    ''' <summary>
    ''' 依頼物件所在地1
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr1 As String
    ''' <summary>
    ''' 依頼物件所在地1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼物件所在地1</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr1")> _
    Public Property HakIraiBknAdr1() As String
        Get
            Return strHakIraiBknAdr1
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr1 = value
        End Set
    End Property
#End Region

#Region "依頼物件所在地2"
    ''' <summary>
    ''' 依頼物件所在地2
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr2 As String
    ''' <summary>
    ''' 依頼物件所在地2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼物件所在地2</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr2")> _
    Public Property HakIraiBknAdr2() As String
        Get
            Return strHakIraiBknAdr2
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr2 = value
        End Set
    End Property
#End Region

#Region "依頼物件所在地3"
    ''' <summary>
    ''' 依頼物件所在地3
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr3 As String
    ''' <summary>
    ''' 依頼物件所在地2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼物件所在地3</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr3")> _
    Public Property HakIraiBknAdr3() As String
        Get
            Return strHakIraiBknAdr3
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr3 = value
        End Set
    End Property
#End Region

#Region "経由"
    ''' <summary>
    ''' 経由
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiyu As Integer
    ''' <summary>
    ''' 経由
    ''' </summary>
    ''' <value></value>
    ''' <returns> 経由</returns>
    ''' <remarks></remarks>
    <TableMap("keiyu")> _
    Public Property Keiyu() As Integer
        Get
            Return intKeiyu
        End Get
        Set(ByVal value As Integer)
            intKeiyu = value
        End Set
    End Property
#End Region

#Region "保証書NO"
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoTk As String
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no_TK")> _
    Public Property HosyousyoNoTk() As String
        Get
            Return strHosyousyoNoTk
        End Get
        Set(ByVal value As String)
            strHosyousyoNoTk = value
        End Set
    End Property
#End Region

#Region "物件状況"
    ''' <summary>
    ''' 物件状況
    ''' </summary>
    ''' <remarks></remarks>
    Private intBukkenJyky As Integer = Integer.MinValue
    ''' <summary>
    ''' 物件状況
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件状況</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyky")> _
    Public Property BukkenJyky() As Integer
        Get
            Return intBukkenJyky
        End Get
        Set(ByVal value As Integer)
            intBukkenJyky = value
        End Set
    End Property
#End Region

#Region "備考TK"
    ''' <summary>
    ''' 備考TK
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikouTk As String
    ''' <summary>
    ''' 備考TK
    ''' </summary>
    ''' <value></value>
    ''' <returns> 備考TK</returns>
    ''' <remarks></remarks>
    <TableMap("bikou_TK")> _
    Public Property BikouTk() As String
        Get
            Return strBikouTk
        End Get
        Set(ByVal value As String)
            strBikouTk = value
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
    <TableMap("data_haki_date")> _
    Public Property DataHakiDate() As DateTime
        Get
            Return dateDataHakiDate
        End Get
        Set(ByVal value As DateTime)
            dateDataHakiDate = value
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
    ''' <returns> 備考2</returns>
    ''' <remarks></remarks>
    <TableMap("bikou2")> _
    Public Property Bikou2() As String
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
    <TableMap("keiyaku_no")> _
    Public Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
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
    <TableMap("ks_koj_kanry_hkks_tyk_date")> _
    Public Property KsKojKanryHkksTykDate() As DateTime
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
    <TableMap("hosyousyo_hak_jyky")> _
    Public Property HosyousyoHakJyky() As Integer
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
    <TableMap("hosyousyo_hak_jyky_settei_date")> _
    Public Property HosyousyoHakJykySetteiDate() As DateTime
        Get
            Return dateHosyousyoHakJykySetteiDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakJykySetteiDate = value
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
    <TableMap("hosyou_umu")> _
    Public Property HosyouUmu() As Integer
        Get
            Return intHosyouUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouUmu = value
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
    <TableMap("hosyou_syouhin_umu")> _
    Public Property HosyouSyouhinUmu() As Integer
        Get
            Return intHosyouSyouhinUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouSyouhinUmu = value
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
    <TableMap("hosyousyo_hak_iraisyo_umu")> _
    Public Property HosyousyoHakIraisyoUmu() As Integer
        Get
            Return intHosyousyoHakIraisyoUmu
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakIraisyoUmu = value
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
    <TableMap("fuho_syoumeisyo_flg")> _
    Public Property FuhoSyoumeisyoFlg() As Integer
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
    <TableMap("henkou_yotei_kameiten_cd")> _
    Public Property HenkouYoteiKameitenCd() As String
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
    <TableMap("hosyou_nasi_riyuu_cd")> _
    Public Property HosyouNasiRiyuuCd() As String
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
    <TableMap("hak_irai_uke_datetime")> _
    Public Property HakIraiUkeDatetime() As DateTime
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
    <TableMap("hak_irai_can_datetime")> _
    Public Property HakIraiCanDatetime() As DateTime
        Get
            Return dateHakIraiCanDatetime
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiCanDatetime = value
        End Set
    End Property
#End Region

End Class

