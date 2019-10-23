''' <summary>
''' 地盤一覧情報で使用するKey項目を設定するクラスです<br/>
''' 検索条件として必要な情報のみ設定して下さい
''' </summary>
''' <remarks></remarks>
Public Class JibanKeyRecord

#Region "区分_1"
    ''' <summary>
    ''' 区分_1 kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn1 As String
    ''' <summary>
    ''' 区分_1 
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分_1</returns>
    ''' <remarks></remarks>
    Public Property Kbn1() As String
        Get
            Return strKbn1
        End Get
        Set(ByVal value As String)
            strKbn1 = value
        End Set
    End Property
#End Region
#Region "区分_2"
    ''' <summary>
    ''' 区分_2 kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn2 As String
    ''' <summary>
    ''' 区分_2
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分_2</returns>
    ''' <remarks></remarks>
    Public Property Kbn2() As String
        Get
            Return strKbn2
        End Get
        Set(ByVal value As String)
            strKbn2 = value
        End Set
    End Property
#End Region
#Region "区分_3"
    ''' <summary>
    ''' 区分_3  kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn3 As String
    ''' <summary>
    ''' 区分_3
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分_3</returns>
    ''' <remarks></remarks>
    Public Property Kbn3() As String
        Get
            Return strKbn3
        End Get
        Set(ByVal value As String)
            strKbn3 = value
        End Set
    End Property
#End Region
#Region "保証書NO 対象範囲"
    ''' <summary>
    ''' 保証書NO 対象範囲 
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoNoHani As Integer
    ''' <summary>
    ''' 保証書NO 対象範囲
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO 対象範囲</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoHani() As Integer
        Get
            Return intHosyousyoNoHani
        End Get
        Set(ByVal value As Integer)
            intHosyousyoNoHani = value
        End Set
    End Property
#End Region
#Region "保証書NO From"
    ''' <summary>
    ''' 保証書NO From hosyousyo_no
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoFrom As String
    ''' <summary>
    ''' 保証書NO From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO From</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoFrom() As String
        Get
            Return strHosyousyoNoFrom
        End Get
        Set(ByVal value As String)
            strHosyousyoNoFrom = value
        End Set
    End Property
#End Region
#Region "保証書NO To"
    ''' <summary>
    ''' 保証書NO To hosyousyo_no
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoTo As String
    ''' <summary>
    ''' 保証書NO To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO To</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoTo() As String
        Get
            Return strHosyousyoNoTo
        End Get
        Set(ByVal value As String)
            strHosyousyoNoTo = value
        End Set
    End Property
#End Region
#Region "加盟店ｺｰﾄﾞ"
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ kameiten_cd
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region
#Region "加盟店カナ1"
    ''' <summary>
    ''' 加盟店カナ1 tenmei_kana1
    ''' </summary>
    ''' <remarks></remarks>
    Private strTenmeiKana1 As String
    ''' <summary>
    ''' 加盟店カナ1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店カナ1</returns>
    ''' <remarks></remarks>
    Public Property TenmeiKana1() As String
        Get
            Return strTenmeiKana1
        End Get
        Set(ByVal value As String)
            strTenmeiKana1 = value
        End Set
    End Property
#End Region
#Region "系列ｺｰﾄﾞ"
    ''' <summary>
    ''' 系列ｺｰﾄﾞ keiretu_cd 
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' 系列ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 系列ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region
#Region "営業所ｺｰﾄﾞ"
    ''' <summary>
    ''' 営業所ｺｰﾄﾞ eigyousyo_cd 
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoCd As String
    ''' <summary>
    ''' 営業所ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業所ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property EigyousyoCd() As String
        Get
            Return strEigyousyoCd
        End Get
        Set(ByVal value As String)
            strEigyousyoCd = value
        End Set
    End Property
#End Region
#Region "調査会社コード＋調査会社事業所コード"
    ''' <summary>
    ''' 調査会社コード＋調査会社事業所コード tys_kaisya_cd tys_kaisya_jigyousyo_cd
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' 調査会社コード＋調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社コード＋調査会社事業所コード</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region
#Region "工事会社コード＋工事会社事業所コード"
    ''' <summary>
    ''' 工事会社コード＋工事会社事業所コード koj_gaisya_cd koj_gaisya_jigyousyo_cd
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaCd As String
    ''' <summary>
    ''' 工事会社コード＋工事会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社コード＋工事会社事業所コード</returns>
    ''' <remarks></remarks>
    Public Property KojGaisyaCd() As String
        Get
            Return strKojGaisyaCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaCd = value
        End Set
    End Property
#End Region
#Region "工事売上年月日From"
    ''' <summary>
    ''' 工事売上年月日From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojUriDateFrom As DateTime
    ''' <summary>
    ''' 工事売上年月日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事売上年月日From</returns>
    ''' <remarks></remarks>
    Public Property KojUriDateFrom() As DateTime
        Get
            Return dateKojUriDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateKojUriDateFrom = value
        End Set
    End Property
#End Region
#Region "工事売上年月日To"
    ''' <summary>
    ''' 工事売上年月日To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojUriDateTo As DateTime
    ''' <summary>
    ''' 工事売上年月日To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事売上年月日To</returns>
    ''' <remarks></remarks>
    Public Property KojUriDateTo() As DateTime
        Get
            Return dateKojUriDateTo
        End Get
        Set(ByVal value As DateTime)
            dateKojUriDateTo = value
        End Set
    End Property
#End Region
#Region "改良工事完了予定日From"
    ''' <summary>
    ''' 改良工事完了予定日From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojKanryYoteiDateFrom As DateTime
    ''' <summary>
    ''' 改良工事完了予定日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事完了予定日From</returns>
    ''' <remarks></remarks>
    Public Property KairyKojKanryYoteiDateFrom() As DateTime
        Get
            Return dateKairyKojKanryYoteiDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojKanryYoteiDateFrom = value
        End Set
    End Property
#End Region
#Region "改良工事完了予定日To"
    ''' <summary>
    ''' 改良工事完了予定日To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojKanryYoteiDateTo As DateTime
    ''' <summary>
    ''' 改良工事完了予定日To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事完了予定日To</returns>
    ''' <remarks></remarks>
    Public Property KairyKojKanryYoteiDateTo() As DateTime
        Get
            Return dateKairyKojKanryYoteiDateTo
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojKanryYoteiDateTo = value
        End Set
    End Property
#End Region
#Region "施主名"
    ''' <summary>
    ''' 施主名 sesyu_mei
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 施主名</returns>
    ''' <remarks></remarks>
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region
#Region "物件住所1+2"
    ''' <summary>
    ''' 物件住所1+2 bukken_jyuusyo12
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo12 As String
    ''' <summary>
    ''' 物件住所1+2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件住所1+2</returns>
    ''' <remarks></remarks>
    Public Property BukkenJyuusyo12() As String
        Get
            Return strBukkenJyuusyo12
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo12 = value
        End Set
    End Property
#End Region
#Region "備考"
    ''' <summary>
    ''' 備考 bikou
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <value></value>
    ''' <returns> 備考</returns>
    ''' <remarks></remarks>
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region
#Region "依頼日From"
    ''' <summary>
    ''' 依頼日From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDateFrom As DateTime
    ''' <summary>
    ''' 依頼日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事完了予定日From</returns>
    ''' <remarks></remarks>
    Public Property IraiDateFrom() As DateTime
        Get
            Return dateIraiDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateIraiDateFrom = value
        End Set
    End Property
#End Region
#Region "依頼日To"
    ''' <summary>
    ''' 依頼日To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDateTo As DateTime
    ''' <summary>
    ''' 依頼日To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事完了予定日To</returns>
    ''' <remarks></remarks>
    Public Property IraiDateTo() As DateTime
        Get
            Return dateIraiDateTo
        End Get
        Set(ByVal value As DateTime)
            dateIraiDateTo = value
        End Set
    End Property
#End Region
#Region "調査希望日From"
    ''' <summary>
    ''' 調査希望日From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTyousaKibouDateFrom As DateTime
    ''' <summary>
    ''' 調査希望日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査希望日From</returns>
    ''' <remarks></remarks>
    Public Property TyousaKibouDateFrom() As DateTime
        Get
            Return dateTyousaKibouDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateTyousaKibouDateFrom = value
        End Set
    End Property
#End Region
#Region "調査希望日To"
    ''' <summary>
    ''' 調査希望日To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTyousaKibouDateTo As DateTime
    ''' <summary>
    ''' 調査希望日To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査希望日To</returns>
    ''' <remarks></remarks>
    Public Property TyousaKibouDateTo() As DateTime
        Get
            Return dateTyousaKibouDateTo
        End Get
        Set(ByVal value As DateTime)
            dateTyousaKibouDateTo = value
        End Set
    End Property
#End Region
#Region "調査実施日From"
    ''' <summary>
    ''' 調査実施日From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTyousaJissiDateFrom As DateTime
    ''' <summary>
    ''' 調査実施日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査実施日From</returns>
    ''' <remarks></remarks>
    Public Property TyousaJissiDateFrom() As DateTime
        Get
            Return dateTyousaJissiDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateTyousaJissiDateFrom = value
        End Set
    End Property
#End Region
#Region "調査実施日To"
    ''' <summary>
    ''' 調査実施日To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTyousaJissiDateTo As DateTime
    ''' <summary>
    ''' 調査実施日To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査実施日To</returns>
    ''' <remarks></remarks>
    Public Property TyousaJissiDateTo() As DateTime
        Get
            Return dateTyousaJissiDateTo
        End Get
        Set(ByVal value As DateTime)
            dateTyousaJissiDateTo = value
        End Set
    End Property
#End Region
#Region "保証書発行日From"
    ''' <summary>
    ''' 保証書発行日From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakkouDateFrom As DateTime
    ''' <summary>
    ''' 保証書発行日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行日From</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouDateFrom() As DateTime
        Get
            Return dateHosyousyoHakkouDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakkouDateFrom = value
        End Set
    End Property
#End Region
#Region "保証書発行日To"
    ''' <summary>
    ''' 保証書発行日To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakkouDateTo As DateTime
    ''' <summary>
    ''' 保証書発行日To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行日To</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouDateTo() As DateTime
        Get
            Return dateHosyousyoHakkouDateTo
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakkouDateTo = value
        End Set
    End Property
#End Region
#Region "承諾書調査日From"
    ''' <summary>
    ''' 承諾書調査日From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoudakusyoTyousaDateFrom As DateTime
    ''' <summary>
    ''' 承諾書調査日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 承諾書調査日From</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoTyousaDateFrom() As DateTime
        Get
            Return dateSyoudakusyoTyousaDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateSyoudakusyoTyousaDateFrom = value
        End Set
    End Property
#End Region
#Region "承諾書調査日To"
    ''' <summary>
    ''' 承諾書調査日To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoudakusyoTyousaDateTo As DateTime
    ''' <summary>
    ''' 承諾書調査日To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 承諾書調査日To</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoTyousaDateTo() As DateTime
        Get
            Return dateSyoudakusyoTyousaDateTo
        End Get
        Set(ByVal value As DateTime)
            dateSyoudakusyoTyousaDateTo = value
        End Set
    End Property
#End Region
#Region "計画書作成日From"
    ''' <summary>
    ''' 計画書作成日From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKeikakusyoSakuseiDateFrom As DateTime
    ''' <summary>
    ''' 計画書作成日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 計画書作成日From</returns>
    ''' <remarks></remarks>
    Public Property KeikakusyoSakuseiDateFrom() As DateTime
        Get
            Return dateKeikakusyoSakuseiDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateKeikakusyoSakuseiDateFrom = value
        End Set
    End Property
#End Region
#Region "計画書作成日To"
    ''' <summary>
    ''' 計画書作成日To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKeikakusyoSakuseiDateTo As DateTime
    ''' <summary>
    ''' 計画書作成日To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 計画書作成日To</returns>
    ''' <remarks></remarks>
    Public Property KeikakusyoSakuseiDateTo() As DateTime
        Get
            Return dateKeikakusyoSakuseiDateTo
        End Get
        Set(ByVal value As DateTime)
            dateKeikakusyoSakuseiDateTo = value
        End Set
    End Property
#End Region
#Region "保証書発行依頼書着日From"
    ''' <summary>
    ''' 保証書発行依頼書着日From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakkouIraisyoTyakuDateFrom As DateTime
    ''' <summary>
    ''' 保証書発行依頼書着日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行依頼書着日From</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouIraisyoTyakuDateFrom() As DateTime
        Get
            Return dateHosyousyoHakkouIraisyoTyakuDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakkouIraisyoTyakuDateFrom = value
        End Set
    End Property
#End Region
#Region "保証書発行依頼書着日To"
    ''' <summary>
    ''' 保証書発行依頼書着日To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakkouIraisyoTyakuDateTo As DateTime
    ''' <summary>
    ''' 保証書発行依頼書着日To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行依頼書着日To</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouIraisyoTyakuDateTo() As DateTime
        Get
            Return dateHosyousyoHakkouIraisyoTyakuDateTo
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakkouIraisyoTyakuDateTo = value
        End Set
    End Property
#End Region
#Region "データ破棄種別"
    ''' <summary>
    ''' データ破棄種別
    ''' </summary>
    ''' <remarks></remarks>
    Private intDataHakiSyubetu As Integer
    ''' <summary>
    ''' データ破棄種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> データ破棄種別</returns>
    ''' <remarks></remarks>
    Public Property DataHakiSyubetu() As Integer
        Get
            Return intDataHakiSyubetu
        End Get
        Set(ByVal value As Integer)
            intDataHakiSyubetu = value
        End Set
    End Property
#End Region
#Region "予約済FLG"
    ''' <summary>
    ''' 予約済FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoyakuZumiFlg As Integer
    ''' <summary>
    ''' 予約済FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 予約済FLG</returns>
    ''' <remarks></remarks>
    Public Property YoyakuZumiFlg() As Integer
        Get
            Return intYoyakuZumiFlg
        End Get
        Set(ByVal value As Integer)
            intYoyakuZumiFlg = value
        End Set
    End Property
#End Region

#Region "分譲コード"
    ''' <summary>
    ''' 分譲コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intBunjouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' 分譲コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BunjouCd() As Integer
        Get
            Return intBunjouCd
        End Get
        Set(ByVal value As Integer)
            intBunjouCd = value
        End Set
    End Property
#End Region
#Region "物件名寄コード"
    ''' <summary>
    ''' 物件名寄コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenNayoseCd As String
    ''' <summary>
    ''' 物件名寄コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BukkenNayoseCd() As String
        Get
            Return strBukkenNayoseCd
        End Get
        Set(ByVal value As String)
            strBukkenNayoseCd = value
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
    Public Overridable Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
        End Set
    End Property
#End Region

#Region "東_西日本フラグ"
    ''' <summary>
    ''' 東_西日本フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTouzaiFlg As String
    ''' <summary>
    ''' 東_西日本フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 東_西日本フラグ</returns>
    ''' <remarks></remarks>
    Public Overridable Property TouzaiFlg() As String
        Get
            Return strTouzaiFlg
        End Get
        Set(ByVal value As String)
            strTouzaiFlg = value
        End Set
    End Property
#End Region


End Class
