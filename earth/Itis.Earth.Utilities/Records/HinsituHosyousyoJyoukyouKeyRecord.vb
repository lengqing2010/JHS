''' <summary>
''' 品質保証書状況で使用するKey項目を設定するクラスです<br/>
''' 検索条件として必要な情報のみ設定して下さい
''' </summary>
''' <remarks></remarks>
Public Class HinsituHosyousyoJyoukyouRecord

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
    ''' <returns> 依頼日From</returns>
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
    ''' <returns> 依頼日To</returns>
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

#Region "発行進捗状況1"
    ''' <summary>
    ''' 発行進捗状況1
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus1 As Integer = Integer.MinValue
    ''' <summary>
    ''' 発行進捗状況1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus1() As Integer
        Get
            Return intHakkouStatus1
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus1 = value
        End Set
    End Property
#End Region
#Region "発行進捗状況2"
    ''' <summary>
    ''' 発行進捗状況2
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus2 As Integer = Integer.MinValue
    ''' <summary>
    ''' 発行進捗状況2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus2() As Integer
        Get
            Return intHakkouStatus2
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus2 = value
        End Set
    End Property
#End Region
#Region "発行進捗状況3"
    ''' <summary>
    ''' 発行進捗状況3
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus3 As Integer = Integer.MinValue
    ''' <summary>
    ''' 発行進捗状況3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus3() As Integer
        Get
            Return intHakkouStatus3
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus3 = value
        End Set
    End Property
#End Region
#Region "発行進捗状況4"
    ''' <summary>
    ''' 発行進捗状況4
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus4 As Integer = Integer.MinValue
    ''' <summary>
    ''' 発行進捗状況4
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus4() As Integer
        Get
            Return intHakkouStatus4
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus4 = value
        End Set
    End Property
#End Region
#Region "発行進捗状況5"
    ''' <summary>
    ''' 発行進捗状況5
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus5 As Integer = Integer.MinValue
    ''' <summary>
    ''' 発行進捗状況5
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus5() As Integer
        Get
            Return intHakkouStatus5
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus5 = value
        End Set
    End Property
#End Region
#Region "発行進捗状況6"
    ''' <summary>
    ''' 発行進捗状況6
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus6 As Integer = Integer.MinValue
    ''' <summary>
    ''' 発行進捗状況6
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus6() As Integer
        Get
            Return intHakkouStatus6
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus6 = value
        End Set
    End Property
#End Region

#Region "発行タイミング"
    ''' <summary>
    ''' 発行タイミング
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouTiming As Integer = Integer.MinValue
    ''' <summary>
    ''' 発行タイミング
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouTiming() As Integer
        Get
            Return intHakkouTiming
        End Get
        Set(ByVal value As Integer)
            intHakkouTiming = value
        End Set
    End Property
#End Region

#Region "保証書発行依頼書着日 空チェックボックス"
    ''' <summary>
    ''' 保証書発行依頼書着日 空チェックボックス
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakkouIraisyoTyakuDateChk As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証書発行依頼書着日 空チェックボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouIraisyoTyakuDateChk() As Integer
        Get
            Return intHosyousyoHakkouIraisyoTyakuDateChk
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakkouIraisyoTyakuDateChk = value
        End Set
    End Property
#End Region

#Region "保証書発行日 空チェックボックス"
    ''' <summary>
    ''' 保証書発行日 空チェックボックス
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakkouDateChk As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証書発行日 空チェックボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouDateChk() As Integer
        Get
            Return intHosyousyoHakkouDateChk
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakkouDateChk = value
        End Set
    End Property
#End Region

#Region "保証書再発行日From"
    ''' <summary>
    ''' 保証書再発行日From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoSaihakDateFrom As DateTime
    ''' <summary>
    ''' 保証書再発行日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書再発行日From</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoSaihakDateFrom() As DateTime
        Get
            Return dateHosyousyoSaihakDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDateFrom = value
        End Set
    End Property
#End Region
#Region "保証書再発行日To"
    ''' <summary>
    ''' 保証書再発行日To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoSaihakDateTo As DateTime
    ''' <summary>
    ''' 保証書再発行日To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書再発行日To</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoSaihakDateTo() As DateTime
        Get
            Return dateHosyousyoSaihakDateTo
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDateTo = value
        End Set
    End Property
#End Region

#Region "発行依頼日時 空チェックボックス"
    ''' <summary>
    ''' 発行依頼日時 空チェックボックス
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakIraiTimeChk As Integer = Integer.MinValue
    ''' <summary>
    ''' 発行依頼日時 空チェックボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakIraiTimeChk() As Integer
        Get
            Return intHakIraiTimeChk
        End Get
        Set(ByVal value As Integer)
            intHakIraiTimeChk = value
        End Set
    End Property
#End Region
#Region "発行依頼日時From"
    ''' <summary>
    ''' 発行依頼日時From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiTimeFrom As DateTime
    ''' <summary>
    ''' 発行依頼日時From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼日時From</returns>
    ''' <remarks></remarks>
    Public Property HakIraiTimeFrom() As DateTime
        Get
            Return dateHakIraiTimeFrom
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiTimeFrom = value
        End Set
    End Property
#End Region
#Region "発行依頼日時To"
    ''' <summary>
    ''' 発行依頼日時To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiTimeTo As DateTime
    ''' <summary>
    ''' 発行依頼日時To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼日時To</returns>
    ''' <remarks></remarks>
    Public Property HakIraiTimeTo() As DateTime
        Get
            Return dateHakIraiTimeTo
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiTimeTo = value
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
    ''' <returns> 保証期間(加盟店)</returns>
    ''' <remarks></remarks>
    Public Property HosyouKikanMK() As Integer
        Get
            Return intHosyouKikanMK
        End Get
        Set(ByVal value As Integer)
            intHosyouKikanMK = value
        End Set
    End Property
#End Region
#Region "保証期間(物件)"
    ''' <summary>
    ''' 保証期間(物件)
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikanTJ As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証期間(物件)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証期間(物件)</returns>
    ''' <remarks></remarks>
    Public Property HosyouKikanTJ() As Integer
        Get
            Return intHosyouKikanTJ
        End Get
        Set(ByVal value As Integer)
            intHosyouKikanTJ = value
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
    Public Property HakIraiHwDate() As DateTime
        Get
            Return dateHakIraiHwDate
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiHwDate = value
        End Set
    End Property
#End Region

End Class
