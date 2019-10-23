Public Class TysKaisyaRecord

#Region "調査会社コード"
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社コード</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd")> _
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "事業所コード"
    ''' <summary>
    ''' 事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strJigyousyoCd As String
    ''' <summary>
    ''' 事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("jigyousyo_cd")> _
    Public Property JigyousyoCd() As String
        Get
            Return strJigyousyoCd
        End Get
        Set(ByVal value As String)
            strJigyousyoCd = value
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
    <TableMap("torikesi")> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "調査会社名"
    ''' <summary>
    ''' 調査会社名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaMei As String
    ''' <summary>
    ''' 調査会社名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社名</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_mei")> _
    Public Property TysKaisyaMei() As String
        Get
            Return strTysKaisyaMei
        End Get
        Set(ByVal value As String)
            strTysKaisyaMei = value
        End Set
    End Property
#End Region

#Region "調査会社名カナ"
    ''' <summary>
    ''' 調査会社名カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaMeiKana As String
    ''' <summary>
    ''' 調査会社名カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社名カナ</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_mei_kana")> _
    Public Property TysKaisyaMeiKana() As String
        Get
            Return strTysKaisyaMeiKana
        End Get
        Set(ByVal value As String)
            strTysKaisyaMeiKana = value
        End Set
    End Property
#End Region

#Region "請求先支払先名"
    ''' <summary>
    ''' 請求先支払先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiShriSakiMei As String
    ''' <summary>
    ''' 請求先支払先名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先支払先名</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_shri_saki_mei")> _
    Public Property SeikyuuSakiShriSakiMei() As String
        Get
            Return strSeikyuuSakiShriSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiShriSakiMei = value
        End Set
    End Property
#End Region

#Region "請求先支払先名カナ"
    ''' <summary>
    ''' 請求先支払先名カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiShriSakiKana As String
    ''' <summary>
    ''' 請求先支払先名カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先支払先名カナ</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_shri_saki_kana")> _
    Public Property SeikyuuSakiShriSakiKana() As String
        Get
            Return strSeikyuuSakiShriSakiKana
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiShriSakiKana = value
        End Set
    End Property
#End Region

#Region "住所1"
    ''' <summary>
    ''' 住所1
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyo1 As String
    ''' <summary>
    ''' 住所1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 住所1</returns>
    ''' <remarks></remarks>
    <TableMap("jyuusyo1")> _
    Public Property Jyuusyo1() As String
        Get
            Return strJyuusyo1
        End Get
        Set(ByVal value As String)
            strJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "住所2"
    ''' <summary>
    ''' 住所2
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyo2 As String
    ''' <summary>
    ''' 住所2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 住所2</returns>
    ''' <remarks></remarks>
    <TableMap("jyuusyo2")> _
    Public Property Jyuusyo2() As String
        Get
            Return strJyuusyo2
        End Get
        Set(ByVal value As String)
            strJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "郵便番号"
    ''' <summary>
    ''' 郵便番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strYuubinNo As String
    ''' <summary>
    ''' 郵便番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 郵便番号</returns>
    ''' <remarks></remarks>
    <TableMap("yuubin_no")> _
    Public Property YuubinNo() As String
        Get
            Return strYuubinNo
        End Get
        Set(ByVal value As String)
            strYuubinNo = value
        End Set
    End Property
#End Region

#Region "電話番号"
    ''' <summary>
    ''' 電話番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strTelNo As String
    ''' <summary>
    ''' 電話番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 電話番号</returns>
    ''' <remarks></remarks>
    <TableMap("tel_no")> _
    Public Property TelNo() As String
        Get
            Return strTelNo
        End Get
        Set(ByVal value As String)
            strTelNo = value
        End Set
    End Property
#End Region

#Region "FAX番号"
    ''' <summary>
    ''' FAX番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strFaxNo As String
    ''' <summary>
    ''' FAX番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> FAX番号</returns>
    ''' <remarks></remarks>
    <TableMap("fax_no")> _
    Public Property FaxNo() As String
        Get
            Return strFaxNo
        End Get
        Set(ByVal value As String)
            strFaxNo = value
        End Set
    End Property
#End Region

#Region "PCA用仕入先コード"
    ''' <summary>
    ''' PCA用仕入先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strPcaSiiresakiCd As String
    ''' <summary>
    ''' PCA用仕入先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> PCA用仕入先コード</returns>
    ''' <remarks></remarks>
    <TableMap("pca_siiresaki_cd")> _
    Public Property PcaSiiresakiCd() As String
        Get
            Return strPcaSiiresakiCd
        End Get
        Set(ByVal value As String)
            strPcaSiiresakiCd = value
        End Set
    End Property
#End Region

#Region "PCA請求先コード"
    ''' <summary>
    ''' PCA請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strPcaSeikyuuCd As String
    ''' <summary>
    ''' PCA請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> PCA請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("pca_seikyuu_cd")> _
    Public Property PcaSeikyuuCd() As String
        Get
            Return strPcaSeikyuuCd
        End Get
        Set(ByVal value As String)
            strPcaSeikyuuCd = value
        End Set
    End Property
#End Region

#Region "請求先コード"
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd")> _
    Public Property SeikyuuSakiCd() As String
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
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc")> _
    Public Property SeikyuuSakiBrc() As String
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
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "請求先名"
    ''' <summary>
    ''' 請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei As String
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

#Region "請求締め日"
    ''' <summary>
    ''' 請求締め日
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSimeDate As String
    ''' <summary>
    ''' 請求締め日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求締め日</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_sime_date")> _
    Public Property SeikyuuSimeDate() As String
        Get
            Return strSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "請求書送付先住所1"
    ''' <summary>
    ''' 請求書送付先住所1
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuJyuusyo1 As String
    ''' <summary>
    ''' 請求書送付先住所1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書送付先住所1</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_jyuusyo1")> _
    Public Property SkysySoufuJyuusyo1() As String
        Get
            Return strSkysySoufuJyuusyo1
        End Get
        Set(ByVal value As String)
            strSkysySoufuJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "請求書送付先住所2"
    ''' <summary>
    ''' 請求書送付先住所2
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuJyuusyo2 As String
    ''' <summary>
    ''' 請求書送付先住所2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書送付先住所2</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_jyuusyo2")> _
    Public Property SkysySoufuJyuusyo2() As String
        Get
            Return strSkysySoufuJyuusyo2
        End Get
        Set(ByVal value As String)
            strSkysySoufuJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "請求書送付先郵便番号"
    ''' <summary>
    ''' 請求書送付先郵便番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuYuubinNo As String
    ''' <summary>
    ''' 請求書送付先郵便番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書送付先郵便番号</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_yuubin_no")> _
    Public Property SkysySoufuYuubinNo() As String
        Get
            Return strSkysySoufuYuubinNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuYuubinNo = value
        End Set
    End Property
#End Region

#Region "請求書送付先電話番号"
    ''' <summary>
    ''' 請求書送付先電話番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuTelNo As String
    ''' <summary>
    ''' 請求書送付先電話番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書送付先電話番号</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_tel_no")> _
    Public Property SkysySoufuTelNo() As String
        Get
            Return strSkysySoufuTelNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuTelNo = value
        End Set
    End Property
#End Region

#Region "新会計支払先コード"
    ''' <summary>
    ''' 新会計支払先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkShriSakiCd As String
    ''' <summary>
    ''' 新会計支払先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新会計支払先コード</returns>
    ''' <remarks></remarks>
    <TableMap("skk_shri_saki_cd")> _
    Public Property SkkShriSakiCd() As String
        Get
            Return strSkkShriSakiCd
        End Get
        Set(ByVal value As String)
            strSkkShriSakiCd = value
        End Set
    End Property
#End Region

#Region "新会計事業所コード"
    ''' <summary>
    ''' 新会計事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkJigyousyoCd As String
    ''' <summary>
    ''' 新会計事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新会計事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("skk_jigyousyo_cd")> _
    Public Property SkkJigyousyoCd() As String
        Get
            Return strSkkJigyousyoCd
        End Get
        Set(ByVal value As String)
            strSkkJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "支払明細集計先事業所コード"
    ''' <summary>
    ''' 支払明細集計先事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriMeisaiJigyousyoCd As String
    ''' <summary>
    ''' 支払明細集計先事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払明細集計先事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("shri_meisai_jigyousyo_cd")> _
    Public Property ShriMeisaiJigyousyoCd() As String
        Get
            Return strShriMeisaiJigyousyoCd
        End Get
        Set(ByVal value As String)
            strShriMeisaiJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "支払集計先事業所コード"
    ''' <summary>
    ''' 支払集計先事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriJigyousyoCd As String
    ''' <summary>
    ''' 支払集計先事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払集計先事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("shri_jigyousyo_cd")> _
    Public Property ShriJigyousyoCd() As String
        Get
            Return strShriJigyousyoCd
        End Get
        Set(ByVal value As String)
            strShriJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "支払締め日"
    ''' <summary>
    ''' 支払締め日
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriSimeDate As String
    ''' <summary>
    ''' 支払締め日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払締め日</returns>
    ''' <remarks></remarks>
    <TableMap("shri_sime_date")> _
    Public Property ShriSimeDate() As String
        Get
            Return strShriSimeDate
        End Get
        Set(ByVal value As String)
            strShriSimeDate = value
        End Set
    End Property
#End Region

#Region "支払予定月数"
    ''' <summary>
    ''' 支払予定月数
    ''' </summary>
    ''' <remarks></remarks>
    Private intShriYoteiGessuu As Integer
    ''' <summary>
    ''' 支払予定月数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払予定月数</returns>
    ''' <remarks></remarks>
    <TableMap("shri_yotei_gessuu")> _
    Public Property ShriYoteiGessuu() As Integer
        Get
            Return intShriYoteiGessuu
        End Get
        Set(ByVal value As Integer)
            intShriYoteiGessuu = value
        End Set
    End Property
#End Region

#Region "ファクタリング開始年月"
    ''' <summary>
    ''' ファクタリング開始年月
    ''' </summary>
    ''' <remarks></remarks>
    Private dateFctringKaisiNengetu As DateTime
    ''' <summary>
    ''' ファクタリング開始年月
    ''' </summary>
    ''' <value></value>
    ''' <returns> ファクタリング開始年月</returns>
    ''' <remarks></remarks>
    <TableMap("fctring_kaisi_nengetu")> _
    Public Property FctringKaisiNengetu() As DateTime
        Get
            Return dateFctringKaisiNengetu
        End Get
        Set(ByVal value As DateTime)
            dateFctringKaisiNengetu = value
        End Set
    End Property
#End Region

#Region "支払用FAX番号"
    ''' <summary>
    ''' 支払用FAX番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriYouFaxNo As String
    ''' <summary>
    ''' 支払用FAX番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払用FAX番号</returns>
    ''' <remarks></remarks>
    <TableMap("shri_you_fax_no")> _
    Public Property ShriYouFaxNo() As String
        Get
            Return strShriYouFaxNo
        End Get
        Set(ByVal value As String)
            strShriYouFaxNo = value
        End Set
    End Property
#End Region

#Region "SS基準価格"
    ''' <summary>
    ''' SS基準価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intSsKijyunKkk As Integer
    ''' <summary>
    ''' SS基準価格
    ''' </summary>
    ''' <value></value>
    ''' <returns> SS基準価格</returns>
    ''' <remarks></remarks>
    <TableMap("ss_kijyun_kkk")> _
    Public Property SsKijyunKkk() As Integer
        Get
            Return intSsKijyunKkk
        End Get
        Set(ByVal value As Integer)
            intSsKijyunKkk = value
        End Set
    End Property
#End Region

#Region "FC店コード"
    ''' <summary>
    ''' FC店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strFcTenCd As String
    ''' <summary>
    ''' FC店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> FC店コード</returns>
    ''' <remarks></remarks>
    <TableMap("fc_ten_cd")> _
    Public Property FcTenCd() As String
        Get
            Return strFcTenCd
        End Get
        Set(ByVal value As String)
            strFcTenCd = value
        End Set
    End Property
#End Region

#Region "検査センターコード"
    ''' <summary>
    ''' 検査センターコード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKensaCenterCd As String
    ''' <summary>
    ''' 検査センターコード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 検査センターコード</returns>
    ''' <remarks></remarks>
    <TableMap("kensa_center_cd")> _
    Public Property KensaCenterCd() As String
        Get
            Return strKensaCenterCd
        End Get
        Set(ByVal value As String)
            strKensaCenterCd = value
        End Set
    End Property
#End Region

#Region "工事報告書直送"
    ''' <summary>
    ''' 工事報告書直送
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojHkksTyokusouFlg As Integer
    ''' <summary>
    ''' 工事報告書直送
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事報告書直送</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_tyokusou_flg")> _
    Public Property KojHkksTyokusouFlg() As Integer
        Get
            Return intKojHkksTyokusouFlg
        End Get
        Set(ByVal value As Integer)
            intKojHkksTyokusouFlg = value
        End Set
    End Property
#End Region

#Region "工事報告書直送変更ログインユーザーID"
    ''' <summary>
    ''' 工事報告書直送変更ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojHkksTyokusouUpdLoginUserId As String
    ''' <summary>
    ''' 工事報告書直送変更ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事報告書直送変更ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_tyokusou_upd_login_user_id")> _
    Public Property KojHkksTyokusouUpdLoginUserId() As String
        Get
            Return strKojHkksTyokusouUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strKojHkksTyokusouUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "工事報告書直送変更日時"
    ''' <summary>
    ''' 工事報告書直送変更日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojHkksTyokusouUpdDatetime As DateTime
    ''' <summary>
    ''' 工事報告書直送変更日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事報告書直送変更日時</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_tyokusou_upd_datetime")> _
    Public Property KojHkksTyokusouUpdDatetime() As DateTime
        Get
            Return dateKojHkksTyokusouUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksTyokusouUpdDatetime = value
        End Set
    End Property
#End Region

#Region "調査会社フラグ"
    ''' <summary>
    ''' 調査会社フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysKaisyaFlg As Integer
    ''' <summary>
    ''' 調査会社フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_flg")> _
    Public Property TysKaisyaFlg() As Integer
        Get
            Return intTysKaisyaFlg
        End Get
        Set(ByVal value As Integer)
            intTysKaisyaFlg = value
        End Set
    End Property
#End Region

#Region "工事会社フラグ"
    ''' <summary>
    ''' 工事会社フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojKaisyaFlg As Integer
    ''' <summary>
    ''' 工事会社フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("koj_kaisya_flg")> _
    Public Property KojKaisyaFlg() As Integer
        Get
            Return intKojKaisyaFlg
        End Get
        Set(ByVal value As Integer)
            intKojKaisyaFlg = value
        End Set
    End Property
#End Region

#Region "JAPAN会区分"
    ''' <summary>
    ''' JAPAN会区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intJapanKaiKbn As Integer
    ''' <summary>
    ''' JAPAN会区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> JAPAN会区分</returns>
    ''' <remarks></remarks>
    <TableMap("japan_kai_kbn")> _
    Public Property JapanKaiKbn() As Integer
        Get
            Return intJapanKaiKbn
        End Get
        Set(ByVal value As Integer)
            intJapanKaiKbn = value
        End Set
    End Property
#End Region

#Region "JAPAN会入会年月"
    ''' <summary>
    ''' JAPAN会入会年月
    ''' </summary>
    ''' <remarks></remarks>
    Private dateJapanKaiNyuukaiDate As DateTime
    ''' <summary>
    ''' JAPAN会入会年月
    ''' </summary>
    ''' <value></value>
    ''' <returns> JAPAN会入会年月</returns>
    ''' <remarks></remarks>
    <TableMap("japan_kai_nyuukai_date")> _
    Public Property JapanKaiNyuukaiDate() As DateTime
        Get
            Return dateJapanKaiNyuukaiDate
        End Get
        Set(ByVal value As DateTime)
            dateJapanKaiNyuukaiDate = value
        End Set
    End Property
#End Region

#Region "JAPAN会退会年月"
    ''' <summary>
    ''' JAPAN会退会年月
    ''' </summary>
    ''' <remarks></remarks>
    Private dateJapanKaiTaikaiDate As DateTime
    ''' <summary>
    ''' JAPAN会退会年月
    ''' </summary>
    ''' <value></value>
    ''' <returns> JAPAN会退会年月</returns>
    ''' <remarks></remarks>
    <TableMap("japan_kai_taikai_date")> _
    Public Property JapanKaiTaikaiDate() As DateTime
        Get
            Return dateJapanKaiTaikaiDate
        End Get
        Set(ByVal value As DateTime)
            dateJapanKaiTaikaiDate = value
        End Set
    End Property
#End Region

#Region "FC店区分"
    ''' <summary>
    ''' FC店区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intFcTenKbn As Integer
    ''' <summary>
    ''' FC店区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> FC店区分</returns>
    ''' <remarks></remarks>
    <TableMap("fc_ten_kbn")> _
    Public Property FcTenKbn() As Integer
        Get
            Return intFcTenKbn
        End Get
        Set(ByVal value As Integer)
            intFcTenKbn = value
        End Set
    End Property
#End Region

#Region "FC入会年月"
    ''' <summary>
    ''' FC入会年月
    ''' </summary>
    ''' <remarks></remarks>
    Private dateFcNyuukaiDate As DateTime
    ''' <summary>
    ''' FC入会年月
    ''' </summary>
    ''' <value></value>
    ''' <returns> FC入会年月</returns>
    ''' <remarks></remarks>
    <TableMap("fc_nyuukai_date")> _
    Public Property FcNyuukaiDate() As DateTime
        Get
            Return dateFcNyuukaiDate
        End Get
        Set(ByVal value As DateTime)
            dateFcNyuukaiDate = value
        End Set
    End Property
#End Region

#Region "FC退会年月"
    ''' <summary>
    ''' FC退会年月
    ''' </summary>
    ''' <remarks></remarks>
    Private dateFcTaikaiDate As DateTime
    ''' <summary>
    ''' FC退会年月
    ''' </summary>
    ''' <value></value>
    ''' <returns> FC退会年月</returns>
    ''' <remarks></remarks>
    <TableMap("fc_taikai_date")> _
    Public Property FcTaikaiDate() As DateTime
        Get
            Return dateFcTaikaiDate
        End Get
        Set(ByVal value As DateTime)
            dateFcTaikaiDate = value
        End Set
    End Property
#End Region

#Region "取消理由"
    ''' <summary>
    ''' 取消理由
    ''' </summary>
    ''' <remarks></remarks>
    Private strTorikesiRiyuu As String
    ''' <summary>
    ''' 取消理由
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消理由</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi_riyuu")> _
    Public Property TorikesiRiyuu() As String
        Get
            Return strTorikesiRiyuu
        End Get
        Set(ByVal value As String)
            strTorikesiRiyuu = value
        End Set
    End Property
#End Region

#Region "ReportJHSトークン有無フラグ"
    ''' <summary>
    ''' ReportJHSトークン有無フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intReportJhsTokenFlg As Integer
    ''' <summary>
    ''' ReportJHSトークン有無フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportJHSトークン有無フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("report_jhs_token_flg")> _
    Public Property ReportJhsTokenFlg() As Integer
        Get
            Return intReportJhsTokenFlg
        End Get
        Set(ByVal value As Integer)
            intReportJhsTokenFlg = value
        End Set
    End Property
#End Region

#Region "宅地地盤調査主任資格有無フラグ"
    ''' <summary>
    ''' 宅地地盤調査主任資格有無フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTktJbnTysSyuninSkkFlg As Integer
    ''' <summary>
    ''' 宅地地盤調査主任資格有無フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 宅地地盤調査主任資格有無フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("tkt_jbn_tys_syunin_skk_flg")> _
    Public Property TktJbnTysSyuninSkkFlg() As Integer
        Get
            Return intTktJbnTysSyuninSkkFlg
        End Get
        Set(ByVal value As Integer)
            intTktJbnTysSyuninSkkFlg = value
        End Set
    End Property
#End Region

End Class