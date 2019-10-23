Public Class TyousakaisyaSearchRecord

#Region "tys_kaisya_cd"
    ''' <summary>
    ''' 調査会社ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' 調査会社ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社ｺｰﾄﾞ</returns>
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

#Region "jigyousyo_cd"
    ''' <summary>
    ''' 事業所ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strJigyousyoCd As String
    ''' <summary>
    ''' 事業所ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 事業所ｺｰﾄﾞ</returns>
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

#Region "torikesi"
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

#Region "tys_kaisya_mei"
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

#Region "tys_kaisya_mei_kana"
    ''' <summary>
    ''' 調査会社名ｶﾅ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaMeiKana As String
    ''' <summary>
    ''' 調査会社名ｶﾅ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社名ｶﾅ</returns>
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
    Private strSskSiharaiMei As String
    ''' <summary>
    ''' 請求先支払先名
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先支払先名</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_shri_saki_mei")> _
    Public Property SeikyuuSakiSiharaiMei() As String
        Get
            Return strSskSiharaiMei
        End Get
        Set(ByVal value As String)
            strSskSiharaiMei = value
        End Set
    End Property
#End Region

#Region "請求先支払先名カナ"
    ''' <summary>
    ''' 請求先支払先名カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSskSiharaiMeiKana As String
    ''' <summary>
    ''' 請求先支払先名カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先支払先名カナ</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_shri_saki_kana")> _
    Public Property SeikyuuSakiSiharaiMeiKana() As String
        Get
            Return strSskSiharaiMeiKana
        End Get
        Set(ByVal value As String)
            strSskSiharaiMeiKana = value
        End Set
    End Property
#End Region

#Region "jyuusyo1"
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

#Region "jyuusyo2"
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

#Region "yuubin_no"
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

#Region "tel_no"
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

#Region "fax_no"
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

#Region "pca_siiresaki_cd"
    ''' <summary>
    ''' PCA用仕入先ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strPcaSiiresakiCd As String
    ''' <summary>
    ''' PCA用仕入先ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> PCA用仕入先ｺｰﾄﾞ</returns>
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

#Region "pca_seikyuu_cd"
    ''' <summary>
    ''' PCA請求先ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strPcaSeikyuuCd As String
    ''' <summary>
    ''' PCA請求先ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> PCA請求先ｺｰﾄﾞ</returns>
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
    ''' <returns>請求先コード</returns>
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
    ''' <returns>請求先枝番</returns>
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
    ''' <returns>請求先区分</returns>
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

#Region "seikyuu_sime_date"
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

#Region "ss_kijyun_kkk"
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

#Region "fc_ten_cd"
    ''' <summary>
    ''' FC店ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strFcTenCd As String
    ''' <summary>
    ''' FC店ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> FC店ｺｰﾄﾞ</returns>
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

#Region "可否区分"
    ''' <summary>
    ''' 可否区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intKahiKbn As Integer
    ''' <summary>
    ''' 可否区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 可否区分</returns>
    ''' <remarks></remarks>
    <TableMap("kahi_kbn")> _
    Public Property KahiKbn() As Integer
        Get
            Return intKahiKbn
        End Get
        Set(ByVal value As Integer)
            intKahiKbn = value
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

End Class