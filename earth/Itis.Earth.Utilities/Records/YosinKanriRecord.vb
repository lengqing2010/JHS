''' <summary>
''' 与信管理レコードクラス
''' </summary>
''' <remarks></remarks>
Public Class YosinKanriRecord

#Region "名寄先コード"
    ''' <summary>
    ''' 名寄先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strNayoseSakiCd As String
    ''' <summary>
    ''' 名寄先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 名寄先コード</returns>
    ''' <remarks></remarks>
    <TableMap("nayose_saki_cd")> _
    Public Property NayoseSakiCd() As String
        Get
            Return strNayoseSakiCd
        End Get
        Set(ByVal value As String)
            strNayoseSakiCd = value
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

#Region "名寄先名１"
    ''' <summary>
    ''' 名寄先名１
    ''' </summary>
    ''' <remarks></remarks>
    Private strNayoseSakiName1 As String
    ''' <summary>
    ''' 名寄先名１
    ''' </summary>
    ''' <value></value>
    ''' <returns> 名寄先名１</returns>
    ''' <remarks></remarks>
    <TableMap("nayose_saki_name1")> _
    Public Property NayoseSakiName1() As String
        Get
            Return strNayoseSakiName1
        End Get
        Set(ByVal value As String)
            strNayoseSakiName1 = value
        End Set
    End Property
#End Region

#Region "名寄先名２"
    ''' <summary>
    ''' 名寄先名２
    ''' </summary>
    ''' <remarks></remarks>
    Private strNayoseSakiName2 As String
    ''' <summary>
    ''' 名寄先名２
    ''' </summary>
    ''' <value></value>
    ''' <returns> 名寄先名２</returns>
    ''' <remarks></remarks>
    <TableMap("nayose_saki_name2")> _
    Public Property NayoseSakiName2() As String
        Get
            Return strNayoseSakiName2
        End Get
        Set(ByVal value As String)
            strNayoseSakiName2 = value
        End Set
    End Property
#End Region

#Region "名寄先カナ１"
    ''' <summary>
    ''' 名寄先カナ１
    ''' </summary>
    ''' <remarks></remarks>
    Private strNayoseSakiKana1 As String
    ''' <summary>
    ''' 名寄先カナ１
    ''' </summary>
    ''' <value></value>
    ''' <returns> 名寄先カナ１</returns>
    ''' <remarks></remarks>
    <TableMap("nayose_saki_kana1")> _
    Public Property NayoseSakiKana1() As String
        Get
            Return strNayoseSakiKana1
        End Get
        Set(ByVal value As String)
            strNayoseSakiKana1 = value
        End Set
    End Property
#End Region

#Region "名寄先カナ２"
    ''' <summary>
    ''' 名寄先カナ２
    ''' </summary>
    ''' <remarks></remarks>
    Private strNayoseSakiKana2 As String
    ''' <summary>
    ''' 名寄先カナ２
    ''' </summary>
    ''' <value></value>
    ''' <returns> 名寄先カナ２</returns>
    ''' <remarks></remarks>
    <TableMap("nayose_saki_kana2")> _
    Public Property NayoseSakiKana2() As String
        Get
            Return strNayoseSakiKana2
        End Get
        Set(ByVal value As String)
            strNayoseSakiKana2 = value
        End Set
    End Property
#End Region

#Region "名寄先与信限度額"
    ''' <summary>
    ''' 名寄先与信限度額
    ''' </summary>
    ''' <remarks></remarks>
    Private intYosinGendoGaku As Integer
    ''' <summary>
    ''' 名寄先与信限度額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 名寄先与信限度額</returns>
    ''' <remarks></remarks>
    <TableMap("yosin_gendo_gaku")> _
    Public Property YosinGendoGaku() As Integer
        Get
            Return intYosinGendoGaku
        End Get
        Set(ByVal value As Integer)
            intYosinGendoGaku = value
        End Set
    End Property
#End Region

#Region "与信警告開始率"
    ''' <summary>
    ''' 与信警告開始率
    ''' </summary>
    ''' <remarks></remarks>
    Private deciYosinKeikouKaisiritsu As Decimal
    ''' <summary>
    ''' 与信警告開始率
    ''' </summary>
    ''' <value></value>
    ''' <returns> 与信警告開始率</returns>
    ''' <remarks></remarks>
    <TableMap("yosin_keikou_kaisiritsu")> _
    Public Property YosinKeikouKaisiritsu() As Decimal
        Get
            Return deciYosinKeikouKaisiritsu
        End Get
        Set(ByVal value As Decimal)
            deciYosinKeikouKaisiritsu = value
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

#Region "警告状況"
    ''' <summary>
    ''' 警告状況
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeikokuJoukyou As Integer = Integer.MinValue
    ''' <summary>
    ''' 警告状況
    ''' </summary>
    ''' <value></value>
    ''' <returns> 警告状況</returns>
    ''' <remarks></remarks>
    <TableMap("keikoku_joukyou")> _
    Public Property KeikokuJoukyou() As Integer
        Get
            Return intKeikokuJoukyou
        End Get
        Set(ByVal value As Integer)
            intKeikokuJoukyou = value
        End Set
    End Property
#End Region

#Region "送信日時"
    ''' <summary>
    ''' 送信日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSoushinDate As DateTime
    ''' <summary>
    ''' 送信日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 送信日時</returns>
    ''' <remarks></remarks>
    <TableMap("soushin_date")> _
    Public Property SoushinDate() As DateTime
        Get
            Return dateSoushinDate
        End Get
        Set(ByVal value As DateTime)
            dateSoushinDate = value
        End Set
    End Property
#End Region

#Region "直工事FLG"
    ''' <summary>
    ''' 直工事FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyokuKojiFlg As Integer
    ''' <summary>
    ''' 直工事FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 直工事FLG</returns>
    ''' <remarks></remarks>
    <TableMap("tyoku_koji_flg")> _
    Public Property TyokuKojiFlg() As Integer
        Get
            Return intTyokuKojiFlg
        End Get
        Set(ByVal value As Integer)
            intTyokuKojiFlg = value
        End Set
    End Property
#End Region

#Region "前日工事状況FLG"
    ''' <summary>
    ''' 前日工事状況FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intZenjitsuKojiFlg As Integer
    ''' <summary>
    ''' 前日工事状況FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 前日工事状況FLG</returns>
    ''' <remarks></remarks>
    <TableMap("zenjitsu_koji_flg")> _
    Public Property ZenjitsuKojiFlg() As Integer
        Get
            Return intZenjitsuKojiFlg
        End Get
        Set(ByVal value As Integer)
            intZenjitsuKojiFlg = value
        End Set
    End Property
#End Region

#Region "受注管理FLG"
    ''' <summary>
    ''' 受注管理FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intJyutyuuKanriFlg As Integer
    ''' <summary>
    ''' 受注管理FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 受注管理FLG</returns>
    ''' <remarks></remarks>
    <TableMap("jyutyuu_kanri_flg")> _
    Public Property JyutyuuKanriFlg() As Integer
        Get
            Return intJyutyuuKanriFlg
        End Get
        Set(ByVal value As Integer)
            intJyutyuuKanriFlg = value
        End Set
    End Property
#End Region

#Region "帝国評点"
    ''' <summary>
    ''' 帝国評点
    ''' </summary>
    ''' <remarks></remarks>
    Private intTeikokuHyouten As Integer
    ''' <summary>
    ''' 帝国評点
    ''' </summary>
    ''' <value></value>
    ''' <returns> 帝国評点</returns>
    ''' <remarks></remarks>
    <TableMap("teikoku_hyouten")> _
    Public Property TeikokuHyouten() As Integer
        Get
            Return intTeikokuHyouten
        End Get
        Set(ByVal value As Integer)
            intTeikokuHyouten = value
        End Set
    End Property
#End Region

#Region "前月債権額"
    ''' <summary>
    ''' 前月債権額
    ''' </summary>
    ''' <remarks></remarks>
    Private intZengetsuSaikenGaku As Integer
    ''' <summary>
    ''' 前月債権額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 前月債権額</returns>
    ''' <remarks></remarks>
    <TableMap("zengetsu_saiken_gaku")> _
    Public Property ZengetsuSaikenGaku() As Integer
        Get
            Return intZengetsuSaikenGaku
        End Get
        Set(ByVal value As Integer)
            intZengetsuSaikenGaku = value
        End Set
    End Property
#End Region

#Region "前月債権額設定年月"
    ''' <summary>
    ''' 前月債権額設定年月
    ''' </summary>
    ''' <remarks></remarks>
    Private dateZengetsuSaikenSetDate As DateTime
    ''' <summary>
    ''' 前月債権額設定年月
    ''' </summary>
    ''' <value></value>
    ''' <returns> 前月債権額設定年月</returns>
    ''' <remarks></remarks>
    <TableMap("zengetsu_saiken_set_date")> _
    Public Property ZengetsuSaikenSetDate() As DateTime
        Get
            Return dateZengetsuSaikenSetDate
        End Get
        Set(ByVal value As DateTime)
            dateZengetsuSaikenSetDate = value
        End Set
    End Property
#End Region

#Region "累積入金額"
    ''' <summary>
    ''' 累積入金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intRuisekiNyuukinGaku As Integer
    ''' <summary>
    ''' 累積入金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 累積入金額</returns>
    ''' <remarks></remarks>
    <TableMap("ruiseki_nyuukin_gaku")> _
    Public Property RuisekiNyuukinGaku() As Integer
        Get
            Return intRuisekiNyuukinGaku
        End Get
        Set(ByVal value As Integer)
            intRuisekiNyuukinGaku = value
        End Set
    End Property
#End Region

#Region "累積入金額設定日時"
    ''' <summary>
    ''' 累積入金額設定日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateRuisekiNyuukinSetDate As DateTime
    ''' <summary>
    ''' 累積入金額設定日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 累積入金額設定日時</returns>
    ''' <remarks></remarks>
    <TableMap("ruiseki_nyuukin_set_date")> _
    Public Property RuisekiNyuukinSetDate() As DateTime
        Get
            Return dateRuisekiNyuukinSetDate
        End Get
        Set(ByVal value As DateTime)
            dateRuisekiNyuukinSetDate = value
        End Set
    End Property
#End Region

#Region "累積瑕疵売上額"
    ''' <summary>
    ''' 累積瑕疵売上額
    ''' </summary>
    ''' <remarks></remarks>
    Private intRuisekiKasiuriGaku As Integer
    ''' <summary>
    ''' 累積瑕疵売上額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 累積瑕疵売上額</returns>
    ''' <remarks></remarks>
    <TableMap("ruiseki_kasiuri_gaku")> _
    Public Property RuisekiKasiuriGaku() As Integer
        Get
            Return intRuisekiKasiuriGaku
        End Get
        Set(ByVal value As Integer)
            intRuisekiKasiuriGaku = value
        End Set
    End Property
#End Region

#Region "累積瑕疵売上額設定日"
    ''' <summary>
    ''' 累積瑕疵売上額設定日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateRuisekiKasiuriSetDate As DateTime
    ''' <summary>
    ''' 累積瑕疵売上額設定日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 累積瑕疵売上額設定日</returns>
    ''' <remarks></remarks>
    <TableMap("ruiseki_kasiuri_set_date")> _
    Public Property RuisekiKasiuriSetDate() As DateTime
        Get
            Return dateRuisekiKasiuriSetDate
        End Get
        Set(ByVal value As DateTime)
            dateRuisekiKasiuriSetDate = value
        End Set
    End Property
#End Region

#Region "累積受注額"
    ''' <summary>
    ''' 累積受注額
    ''' </summary>
    ''' <remarks></remarks>
    Private intRuisekiJyutyuuGaku As Integer
    ''' <summary>
    ''' 累積受注額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 累積受注額</returns>
    ''' <remarks></remarks>
    <TableMap("ruiseki_jyutyuu_gaku")> _
    Public Property RuisekiJyutyuuGaku() As Integer
        Get
            Return intRuisekiJyutyuuGaku
        End Get
        Set(ByVal value As Integer)
            intRuisekiJyutyuuGaku = value
        End Set
    End Property
#End Region

#Region "累積受注額設定年月日"
    ''' <summary>
    ''' 累積受注額設定年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateRuisekiJyutyuuSetDate As DateTime
    ''' <summary>
    ''' 累積受注額設定年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 累積受注額設定年月日</returns>
    ''' <remarks></remarks>
    <TableMap("ruiseki_jyutyuu_set_date")> _
    Public Property RuisekiJyutyuuSetDate() As DateTime
        Get
            Return dateRuisekiJyutyuuSetDate
        End Get
        Set(ByVal value As DateTime)
            dateRuisekiJyutyuuSetDate = value
        End Set
    End Property
#End Region

#Region "当日受注額"
    ''' <summary>
    ''' 当日受注額
    ''' </summary>
    ''' <remarks></remarks>
    Private intToujitsuJyutyuuGaku As Integer
    ''' <summary>
    ''' 当日受注額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 当日受注額</returns>
    ''' <remarks></remarks>
    <TableMap("toujitsu_jyutyuu_gaku")> _
    Public Property ToujitsuJyutyuuGaku() As Integer
        Get
            Return intToujitsuJyutyuuGaku
        End Get
        Set(ByVal value As Integer)
            intToujitsuJyutyuuGaku = value
        End Set
    End Property
#End Region

#Region "担当部署コード01"
    ''' <summary>
    ''' 担当部署コード01
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantouBusyoCd01 As String
    ''' <summary>
    ''' 担当部署コード01
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当部署コード01</returns>
    ''' <remarks></remarks>
    <TableMap("tantou_busyo_cd01")> _
    Public Property TantouBusyoCd01() As String
        Get
            Return strTantouBusyoCd01
        End Get
        Set(ByVal value As String)
            strTantouBusyoCd01 = value
        End Set
    End Property
#End Region

#Region "担当部署コード02"
    ''' <summary>
    ''' 担当部署コード02
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantouBusyoCd02 As String
    ''' <summary>
    ''' 担当部署コード02
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当部署コード02</returns>
    ''' <remarks></remarks>
    <TableMap("tantou_busyo_cd02")> _
    Public Property TantouBusyoCd02() As String
        Get
            Return strTantouBusyoCd02
        End Get
        Set(ByVal value As String)
            strTantouBusyoCd02 = value
        End Set
    End Property
#End Region

#Region "担当部署コード03"
    ''' <summary>
    ''' 担当部署コード03
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantouBusyoCd03 As String
    ''' <summary>
    ''' 担当部署コード03
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当部署コード03</returns>
    ''' <remarks></remarks>
    <TableMap("tantou_busyo_cd03")> _
    Public Property TantouBusyoCd03() As String
        Get
            Return strTantouBusyoCd03
        End Get
        Set(ByVal value As String)
            strTantouBusyoCd03 = value
        End Set
    End Property
#End Region

#Region "担当部署コード04"
    ''' <summary>
    ''' 担当部署コード04
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantouBusyoCd04 As String
    ''' <summary>
    ''' 担当部署コード04
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当部署コード04</returns>
    ''' <remarks></remarks>
    <TableMap("tantou_busyo_cd04")> _
    Public Property TantouBusyoCd04() As String
        Get
            Return strTantouBusyoCd04
        End Get
        Set(ByVal value As String)
            strTantouBusyoCd04 = value
        End Set
    End Property
#End Region

#Region "担当部署コード05"
    ''' <summary>
    ''' 担当部署コード05
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantouBusyoCd05 As String
    ''' <summary>
    ''' 担当部署コード05
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当部署コード05</returns>
    ''' <remarks></remarks>
    <TableMap("tantou_busyo_cd05")> _
    Public Property TantouBusyoCd05() As String
        Get
            Return strTantouBusyoCd05
        End Get
        Set(ByVal value As String)
            strTantouBusyoCd05 = value
        End Set
    End Property
#End Region

#Region "担当部署コード06"
    ''' <summary>
    ''' 担当部署コード06
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantouBusyoCd06 As String
    ''' <summary>
    ''' 担当部署コード06
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当部署コード06</returns>
    ''' <remarks></remarks>
    <TableMap("tantou_busyo_cd06")> _
    Public Property TantouBusyoCd06() As String
        Get
            Return strTantouBusyoCd06
        End Get
        Set(ByVal value As String)
            strTantouBusyoCd06 = value
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

End Class