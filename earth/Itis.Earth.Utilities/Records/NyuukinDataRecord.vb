''' <summary>
''' 入金データテーブルのレコードクラスです
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_nyuukin_data")> _
Public Class NyuukinDataRecord

#Region "伝票ユニークNO"
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenpyouUniqueNo As Integer
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no")> _
    Public Property DenpyouUniqueNo() As Integer
        Get
            Return intDenpyouUniqueNo
        End Get
        Set(ByVal value As Integer)
            intDenpyouUniqueNo = value
        End Set
    End Property
#End Region

#Region "伝票NO"
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenpyouNo As String
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票NO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_no")> _
    Public Property DenpyouNo() As String
        Get
            Return strDenpyouNo
        End Get
        Set(ByVal value As String)
            strDenpyouNo = value
        End Set
    End Property
#End Region

#Region "伝票種別"
    ''' <summary>
    ''' 伝票種別
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenpyouSyubetu As String
    ''' <summary>
    ''' 伝票種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票種別</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_syubetu")> _
    Public Property DenpyouSyubetu() As String
        Get
            Return strDenpyouSyubetu
        End Get
        Set(ByVal value As String)
            strDenpyouSyubetu = value
        End Set
    End Property
#End Region

#Region "取消元伝票ユニークNO"
    ''' <summary>
    ''' 取消元伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesiMotoDenpyouUniqueNo As Integer
    ''' <summary>
    ''' 取消元伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消元伝票ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi_moto_denpyou_unique_no")> _
    Public Property TorikesiMotoDenpyouUniqueNo() As Integer
        Get
            Return intTorikesiMotoDenpyouUniqueNo
        End Get
        Set(ByVal value As Integer)
            intTorikesiMotoDenpyouUniqueNo = value
        End Set
    End Property
#End Region

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

#Region "番号"
    ''' <summary>
    ''' 番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strBangou As String
    ''' <summary>
    ''' 番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 番号</returns>
    ''' <remarks></remarks>
    <TableMap("bangou")> _
    Public Property Bangou() As String
        Get
            Return strBangou
        End Get
        Set(ByVal value As String)
            strBangou = value
        End Set
    End Property
#End Region

#Region "入金取込ユニークNO"
    ''' <summary>
    ''' 入金取込ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinTorikomiUniqueNo As Integer
    ''' <summary>
    ''' 入金取込ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金取込ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_torikomi_unique_no")> _
    Public Property NyuukinTorikomiUniqueNo() As Integer
        Get
            Return intNyuukinTorikomiUniqueNo
        End Get
        Set(ByVal value As Integer)
            intNyuukinTorikomiUniqueNo = value
        End Set
    End Property
#End Region

#Region "紐付けコード"
    ''' <summary>
    ''' 紐付けコード
    ''' </summary>
    ''' <remarks></remarks>
    Private strHimodukeCd As String
    ''' <summary>
    ''' 紐付けコード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 紐付けコード</returns>
    ''' <remarks></remarks>
    <TableMap("himoduke_cd")> _
    Public Property HimodukeCd() As String
        Get
            Return strHimodukeCd
        End Get
        Set(ByVal value As String)
            strHimodukeCd = value
        End Set
    End Property
#End Region

#Region "紐付け元テーブル種別"
    ''' <summary>
    ''' 紐付け元テーブル種別
    ''' </summary>
    ''' <remarks></remarks>
    Private intHimodukeTableType As Integer
    ''' <summary>
    ''' 紐付け元テーブル種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 紐付け元テーブル種別</returns>
    ''' <remarks></remarks>
    <TableMap("himoduke_table_type")> _
    Public Property HimodukeTableType() As Integer
        Get
            Return intHimodukeTableType
        End Get
        Set(ByVal value As Integer)
            intHimodukeTableType = value
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

#Region "照合口座No"
    ''' <summary>
    ''' 照合口座No
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyougouKouzaNo As String
    ''' <summary>
    ''' 照合口座No
    ''' </summary>
    ''' <value></value>
    ''' <returns> 照合口座No</returns>
    ''' <remarks></remarks>
    <TableMap("syougou_kouza_no")> _
    Public Property SyougouKouzaNo() As String
        Get
            Return strSyougouKouzaNo
        End Get
        Set(ByVal value As String)
            strSyougouKouzaNo = value
        End Set
    End Property
#End Region

#Region "入金日"
    ''' <summary>
    ''' 入金日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuukinDate As DateTime
    ''' <summary>
    ''' 入金日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金日</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_date")> _
    Public Property NyuukinDate() As DateTime
        Get
            Return dateNyuukinDate
        End Get
        Set(ByVal value As DateTime)
            dateNyuukinDate = value
        End Set
    End Property
#End Region

#Region "入金額 [現金]"
    ''' <summary>
    ''' 入金額 [現金]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngGenkin As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [現金]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [現金]</returns>
    ''' <remarks></remarks>
    <TableMap("genkin")> _
    Public Property Genkin() As Long
        Get
            Return lngGenkin
        End Get
        Set(ByVal value As Long)
            lngGenkin = value
        End Set
    End Property
#End Region

#Region "入金額 [小切手]"
    ''' <summary>
    ''' 入金額 [小切手]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKogitte As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [小切手]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [小切手]</returns>
    ''' <remarks></remarks>
    <TableMap("kogitte")> _
    Public Property Kogitte() As Long
        Get
            Return lngKogitte
        End Get
        Set(ByVal value As Long)
            lngKogitte = value
        End Set
    End Property
#End Region

#Region "入金額 [振込]"
    ''' <summary>
    ''' 入金額 [振込]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomi As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [振込]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [振込]</returns>
    ''' <remarks></remarks>
    <TableMap("furikomi")> _
    Public Property Furikomi() As Long
        Get
            Return lngFurikomi
        End Get
        Set(ByVal value As Long)
            lngFurikomi = value
        End Set
    End Property
#End Region

#Region "入金額 [手形]"
    ''' <summary>
    ''' 入金額 [手形]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTegata As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [手形]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [手形]</returns>
    ''' <remarks></remarks>
    <TableMap("tegata")> _
    Public Property Tegata() As Long
        Get
            Return lngTegata
        End Get
        Set(ByVal value As Long)
            lngTegata = value
        End Set
    End Property
#End Region

#Region "入金額 [相殺]"
    ''' <summary>
    ''' 入金額 [相殺]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSousai As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [相殺]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [相殺]</returns>
    ''' <remarks></remarks>
    <TableMap("sousai")> _
    Public Property Sousai() As Long
        Get
            Return lngSousai
        End Get
        Set(ByVal value As Long)
            lngSousai = value
        End Set
    End Property
#End Region

#Region "入金額 [値引]"
    ''' <summary>
    ''' 入金額 [値引]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngNebiki As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [値引]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [値引]</returns>
    ''' <remarks></remarks>
    <TableMap("nebiki")> _
    Public Property Nebiki() As Long
        Get
            Return lngNebiki
        End Get
        Set(ByVal value As Long)
            lngNebiki = value
        End Set
    End Property
#End Region

#Region "入金額 [その他]"
    ''' <summary>
    ''' 入金額 [その他]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSonota As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [その他]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [その他]</returns>
    ''' <remarks></remarks>
    <TableMap("sonota")> _
    Public Property Sonota() As Long
        Get
            Return lngSonota
        End Get
        Set(ByVal value As Long)
            lngSonota = value
        End Set
    End Property
#End Region

#Region "入金額 [協力会費]"
    ''' <summary>
    ''' 入金額 [協力会費]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKyouryokuKaihi As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [協力会費]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [協力会費]</returns>
    ''' <remarks></remarks>
    <TableMap("kyouryoku_kaihi")> _
    Public Property KyouryokuKaihi() As Long
        Get
            Return lngKyouryokuKaihi
        End Get
        Set(ByVal value As Long)
            lngKyouryokuKaihi = value
        End Set
    End Property
#End Region

#Region "入金額 [口座振替]"
    ''' <summary>
    ''' 入金額 [口座振替]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKouzaFurikae As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [口座振替]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [口座振替]</returns>
    ''' <remarks></remarks>
    <TableMap("kouza_furikae")> _
    Public Property KouzaFurikae() As Long
        Get
            Return lngKouzaFurikae
        End Get
        Set(ByVal value As Long)
            lngKouzaFurikae = value
        End Set
    End Property
#End Region

#Region "入金額 [振込手数料]"
    ''' <summary>
    ''' 入金額 [振込手数料]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomiTesuuryou As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [振込手数料]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [振込手数料]</returns>
    ''' <remarks></remarks>
    <TableMap("furikomi_tesuuryou")> _
    Public Property FurikomiTesuuryou() As Long
        Get
            Return lngFurikomiTesuuryou
        End Get
        Set(ByVal value As Long)
            lngFurikomiTesuuryou = value
        End Set
    End Property
#End Region

#Region "伝票合計金額[サマリー]"
    ''' <summary>
    ''' 伝票合計金額[サマリー]
    ''' </summary>
    ''' <value></value>
    ''' <returns>伝票合計金額[サマリー]</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DenpyouGoukeiGaku() As Long
        Get
            Dim tmpGoukei As Long = 0

            Dim tmpGenkin As Long = IIf(lngGenkin = Long.MinValue, 0, lngGenkin)
            Dim tmpKogitte As Long = IIf(lngKogitte = Long.MinValue, 0, lngKogitte)
            Dim tmpFurikomi As Long = IIf(lngFurikomi = Long.MinValue, 0, lngFurikomi)
            Dim tmpTegata As Long = IIf(lngTegata = Long.MinValue, 0, lngTegata)
            Dim tmpSousai As Long = IIf(lngSousai = Long.MinValue, 0, lngSousai)
            Dim tmpNebiki As Long = IIf(lngNebiki = Long.MinValue, 0, lngNebiki)
            Dim tmpSonota As Long = IIf(lngSonota = Long.MinValue, 0, lngSonota)
            Dim tmpKyouryokuKaihi As Long = IIf(lngKyouryokuKaihi = Long.MinValue, 0, lngKyouryokuKaihi)
            Dim tmpKouzaFurikae As Long = IIf(lngKouzaFurikae = Long.MinValue, 0, lngKouzaFurikae)
            Dim tmpFurikomiTesuuryou As Long = IIf(lngFurikomiTesuuryou = Long.MinValue, 0, lngFurikomiTesuuryou)

            tmpGoukei = tmpGenkin + tmpKogitte + tmpFurikomi + tmpTegata + tmpSousai _
                            + tmpNebiki + tmpSonota + tmpKyouryokuKaihi + tmpKouzaFurikae + tmpFurikomiTesuuryou

            Return tmpGoukei
        End Get
    End Property
#End Region

#Region "手形期日"
    ''' <summary>
    ''' 手形期日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTegataKijitu As DateTime
    ''' <summary>
    ''' 手形期日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 手形期日</returns>
    ''' <remarks></remarks>
    <TableMap("tegata_kijitu")> _
    Public Property TegataKijitu() As DateTime
        Get
            Return dateTegataKijitu
        End Get
        Set(ByVal value As DateTime)
            dateTegataKijitu = value
        End Set
    End Property
#End Region

#Region "手形No."
    ''' <summary>
    ''' 手形No.
    ''' </summary>
    ''' <remarks></remarks>
    Private strTegataNo As String
    ''' <summary>
    ''' 手形No.
    ''' </summary>
    ''' <value></value>
    ''' <returns> 手形No.</returns>
    ''' <remarks></remarks>
    <TableMap("tegata_no")> _
    Public Property TegataNo() As String
        Get
            Return strTegataNo
        End Get
        Set(ByVal value As String)
            strTegataNo = value
        End Set
    End Property
#End Region

#Region "摘要名"
    ''' <summary>
    ''' 摘要名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTekiyouMei As String
    ''' <summary>
    ''' 摘要名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 摘要名</returns>
    ''' <remarks></remarks>
    <TableMap("tekiyou_mei")> _
    Public Property TekiyouMei() As String
        Get
            Return strTekiyouMei
        End Get
        Set(ByVal value As String)
            strTekiyouMei = value
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

#Region "登録ログインユーザー名"
    ''' <summary>
    ''' 登録ログインユーザー名
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserName As String
    ''' <summary>
    ''' 登録ログインユーザー名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ログインユーザー名</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_name")> _
    Public Property AddLoginUserName() As String
        Get
            Return strAddLoginUserName
        End Get
        Set(ByVal value As String)
            strAddLoginUserName = value
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

End Class