''' <summary>
''' 加盟店マスタ検索レコードクラス
''' </summary>
''' <remarks></remarks>
Public Class KameitenSearchRecord

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

#Region "取消理由"
    ''' <summary>
    ''' 取消理由[拡張名称マスタ.名称種別=56]
    ''' </summary>
    ''' <remarks></remarks>
    Private strKtTorikesiRiyuu As String = String.Empty
    ''' <summary>
    ''' 取消理由
    ''' </summary>
    ''' <value></value>
    ''' <returns>取消理由</returns>
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

#Region "店名ｶﾅ1"
    ''' <summary>
    ''' 店名ｶﾅ1
    ''' </summary>
    ''' <remarks></remarks>
    Private strTenmeiKana1 As String
    ''' <summary>
    ''' 店名ｶﾅ1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 店名ｶﾅ1</returns>
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

#Region "加盟店名2"
    ''' <summary>
    ''' 加盟店名2
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei2 As String
    ''' <summary>
    ''' 加盟店名2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店名2</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei2")> _
    Public Property KameitenMei2() As String
        Get
            Return strKameitenMei2
        End Get
        Set(ByVal value As String)
            strKameitenMei2 = value
        End Set
    End Property
#End Region

#Region "店名ｶﾅ2"
    ''' <summary>
    ''' 店名ｶﾅ2
    ''' </summary>
    ''' <remarks></remarks>
    Private strTenmeiKana2 As String
    ''' <summary>
    ''' 店名ｶﾅ2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 店名ｶﾅ2</returns>
    ''' <remarks></remarks>
    <TableMap("tenmei_kana2")> _
    Public Property TenmeiKana2() As String
        Get
            Return strTenmeiKana2
        End Get
        Set(ByVal value As String)
            strTenmeiKana2 = value
        End Set
    End Property
#End Region

#Region "加盟店正式名"
    ''' <summary>
    ''' 加盟店正式名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenSeisikiMei As String
    ''' <summary>
    ''' 加盟店正式名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店正式名</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_seisiki_mei")> _
    Public Property KameitenSeisikiMei() As String
        Get
            Return strKameitenSeisikiMei
        End Get
        Set(ByVal value As String)
            strKameitenSeisikiMei = value
        End Set
    End Property
#End Region

#Region "加盟店正式名カナ"
    ''' <summary>
    ''' 加盟店正式名カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenSeisikiMeiKana As String
    ''' <summary>
    ''' 加盟店正式名カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店正式名カナ</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_seisiki_mei_kana")> _
    Public Property KameitenSeisikiMeiKana() As String
        Get
            Return strKameitenSeisikiMeiKana
        End Get
        Set(ByVal value As String)
            strKameitenSeisikiMeiKana = value
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

#Region "メールアドレス"
    ''' <summary>
    ''' メールアドレス
    ''' </summary>
    ''' <remarks></remarks>
    Private strMailAddress As String
    ''' <summary>
    ''' メールアドレス
    ''' </summary>
    ''' <value></value>
    ''' <returns> メールアドレス</returns>
    ''' <remarks></remarks>
    <TableMap("mail_address")> _
    Public Property MailAddress() As String
        Get
            Return strMailAddress
        End Get
        Set(ByVal value As String)
            strMailAddress = value
        End Set
    End Property
#End Region

#Region "ﾋﾞﾙﾀﾞｰNO"
    ''' <summary>
    ''' ﾋﾞﾙﾀﾞｰNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strBuilderNo As String
    ''' <summary>
    ''' ﾋﾞﾙﾀﾞｰNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ﾋﾞﾙﾀﾞｰNO</returns>
    ''' <remarks></remarks>
    <TableMap("builder_no")> _
    Public Property BuilderNo() As String
        Get
            Return strBuilderNo
        End Get
        Set(ByVal value As String)
            strBuilderNo = value
        End Set
    End Property
#End Region

#Region "ビルダー情報件数"
    ''' <summary>
    ''' ビルダー情報件数
    ''' </summary>
    ''' <remarks></remarks>
    Private intBuilderCount As Integer
    ''' <summary>
    ''' ビルダー情報件数
    ''' </summary>
    ''' <value>ビルダー情報件数</value>
    ''' <returns>ビルダー情報件数</returns>
    ''' <remarks>ビルダー情報が存在する場合、明細件数が設定されます</remarks>
    <TableMap("builder_count")> _
    Public Property BuilderCount() As Integer
        Get
            Return intBuilderCount
        End Get
        Set(ByVal value As Integer)
            intBuilderCount = value
        End Set
    End Property
#End Region

#Region "系列名"
    ''' <summary>
    ''' 系列名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuMei As String
    ''' <summary>
    ''' 系列名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 系列名</returns>
    ''' <remarks></remarks>
    <TableMap("keiretu_mei")> _
    Public Property KeiretuMei() As String
        Get
            Return strKeiretuMei
        End Get
        Set(ByVal value As String)
            strKeiretuMei = value
        End Set
    End Property
#End Region

#Region "営業所コード"
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoCd As String
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("eigyousyo_cd")> _
    Public Property EigyousyoCd() As String
        Get
            Return strEigyousyoCd
        End Get
        Set(ByVal value As String)
            strEigyousyoCd = value
        End Set
    End Property
#End Region

#Region "営業所名"
    ''' <summary>
    ''' 営業所名
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoMei As String
    ''' <summary>
    ''' 営業所名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業所名</returns>
    ''' <remarks></remarks>
    <TableMap("eigyousyo_mei")> _
    Public Property EigyousyoMei() As String
        Get
            Return strEigyousyoMei
        End Get
        Set(ByVal value As String)
            strEigyousyoMei = value
        End Set
    End Property
#End Region

#Region "系列ｺｰﾄﾞ"
    ''' <summary>
    ''' 系列ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' 系列ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 系列ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("keiretu_cd")> _
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region

#Region "TH瑕疵コード"
    ''' <summary>
    ''' TH瑕疵コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strThKasiCd As String
    ''' <summary>
    ''' TH瑕疵コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> TH瑕疵コード</returns>
    ''' <remarks></remarks>
    <TableMap("th_kasi_cd")> _
    Public Property ThKasiCd() As String
        Get
            Return strThKasiCd
        End Get
        Set(ByVal value As String)
            strThKasiCd = value
        End Set
    End Property
#End Region

#Region "断面図1"
    ''' <summary>
    ''' 断面図1
    ''' </summary>
    ''' <remarks></remarks>
    Private intDanmenzu1 As Integer
    ''' <summary>
    ''' 断面図1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 断面図1</returns>
    ''' <remarks></remarks>
    <TableMap("danmenzu1")> _
    Public Property Danmenzu1() As Integer
        Get
            Return intDanmenzu1
        End Get
        Set(ByVal value As Integer)
            intDanmenzu1 = value
        End Set
    End Property
#End Region

#Region "断面図2"
    ''' <summary>
    ''' 断面図2
    ''' </summary>
    ''' <remarks></remarks>
    Private intDanmenzu2 As Integer
    ''' <summary>
    ''' 断面図2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 断面図2</returns>
    ''' <remarks></remarks>
    <TableMap("danmenzu2")> _
    Public Property Danmenzu2() As Integer
        Get
            Return intDanmenzu2
        End Get
        Set(ByVal value As Integer)
            intDanmenzu2 = value
        End Set
    End Property
#End Region

#Region "断面図3"
    ''' <summary>
    ''' 断面図3
    ''' </summary>
    ''' <remarks></remarks>
    Private intDanmenzu3 As Integer
    ''' <summary>
    ''' 断面図3
    ''' </summary>
    ''' <value></value>
    ''' <returns> 断面図3</returns>
    ''' <remarks></remarks>
    <TableMap("danmenzu3")> _
    Public Property Danmenzu3() As Integer
        Get
            Return intDanmenzu3
        End Get
        Set(ByVal value As Integer)
            intDanmenzu3 = value
        End Set
    End Property
#End Region

#Region "断面図4"
    ''' <summary>
    ''' 断面図4
    ''' </summary>
    ''' <remarks></remarks>
    Private intDanmenzu4 As Integer
    ''' <summary>
    ''' 断面図4
    ''' </summary>
    ''' <value></value>
    ''' <returns> 断面図4</returns>
    ''' <remarks></remarks>
    <TableMap("danmenzu4")> _
    Public Property Danmenzu4() As Integer
        Get
            Return intDanmenzu4
        End Get
        Set(ByVal value As Integer)
            intDanmenzu4 = value
        End Set
    End Property
#End Region

#Region "断面図5"
    ''' <summary>
    ''' 断面図5
    ''' </summary>
    ''' <remarks></remarks>
    Private intDanmenzu5 As Integer
    ''' <summary>
    ''' 断面図5
    ''' </summary>
    ''' <value></value>
    ''' <returns> 断面図5</returns>
    ''' <remarks></remarks>
    <TableMap("danmenzu5")> _
    Public Property Danmenzu5() As Integer
        Get
            Return intDanmenzu5
        End Get
        Set(ByVal value As Integer)
            intDanmenzu5 = value
        End Set
    End Property
#End Region

#Region "断面図6"
    ''' <summary>
    ''' 断面図6
    ''' </summary>
    ''' <remarks></remarks>
    Private intDanmenzu6 As Integer
    ''' <summary>
    ''' 断面図6
    ''' </summary>
    ''' <value></value>
    ''' <returns> 断面図6</returns>
    ''' <remarks></remarks>
    <TableMap("danmenzu6")> _
    Public Property Danmenzu6() As Integer
        Get
            Return intDanmenzu6
        End Get
        Set(ByVal value As Integer)
            intDanmenzu6 = value
        End Set
    End Property
#End Region

#Region "断面図7"
    ''' <summary>
    ''' 断面図7
    ''' </summary>
    ''' <remarks></remarks>
    Private intDanmenzu7 As Integer
    ''' <summary>
    ''' 断面図7
    ''' </summary>
    ''' <value></value>
    ''' <returns> 断面図7</returns>
    ''' <remarks></remarks>
    <TableMap("danmenzu7")> _
    Public Property Danmenzu7() As Integer
        Get
            Return intDanmenzu7
        End Get
        Set(ByVal value As Integer)
            intDanmenzu7 = value
        End Set
    End Property
#End Region

#Region "工事担当FLG"
    ''' <summary>
    ''' 工事担当FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojTantouFlg As Integer
    ''' <summary>
    ''' 工事担当FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事担当FLG</returns>
    ''' <remarks></remarks>
    <TableMap("koj_tantou_flg")> _
    Public Property KojTantouFlg() As Integer
        Get
            Return intKojTantouFlg
        End Get
        Set(ByVal value As Integer)
            intKojTantouFlg = value
        End Set
    End Property
#End Region

#Region "調査見積書FLG"
    ''' <summary>
    ''' 調査見積書FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysMitsyoFlg As Integer
    ''' <summary>
    ''' 調査見積書FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査見積書FLG</returns>
    ''' <remarks></remarks>
    <TableMap("tys_mitsyo_flg")> _
    Public Property TysMitsyoFlg() As Integer
        Get
            Return intTysMitsyoFlg
        End Get
        Set(ByVal value As Integer)
            intTysMitsyoFlg = value
        End Set
    End Property
#End Region

#Region "調査見積書NGメッセージ"
    ''' <summary>
    ''' 調査見積書NGメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysMitsyoMsg As String
    ''' <summary>
    ''' 調査見積書NGメッセージ
    ''' </summary>
    ''' <value>調査見積書NGメッセージ</value>
    ''' <returns>調査見積書NGメッセージ</returns>
    ''' <remarks>調査見積書FLGが0以外の場合、メッセージが設定されます</remarks>
    <TableMap("tys_mitsyo_msg")> _
    Public Property TysMitsyoMsg() As String
        Get
            Return strTysMitsyoMsg
        End Get
        Set(ByVal value As String)
            strTysMitsyoMsg = value
        End Set
    End Property
#End Region

#Region "発注書FLG"
    ''' <summary>
    ''' 発注書FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoFlg As Integer
    ''' <summary>
    ''' 発注書FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発注書FLG</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_flg")> _
    Public Property HattyuusyoFlg() As Integer
        Get
            Return intHattyuusyoFlg
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoFlg = value
        End Set
    End Property
#End Region

#Region "発注書NGメッセージ"
    ''' <summary>
    ''' 発注書NGメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Private strHattyuusyoMsg As String
    ''' <summary>
    ''' 発注書NGメッセージ
    ''' </summary>
    ''' <value>発注書NGメッセージ</value>
    ''' <returns>発注書NGメッセージ</returns>
    ''' <remarks>発注書FLGが0以外の場合、メッセージが設定されます</remarks>
    <TableMap("hattyuusyo_msg")> _
    Public Property HattyuusyoMsg() As String
        Get
            Return strHattyuusyoMsg
        End Get
        Set(ByVal value As String)
            strHattyuusyoMsg = value
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

#Region "調査請求先"
    ''' <summary>
    ''' 調査請求先
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysSeikyuuSaki As String
    ''' <summary>
    ''' 調査請求先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査請求先</returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki")> _
    Public Property TysSeikyuuSaki() As String
        Get
            Return strTysSeikyuuSaki
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSaki = value
        End Set
    End Property
#End Region

#Region "調査請求先コード"
    ''' <summary>
    ''' 調査請求先コード
    ''' </summary>
    ''' <remarks>調査請求先コード</remarks>
    Private strTysSeikyuuSakiCd As String
    ''' <summary>
    ''' 調査請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki_cd")> _
    Public Property TysSeikyuuSakiCd() As String
        Get
            Return strTysSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "調査請求先枝番"
    ''' <summary>
    ''' 調査請求先枝番
    ''' </summary>
    ''' <remarks>調査請求先枝番</remarks>
    Private strTysSeikyuuSakiBrc As String
    ''' <summary>
    ''' 調査請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki_brc")> _
    Public Property TysSeikyuuSakiBrc() As String
        Get
            Return strTysSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "調査請求先区分"
    ''' <summary>
    ''' 調査請求先区分
    ''' </summary>
    ''' <remarks>調査請求先区分</remarks>
    Private strTysSeikyuuSakiKbn As String
    ''' <summary>
    ''' 調査請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki_kbn")> _
    Public Property TysSeikyuuSakiKbn() As String
        Get
            Return strTysSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "調査請求締め日"
    ''' <summary>
    ''' 調査請求締め日
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysSeikyuuSimeDate As String
    ''' <summary>
    ''' 調査請求締め日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査請求締め日</returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_sime_date")> _
    Public Property TysSeikyuuSimeDate() As String
        Get
            Return strTysSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "工事請求先"
    ''' <summary>
    ''' 工事請求先
    ''' </summary>
    ''' <remarks></remarks>
    Private strKoujiSeikyuuSaki As String
    ''' <summary>
    ''' 工事請求先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事請求先</returns>
    ''' <remarks></remarks>
    <TableMap("koj_seikyuusaki")> _
    Public Property KoujiSeikyuuSaki() As String
        Get
            Return strKoujiSeikyuuSaki
        End Get
        Set(ByVal value As String)
            strKoujiSeikyuuSaki = value
        End Set
    End Property
#End Region

#Region "工事請求先コード"
    ''' <summary>
    ''' 工事請求先コード
    ''' </summary>
    ''' <remarks>工事請求先コード</remarks>
    Private strKojSeikyuuSakiCd As String
    ''' <summary>
    ''' 工事請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("koj_seikyuu_saki_cd")> _
    Public Property KojSeikyuuSakiCd() As String
        Get
            Return strKojSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strKojSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "工事請求先枝番"
    ''' <summary>
    ''' 工事請求先枝番
    ''' </summary>
    ''' <remarks>工事請求先枝番</remarks>
    Private strKojSeikyuuSakiBrc As String
    ''' <summary>
    ''' 工事請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("koj_seikyuu_saki_brc")> _
    Public Property KojSeikyuuSakiBrc() As String
        Get
            Return strKojSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strKojSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "工事請求先区分"
    ''' <summary>
    ''' 工事請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojSeikyuuSakiKbn As String
    ''' <summary>
    ''' 工事請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("koj_seikyuu_saki_kbn")> _
    Public Property KojSeikyuuSakiKbn() As String
        Get
            Return strKojSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strKojSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "工事請求締め日"
    ''' <summary>
    ''' 工事請求締め日
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojSeikyuuSimeDate As String
    ''' <summary>
    ''' 工事請求締め日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事請求締め日</returns>
    ''' <remarks></remarks>
    <TableMap("koj_seikyuu_sime_date")> _
    Public Property KojSeikyuuSimeDate() As String
        Get
            Return strKojSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strKojSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "販促品請求先"
    ''' <summary>
    ''' 販促品請求先
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuuSaki As String
    ''' <summary>
    ''' 販促品請求先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuusaki")> _
    Public Property HansokuhinSeikyuuSaki() As String
        Get
            Return strHansokuhinSeikyuuSaki
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuuSaki = value
        End Set
    End Property
#End Region

#Region "販促品請求先コード"
    ''' <summary>
    ''' 販促品請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuuSakiCd As String
    ''' <summary>
    ''' 販促品請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_saki_cd")> _
    Public Property HansokuhinSeikyuuSakiCd() As String
        Get
            Return strHansokuhinSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "販促品請求先枝番"
    ''' <summary>
    ''' 販促品請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuuSakiBrc As String
    ''' <summary>
    ''' 販促品請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_saki_brc")> _
    Public Property HansokuhinSeikyuuSakiBrc() As String
        Get
            Return strHansokuhinSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "販促品請求先区分"
    ''' <summary>
    ''' 販促品請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuuSakiKbn As String
    ''' <summary>
    ''' 販促品請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_saki_kbn")> _
    Public Property HansokuhinSeikyuuSakiKbn() As String
        Get
            Return strHansokuhinSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "販促品請求締め日"
    ''' <summary>
    ''' 販促品請求締め日
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuuSimeDate As String
    ''' <summary>
    ''' 販促品請求締め日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求締め日</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_sime_date")> _
    Public Property HansokuhinSeikyuuSimeDate() As String
        Get
            Return strHansokuhinSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "SS価格"
    ''' <summary>
    ''' SS価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intSsKkk As Integer
    ''' <summary>
    ''' SS価格
    ''' </summary>
    ''' <value></value>
    ''' <returns> SS価格</returns>
    ''' <remarks></remarks>
    <TableMap("ss_kkk")> _
    Public Property SsKkk() As Integer
        Get
            Return intSsKkk
        End Get
        Set(ByVal value As Integer)
            intSsKkk = value
        End Set
    End Property
#End Region

#Region "再調査価格"
    ''' <summary>
    ''' 再調査価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intSaiTysKkk As Integer
    ''' <summary>
    ''' 再調査価格
    ''' </summary>
    ''' <value></value>
    ''' <returns> 再調査価格</returns>
    ''' <remarks></remarks>
    <TableMap("sai_tys_kkk")> _
    Public Property SaiTysKkk() As Integer
        Get
            Return intSaiTysKkk
        End Get
        Set(ByVal value As Integer)
            intSaiTysKkk = value
        End Set
    End Property
#End Region

#Region "SSGR価格"
    ''' <summary>
    ''' SSGR価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intSsgrKkk As Integer
    ''' <summary>
    ''' SSGR価格
    ''' </summary>
    ''' <value></value>
    ''' <returns> SSGR価格</returns>
    ''' <remarks></remarks>
    <TableMap("ssgr_kkk")> _
    Public Property SsgrKkk() As Integer
        Get
            Return intSsgrKkk
        End Get
        Set(ByVal value As Integer)
            intSsgrKkk = value
        End Set
    End Property
#End Region

#Region "解析保証価格"
    ''' <summary>
    ''' 解析保証価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisekiHosyouKkk As Integer
    ''' <summary>
    ''' 解析保証価格
    ''' </summary>
    ''' <value></value>
    ''' <returns> 解析保証価格</returns>
    ''' <remarks></remarks>
    <TableMap("kaiseki_hosyou_kkk")> _
    Public Property KaisekiHosyouKkk() As Integer
        Get
            Return intKaisekiHosyouKkk
        End Get
        Set(ByVal value As Integer)
            intKaisekiHosyouKkk = value
        End Set
    End Property
#End Region

#Region "保証無価格"
    ''' <summary>
    ''' 保証無価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyounasiUmu As Integer
    ''' <summary>
    ''' 保証無価格
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証無価格</returns>
    ''' <remarks></remarks>
    <TableMap("hosyounasi_umu")> _
    Public Property HosyounasiUmu() As Integer
        Get
            Return intHosyounasiUmu
        End Get
        Set(ByVal value As Integer)
            intHosyounasiUmu = value
        End Set
    End Property
#End Region

#Region "解約払戻価格"
    ''' <summary>
    ''' 解約払戻価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaiyakuHaraimodosiKkk As Integer
    ''' <summary>
    ''' 解約払戻価格
    ''' </summary>
    ''' <value></value>
    ''' <returns> 解約払戻価格</returns>
    ''' <remarks></remarks>
    <TableMap("kaiyaku_haraimodosi_kkk")> _
    Public Property KaiyakuHaraimodosiKkk() As Integer
        Get
            Return intKaiyakuHaraimodosiKkk
        End Get
        Set(ByVal value As Integer)
            intKaiyakuHaraimodosiKkk = value
        End Set
    End Property
#End Region

#Region "都道府県コード"
    ''' <summary>
    ''' 都道府県コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTodouhukenCd As String
    ''' <summary>
    ''' 都道府県コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 都道府県コード</returns>
    ''' <remarks></remarks>
    <TableMap("todouhuken_cd")> _
    Public Property TodouhukenCd() As String
        Get
            Return strTodouhukenCd
        End Get
        Set(ByVal value As String)
            strTodouhukenCd = value
        End Set
    End Property
#End Region

#Region "年間棟数"
    ''' <summary>
    ''' 年間棟数
    ''' </summary>
    ''' <remarks></remarks>
    Private strNenkanTousuu As String
    ''' <summary>
    ''' 年間棟数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 年間棟数</returns>
    ''' <remarks></remarks>
    <TableMap("nenkan_tousuu")> _
    Public Property NenkanTousuu() As String
        Get
            Return strNenkanTousuu
        End Get
        Set(ByVal value As String)
            strNenkanTousuu = value
        End Set
    End Property
#End Region

#Region "入金確認条件"
    ''' <summary>
    ''' 入金確認条件
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinKakuninJyouken As Integer
    ''' <summary>
    ''' 入金確認条件
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金確認条件</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_kakunin_jyouken")> _
    Public Property NyuukinKakuninJyouken() As Integer
        Get
            Return intNyuukinKakuninJyouken
        End Get
        Set(ByVal value As Integer)
            intNyuukinKakuninJyouken = value
        End Set
    End Property
#End Region

#Region "入金確認覚書"
    ''' <summary>
    ''' 入金確認覚書
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuukinKakuninOboegaki As DateTime
    ''' <summary>
    ''' 入金確認覚書
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金確認覚書</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_kakunin_oboegaki")> _
    Public Property NyuukinKakuninOboegaki() As DateTime
        Get
            Return dateNyuukinKakuninOboegaki
        End Get
        Set(ByVal value As DateTime)
            dateNyuukinKakuninOboegaki = value
        End Set
    End Property
#End Region

#Region "営業担当者"
    ''' <summary>
    ''' 営業担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyouTantousyaMei As String
    ''' <summary>
    ''' 営業担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業担当者</returns>
    ''' <remarks></remarks>
    <TableMap("eigyou_tantousya_mei")> _
    Public Property EigyouTantousyaMei() As String
        Get
            Return strEigyouTantousyaMei
        End Get
        Set(ByVal value As String)
            strEigyouTantousyaMei = value
        End Set
    End Property
#End Region

#Region "見積書ファイル名"
    ''' <summary>
    ''' 見積書ファイル名
    ''' </summary>
    ''' <remarks></remarks>
    Private strMitsyoFileMei As String
    ''' <summary>
    ''' 見積書ファイル名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 見積書ファイル名</returns>
    ''' <remarks></remarks>
    <TableMap("mitsyo_file_mei")> _
    Public Property MitsyoFileMei() As String
        Get
            Return strMitsyoFileMei
        End Get
        Set(ByVal value As String)
            strMitsyoFileMei = value
        End Set
    End Property
#End Region

#Region "事前調査価格"
    ''' <summary>
    ''' 事前調査価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intJizenTysKkk As Integer
    ''' <summary>
    ''' 事前調査価格
    ''' </summary>
    ''' <value></value>
    ''' <returns> 事前調査価格</returns>
    ''' <remarks></remarks>
    <TableMap("jizen_tys_kkk")> _
    Public Property JizenTysKkk() As Integer
        Get
            Return intJizenTysKkk
        End Get
        Set(ByVal value As Integer)
            intJizenTysKkk = value
        End Set
    End Property
#End Region

#Region "地震補償FLG"
    ''' <summary>
    ''' 地震補償FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intJisinHosyouFlg As Integer
    ''' <summary>
    ''' 地震補償FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 地震補償FLG</returns>
    ''' <remarks></remarks>
    <TableMap("jisin_hosyou_flg")> _
    Public Property JisinHosyouFlg() As Integer
        Get
            Return intJisinHosyouFlg
        End Get
        Set(ByVal value As Integer)
            intJisinHosyouFlg = value
        End Set
    End Property
#End Region

#Region "地震補償登録日"
    ''' <summary>
    ''' 地震補償登録日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateJisinHosyouAddDate As DateTime
    ''' <summary>
    ''' 地震補償登録日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 地震補償登録日</returns>
    ''' <remarks></remarks>
    <TableMap("jisin_hosyou_add_date")> _
    Public Property JisinHosyouAddDate() As DateTime
        Get
            Return dateJisinHosyouAddDate
        End Get
        Set(ByVal value As DateTime)
            dateJisinHosyouAddDate = value
        End Set
    End Property
#End Region

#Region "引継ぎ完了日"
    ''' <summary>
    ''' 引継ぎ完了日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHikitugiKanryouDate As DateTime
    ''' <summary>
    ''' 引継ぎ完了日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引継ぎ完了日</returns>
    ''' <remarks></remarks>
    <TableMap("hikitugi_kanryou_date")> _
    Public Property HikitugiKanryouDate() As DateTime
        Get
            Return dateHikitugiKanryouDate
        End Get
        Set(ByVal value As DateTime)
            dateHikitugiKanryouDate = value
        End Set
    End Property
#End Region

#Region "旧営業担当者"
    ''' <summary>
    ''' 旧営業担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strKyuuEigyouTantousyaMei As String
    ''' <summary>
    ''' 旧営業担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 旧営業担当者</returns>
    ''' <remarks></remarks>
    <TableMap("kyuu_eigyou_tantousya_mei")> _
    Public Property KyuuEigyouTantousyaMei() As String
        Get
            Return strKyuuEigyouTantousyaMei
        End Get
        Set(ByVal value As String)
            strKyuuEigyouTantousyaMei = value
        End Set
    End Property
#End Region

#Region "工事売上種別"
    ''' <summary>
    ''' 工事売上種別
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojUriSyubetsu As Integer
    ''' <summary>
    ''' 工事売上種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事売上種別</returns>
    ''' <remarks></remarks>
    <TableMap("koj_uri_syubetsu")> _
    Public Property KojUriSyubetsu() As Integer
        Get
            Return intKojUriSyubetsu
        End Get
        Set(ByVal value As Integer)
            intKojUriSyubetsu = value
        End Set
    End Property
#End Region

#Region "工事サポートシステム"
    ''' <summary>
    ''' 工事サポートシステム
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojSupportSystem As Integer
    ''' <summary>
    ''' 工事サポートシステム
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事サポートシステム</returns>
    ''' <remarks></remarks>
    <TableMap("koj_support_system")> _
    Public Property KojSupportSystem() As Integer
        Get
            Return intKojSupportSystem
        End Get
        Set(ByVal value As Integer)
            intKojSupportSystem = value
        End Set
    End Property
#End Region

#Region "都道府県名"
    ''' <summary>
    ''' 都道府県名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTodouhukenMei As String
    ''' <summary>
    ''' 都道府県名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 都道府県名</returns>
    ''' <remarks></remarks>
    <TableMap("todouhuken_mei")> _
    Public Property TodouhukenMei() As String
        Get
            Return strTodouhukenMei
        End Get
        Set(ByVal value As String)
            strTodouhukenMei = value
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
    ''' <returns> 保証書発行有無</returns>
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

#Region "保証期間"
    ''' <summary>
    ''' 保証期間
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikan As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証期間
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証期間</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kikan")> _
    Public Property HosyouKikan() As Integer
        Get
            Return intHosyouKikan
        End Get
        Set(ByVal value As Integer)
            intHosyouKikan = value
        End Set
    End Property
#End Region

#Region "工事会社請求有無"
    ''' <summary>
    ''' 工事会社請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojGaisyaSeikyuuUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 工事会社請求有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社請求有無</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_seikyuu_umu")> _
    Public Property KojGaisyaSeikyuuUmu() As Integer
        Get
            Return intKojGaisyaSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intKojGaisyaSeikyuuUmu = value
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
    ''' <returns> 付保証明書FLG</returns>
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

#Region "付保証明開始年月"
    ''' <summary>
    ''' 付保証明開始年月
    ''' </summary>
    ''' <remarks></remarks>
    Private dateFuhoSyoumeiKaisiNengetu As DateTime
    ''' <summary>
    ''' 付保証明開始年月
    ''' </summary>
    ''' <value></value>
    ''' <returns>付保証明開始年月</returns>
    ''' <remarks></remarks>
    <TableMap("fuho_syoumeisyo_kaisi_nengetu")> _
    Public Property FuhoSyoumeiKaisiNengetu() As DateTime
        Get
            Return dateFuhoSyoumeiKaisiNengetu
        End Get
        Set(ByVal value As DateTime)
            dateFuhoSyoumeiKaisiNengetu = value
        End Set
    End Property
#End Region

#Region "住所"
    ''' <summary>
    ''' 住所
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyo As String
    ''' <summary>
    ''' 住所
    ''' </summary>
    ''' <value></value>
    ''' <returns>住所</returns>
    ''' <remarks></remarks>
    <TableMap("jyuusyo")> _
    Public Property Jyuusyo() As String
        Get
            Return strJyuusyo
        End Get
        Set(ByVal value As String)
            strJyuusyo = value
        End Set
    End Property
#End Region

#Region "JIO先FLG"
    ''' <summary>
    ''' JIO先FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intJioSakiFLG As Integer = Integer.MinValue
    ''' <summary>
    ''' JIO先FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>JIO先FLG</returns>
    ''' <remarks></remarks>
    <TableMap("jiosaki_flg")> _
    Public Property JioSakiFLG() As Integer
        Get
            Return intJioSakiFLG
        End Get
        Set(ByVal value As Integer)
            intJioSakiFLG = value
        End Set
    End Property
#End Region

#Region "発注停止フラグ"
    ''' <summary>
    ''' 発注停止フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private strOrderStopFLG As String = String.Empty
    ''' <summary>
    ''' 発注停止フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns>発注停止フラグG</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuu_teisi_flg")> _
    Public Property OrderStopFLG() As String
        Get
            Return strOrderStopFLG
        End Get
        Set(ByVal value As String)
            strOrderStopFLG = value
        End Set
    End Property
#End Region

#Region "SDS自動設定情報"
    ''' <summary>
    ''' SDS自動設定情報
    ''' </summary>
    ''' <remarks></remarks>
    Private intSdsJidoouSetInfo As Integer = Integer.MinValue
    ''' <summary>
    ''' SDS自動設定情報
    ''' </summary>
    ''' <value></value>
    ''' <returns>SDS自動設定情報</returns>
    ''' <remarks></remarks>
    <TableMap("sds_jidoou_set_info")> _
    Public Property SdsJidoouSetInfo() As Integer
        Get
            Return intSdsJidoouSetInfo
        End Get
        Set(ByVal value As Integer)
            intSdsJidoouSetInfo = value
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
    <TableMap("add_login_user_id")> _
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
    <TableMap("add_datetime")> _
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
    <TableMap("upd_login_user_id")> _
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

#Region "FC店情報"
#Region "FC店名"
    ''' <summary>
    ''' FC店名(加盟店コード->営業所コード->FC調査会社コード(事業所コード)->調査会社名)
    ''' </summary>
    ''' <remarks></remarks>
    Private strFcTenMei As String
    ''' <summary>
    ''' FC店名
    ''' </summary>
    ''' <value></value>
    ''' <returns>FC店名</returns>
    ''' <remarks></remarks>
    <TableMap("fc_ten_mei")> _
    Public Property FcTenMei() As String
        Get
            Return strFcTenMei
        End Get
        Set(ByVal value As String)
            strFcTenMei = value
        End Set
    End Property
#End Region
#End Region
End Class
