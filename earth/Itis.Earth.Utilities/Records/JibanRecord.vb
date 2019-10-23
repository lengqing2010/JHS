Imports Itis.ApplicationBlocks.ExceptionManagement
''' <summary>
''' 地盤データのレコードクラスです
''' </summary>
''' <remarks>地盤テーブルの全レコードカラムに加え、邸別データを保持してます<br/>
'''          商品コード１の邸別請求レコード：TeibetuSeikyuuRecord<br/>
'''          商品コード２の邸別請求レコード：Dictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          商品コード３の邸別請求レコード：Dictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          商品コード４の邸別請求レコード：Dictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
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
Public Class JibanRecord
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

#Region "受注物件名"
    ''' <summary>
    ''' 受注物件名
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyutyuuBukkenMei As String
    ''' <summary>
    ''' 受注物件名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 受注物件名</returns>
    ''' <remarks></remarks>
    <TableMap("jutyuu_bukken_mei", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Overrides Property JyutyuuBukkenMei() As String
        Get
            Return strJyutyuuBukkenMei
        End Get
        Set(ByVal value As String)
            strJyutyuuBukkenMei = value
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
    ''' <returns> 分譲コード</returns>
    ''' <remarks></remarks>
    <TableMap("bunjou_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property BunjouCd() As Integer
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
    Private strBukkenNayoseCd As String = String.Empty
    ''' <summary>
    ''' 物件名寄コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件名寄コード</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_nayose_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=11)> _
    Public Overrides Property BukkenNayoseCd() As String
        Get
            Return strBukkenNayoseCd
        End Get
        Set(ByVal value As String)
            strBukkenNayoseCd = value
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

#Region "商品区分"
    ''' <summary>
    ''' 商品区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhinKbn As Integer
    ''' <summary>
    ''' 商品区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品区分</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_kbn", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SyouhinKbn() As Integer
        Get
            Return intSyouhinKbn
        End Get
        Set(ByVal value As Integer)
            intSyouhinKbn = value
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
    <TableMap("irai_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property IraiDate() As DateTime
        Get
            Return dateIraiDate
        End Get
        Set(ByVal value As DateTime)
            dateIraiDate = value
        End Set
    End Property
#End Region

#Region "依頼担当者"
    ''' <summary>
    ''' 依頼担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strIraiTantousyaMei As String
    ''' <summary>
    ''' 依頼担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼担当者</returns>
    ''' <remarks></remarks>
    <TableMap("irai_tantousya_mei", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property IraiTantousyaMei() As String
        Get
            Return strIraiTantousyaMei
        End Get
        Set(ByVal value As String)
            strIraiTantousyaMei = value
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

#Region "TH瑕疵有無"
    ''' <summary>
    ''' TH瑕疵有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intThKasiUmu As Integer
    ''' <summary>
    ''' TH瑕疵有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> TH瑕疵有無</returns>
    ''' <remarks></remarks>
    <TableMap("th_kasi_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property ThKasiUmu() As Integer
        Get
            Return intThKasiUmu
        End Get
        Set(ByVal value As Integer)
            intThKasiUmu = value
        End Set
    End Property
#End Region

#Region "階層"
    ''' <summary>
    ''' 階層
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisou As Integer = Integer.MinValue
    ''' <summary>
    ''' 階層
    ''' </summary>
    ''' <value></value>
    ''' <returns> 階層</returns>
    ''' <remarks></remarks>
    <TableMap("kaisou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Kaisou() As Integer
        Get
            Return intKaisou
        End Get
        Set(ByVal value As Integer)
            intKaisou = value
        End Set
    End Property
#End Region

#Region "新築建替"
    ''' <summary>
    ''' 新築建替
    ''' </summary>
    ''' <remarks></remarks>
    Private intSintikuTatekae As Integer
    ''' <summary>
    ''' 新築建替
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新築建替</returns>
    ''' <remarks></remarks>
    <TableMap("sintiku_tatekae", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SintikuTatekae() As Integer
        Get
            Return intSintikuTatekae
        End Get
        Set(ByVal value As Integer)
            intSintikuTatekae = value
        End Set
    End Property
#End Region

#Region "構造"
    ''' <summary>
    ''' 構造
    ''' </summary>
    ''' <remarks></remarks>
    Private intKouzou As Integer
    ''' <summary>
    ''' 構造
    ''' </summary>
    ''' <value></value>
    ''' <returns> 構造</returns>
    ''' <remarks></remarks>
    <TableMap("kouzou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Kouzou() As Integer
        Get
            Return intKouzou
        End Get
        Set(ByVal value As Integer)
            intKouzou = value
        End Set
    End Property
#End Region

#Region "構造MEMO"
    ''' <summary>
    ''' 構造MEMO
    ''' </summary>
    ''' <remarks></remarks>
    Private strKouzouMemo As String
    ''' <summary>
    ''' 構造MEMO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 構造MEMO</returns>
    ''' <remarks></remarks>
    <TableMap("kouzou_memo", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overrides Property KouzouMemo() As String
        Get
            Return strKouzouMemo
        End Get
        Set(ByVal value As String)
            strKouzouMemo = value
        End Set
    End Property
#End Region

#Region "車庫"
    ''' <summary>
    ''' 車庫
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyako As Integer
    ''' <summary>
    ''' 車庫
    ''' </summary>
    ''' <value></value>
    ''' <returns> 車庫</returns>
    ''' <remarks></remarks>
    <TableMap("syako", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Syako() As Integer
        Get
            Return intSyako
        End Get
        Set(ByVal value As Integer)
            intSyako = value
        End Set
    End Property
#End Region

#Region "根切り深さ"
    ''' <summary>
    ''' 根切り深さ
    ''' </summary>
    ''' <remarks></remarks>
    Private decNegiriHukasa As Decimal
    ''' <summary>
    ''' 根切り深さ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 根切り深さ</returns>
    ''' <remarks></remarks>
    <TableMap("negiri_hukasa", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property NegiriHukasa() As Decimal
        Get
            Return decNegiriHukasa
        End Get
        Set(ByVal value As Decimal)
            decNegiriHukasa = value
        End Set
    End Property
#End Region

#Region "予定盛土厚さ"
    ''' <summary>
    ''' 予定盛土厚さ
    ''' </summary>
    ''' <remarks></remarks>
    Private decYoteiMoritutiAtusa As Decimal
    ''' <summary>
    ''' 予定盛土厚さ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 予定盛土厚さ</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_morituti_atusa", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property YoteiMoritutiAtusa() As Decimal
        Get
            Return decYoteiMoritutiAtusa
        End Get
        Set(ByVal value As Decimal)
            decYoteiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "予定基礎"
    ''' <summary>
    ''' 予定基礎
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoteiKs As Integer
    ''' <summary>
    ''' 予定基礎
    ''' </summary>
    ''' <value></value>
    ''' <returns> 予定基礎</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YoteiKs() As Integer
        Get
            Return intYoteiKs
        End Get
        Set(ByVal value As Integer)
            intYoteiKs = value
        End Set
    End Property
#End Region

#Region "予定基礎MEMO"
    ''' <summary>
    ''' 予定基礎MEMO
    ''' </summary>
    ''' <remarks></remarks>
    Private strYoteiKsMemo As String
    ''' <summary>
    ''' 予定基礎MEMO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 予定基礎MEMO</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks_memo", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overrides Property YoteiKsMemo() As String
        Get
            Return strYoteiKsMemo
        End Get
        Set(ByVal value As String)
            strYoteiKsMemo = value
        End Set
    End Property
#End Region

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
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "調査会社事業所コード"
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "調査方法"
    ''' <summary>
    ''' 調査方法
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHouhou As Integer = Integer.MinValue
    ''' <summary>
    ''' 調査方法
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査方法</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TysHouhou() As Integer
        Get
            Return intTysHouhou
        End Get
        Set(ByVal value As Integer)
            intTysHouhou = value
        End Set
    End Property
#End Region

#Region "調査概要"
    ''' <summary>
    ''' 調査概要
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysGaiyou As Integer
    ''' <summary>
    ''' 調査概要
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査概要</returns>
    ''' <remarks></remarks>
    <TableMap("tys_gaiyou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TysGaiyou() As Integer
        Get
            Return intTysGaiyou
        End Get
        Set(ByVal value As Integer)
            intTysGaiyou = value
        End Set
    End Property
#End Region

#Region "FCﾋﾞﾙﾀﾞｰ販売金額"
    ''' <summary>
    ''' FCﾋﾞﾙﾀﾞｰ販売金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intFcBuilderHanbaiGaku As Integer
    ''' <summary>
    ''' FCﾋﾞﾙﾀﾞｰ販売金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> FCﾋﾞﾙﾀﾞｰ販売金額</returns>
    ''' <remarks></remarks>
    <TableMap("fc_builder_hanbai_gaku", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property FcBuilderHanbaiGaku() As Integer
        Get
            Return intFcBuilderHanbaiGaku
        End Get
        Set(ByVal value As Integer)
            intFcBuilderHanbaiGaku = value
        End Set
    End Property
#End Region

#Region "調査希望日"
    ''' <summary>
    ''' 調査希望日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKibouDate As DateTime
    ''' <summary>
    ''' 調査希望日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査希望日</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysKibouDate() As DateTime
        Get
            Return dateTysKibouDate
        End Get
        Set(ByVal value As DateTime)
            dateTysKibouDate = value
        End Set
    End Property
#End Region

#Region "調査希望時間"
    ''' <summary>
    ''' 調査希望時間
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKibouJikan As String
    ''' <summary>
    ''' 調査希望時間
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査希望時間</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_jikan", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=26)> _
    Public Overrides Property TysKibouJikan() As String
        Get
            Return strTysKibouJikan
        End Get
        Set(ByVal value As String)
            strTysKibouJikan = value
        End Set
    End Property
#End Region

#Region "立会有無"
    ''' <summary>
    ''' 立会有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatiaiUmu As Integer
    ''' <summary>
    ''' 立会有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 立会有無</returns>
    ''' <remarks></remarks>
    <TableMap("tatiai_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TatiaiUmu() As Integer
        Get
            Return intTatiaiUmu
        End Get
        Set(ByVal value As Integer)
            intTatiaiUmu = value
        End Set
    End Property
#End Region

#Region "立会者コード"
    ''' <summary>
    ''' 立会者コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatiaisyaCd As Integer
    ''' <summary>
    ''' 立会者コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 立会者コード</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TatiaisyaCd() As Integer
        Get
            Return intTatiaisyaCd
        End Get
        Set(ByVal value As Integer)
            intTatiaisyaCd = value
        End Set
    End Property
#End Region

#Region "添付_平面図"
    ''' <summary>
    ''' 添付_平面図
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuHeimenzu As Integer
    ''' <summary>
    ''' 添付_平面図
    ''' </summary>
    ''' <value></value>
    ''' <returns> 添付_平面図</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_heimenzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuHeimenzu() As Integer
        Get
            Return intTenpuHeimenzu
        End Get
        Set(ByVal value As Integer)
            intTenpuHeimenzu = value
        End Set
    End Property
#End Region

#Region "添付_立面図"
    ''' <summary>
    ''' 添付_立面図
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuRitumenzu As Integer
    ''' <summary>
    ''' 添付_立面図
    ''' </summary>
    ''' <value></value>
    ''' <returns> 添付_立面図</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_ritumenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuRitumenzu() As Integer
        Get
            Return intTenpuRitumenzu
        End Get
        Set(ByVal value As Integer)
            intTenpuRitumenzu = value
        End Set
    End Property
#End Region

#Region "添付_基礎伏図"
    ''' <summary>
    ''' 添付_基礎伏図
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuKsHusezu As Integer
    ''' <summary>
    ''' 添付_基礎伏図
    ''' </summary>
    ''' <value></value>
    ''' <returns> 添付_基礎伏図</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_ks_husezu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuKsHusezu() As Integer
        Get
            Return intTenpuKsHusezu
        End Get
        Set(ByVal value As Integer)
            intTenpuKsHusezu = value
        End Set
    End Property
#End Region

#Region "添付_断面図"
    ''' <summary>
    ''' 添付_断面図
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuDanmenzu As Integer
    ''' <summary>
    ''' 添付_断面図
    ''' </summary>
    ''' <value></value>
    ''' <returns> 添付_断面図</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_danmenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuDanmenzu() As Integer
        Get
            Return intTenpuDanmenzu
        End Get
        Set(ByVal value As Integer)
            intTenpuDanmenzu = value
        End Set
    End Property
#End Region

#Region "添付_矩計図"
    ''' <summary>
    ''' 添付_矩計図
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuKukeizu As Integer
    ''' <summary>
    ''' 添付_矩計図
    ''' </summary>
    ''' <value></value>
    ''' <returns> 添付_矩計図</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_kukeizu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuKukeizu() As Integer
        Get
            Return intTenpuKukeizu
        End Get
        Set(ByVal value As Integer)
            intTenpuKukeizu = value
        End Set
    End Property
#End Region

#Region "承諾書調査日"
    ''' <summary>
    ''' 承諾書調査日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoudakusyoTysDate As DateTime
    ''' <summary>
    ''' 承諾書調査日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 承諾書調査日</returns>
    ''' <remarks></remarks>
    <TableMap("syoudakusyo_tys_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property SyoudakusyoTysDate() As DateTime
        Get
            Return dateSyoudakusyoTysDate
        End Get
        Set(ByVal value As DateTime)
            dateSyoudakusyoTysDate = value
        End Set
    End Property
#End Region

#Region "調査実施日"
    ''' <summary>
    ''' 調査実施日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysJissiDate As DateTime
    ''' <summary>
    ''' 調査実施日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査実施日</returns>
    ''' <remarks></remarks>
    <TableMap("tys_jissi_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysJissiDate() As DateTime
        Get
            Return dateTysJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateTysJissiDate = value
        End Set
    End Property
#End Region

#Region "土質"
    ''' <summary>
    ''' 土質
    ''' </summary>
    ''' <remarks></remarks>
    Private strDositu As String
    ''' <summary>
    ''' 土質
    ''' </summary>
    ''' <value></value>
    ''' <returns> 土質</returns>
    ''' <remarks></remarks>
    <TableMap("dositu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overrides Property Dositu() As String
        Get
            Return strDositu
        End Get
        Set(ByVal value As String)
            strDositu = value
        End Set
    End Property
#End Region

#Region "許容支持力"
    ''' <summary>
    ''' 許容支持力
    ''' </summary>
    ''' <remarks></remarks>
    Private strKyoyouSijiryoku As String
    ''' <summary>
    ''' 許容支持力
    ''' </summary>
    ''' <value></value>
    ''' <returns> 許容支持力</returns>
    ''' <remarks></remarks>
    <TableMap("kyoyou_sijiryoku", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overrides Property KyoyouSijiryoku() As String
        Get
            Return strKyoyouSijiryoku
        End Get
        Set(ByVal value As String)
            strKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "判定コード1"
    ''' <summary>
    ''' 判定コード1
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiCd1 As Integer
    ''' <summary>
    ''' 判定コード1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 判定コード1</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_cd1", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HanteiCd1() As Integer
        Get
            Return intHanteiCd1
        End Get
        Set(ByVal value As Integer)
            intHanteiCd1 = value
        End Set
    End Property
#End Region

#Region "判定コード2"
    ''' <summary>
    ''' 判定コード2
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiCd2 As Integer
    ''' <summary>
    ''' 判定コード2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 判定コード2</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_cd2", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HanteiCd2() As Integer
        Get
            Return intHanteiCd2
        End Get
        Set(ByVal value As Integer)
            intHanteiCd2 = value
        End Set
    End Property
#End Region

#Region "判定接続文字"
    ''' <summary>
    ''' 判定接続文字
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiSetuzokuMoji As Integer
    ''' <summary>
    ''' 判定接続文字
    ''' </summary>
    ''' <value></value>
    ''' <returns> 判定接続文字</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_setuzoku_moji", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HanteiSetuzokuMoji() As Integer
        Get
            Return intHanteiSetuzokuMoji
        End Get
        Set(ByVal value As Integer)
            intHanteiSetuzokuMoji = value
        End Set
    End Property
#End Region

#Region "担当者コード"
    ''' <summary>
    ''' 担当者コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intTantousyaCd As Integer
    ''' <summary>
    ''' 担当者コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当者コード</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TantousyaCd() As Integer
        Get
            Return intTantousyaCd
        End Get
        Set(ByVal value As Integer)
            intTantousyaCd = value
        End Set
    End Property
#End Region

#Region "担当者名"
    ''' <summary>
    ''' 担当者名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantousyaMei As String
    ''' <summary>
    ''' 担当者名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当者名</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_mei")> _
    Public Overrides Property TantousyaMei() As String
        Get
            Return strTantousyaMei
        End Get
        Set(ByVal value As String)
            strTantousyaMei = value
        End Set
    End Property
#End Region

#Region "承認者コード"
    ''' <summary>
    ''' 承認者コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouninsyaCd As Integer
    ''' <summary>
    ''' 承認者コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 承認者コード</returns>
    ''' <remarks></remarks>
    <TableMap("syouninsya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SyouninsyaCd() As Integer
        Get
            Return intSyouninsyaCd
        End Get
        Set(ByVal value As Integer)
            intSyouninsyaCd = value
        End Set
    End Property
#End Region

#Region "承認者名"
    ''' <summary>
    ''' 承認者名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouninsyaMei As String
    ''' <summary>
    ''' 承認者名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 承認者名</returns>
    ''' <remarks></remarks>
    <TableMap("syouninsya_mei")> _
    Public Overrides Property SyouninsyaMei() As String
        Get
            Return strSyouninsyaMei
        End Get
        Set(ByVal value As String)
            strSyouninsyaMei = value
        End Set
    End Property
#End Region

#Region "計画書作成日"
    ''' <summary>
    ''' 計画書作成日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKeikakusyoSakuseiDate As DateTime
    ''' <summary>
    ''' 計画書作成日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 計画書作成日</returns>
    ''' <remarks></remarks>
    <TableMap("keikakusyo_sakusei_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KeikakusyoSakuseiDate() As DateTime
        Get
            Return dateKeikakusyoSakuseiDate
        End Get
        Set(ByVal value As DateTime)
            dateKeikakusyoSakuseiDate = value
        End Set
    End Property
#End Region

#Region "基礎断面コード"
    ''' <summary>
    ''' 基礎断面コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsDanmenCd As Integer
    ''' <summary>
    ''' 基礎断面コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎断面コード</returns>
    ''' <remarks></remarks>
    <TableMap("ks_danmen_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsDanmenCd() As Integer
        Get
            Return intKsDanmenCd
        End Get
        Set(ByVal value As Integer)
            intKsDanmenCd = value
        End Set
    End Property
#End Region

#Region "断面図説明"
    ''' <summary>
    ''' 断面図説明
    ''' </summary>
    ''' <remarks></remarks>
    Private strDanmenzuSetumei As String
    ''' <summary>
    ''' 断面図説明
    ''' </summary>
    ''' <value></value>
    ''' <returns> 断面図説明</returns>
    ''' <remarks></remarks>
    <TableMap("danmenzu_setumei")> _
    Public Overrides Property DanmenzuSetumei() As String
        Get
            Return strDanmenzuSetumei
        End Get
        Set(ByVal value As String)
            strDanmenzuSetumei = value
        End Set
    End Property
#End Region

#Region "考察"
    ''' <summary>
    ''' 考察
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousatu As String
    ''' <summary>
    ''' 考察
    ''' </summary>
    ''' <value></value>
    ''' <returns> 考察</returns>
    ''' <remarks></remarks>
    <TableMap("kousatu")> _
    Public Overrides Property Kousatu() As String
        Get
            Return strKousatu
        End Get
        Set(ByVal value As String)
            strKousatu = value
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
    <TableMap("ks_hkks_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("ks_koj_kanry_hkks_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("hosyousyo_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakDate() As DateTime
        Get
            Return dateHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakDate = value
        End Set
    End Property
#End Region

#Region "印刷保証書発行日"
    ''' <summary>
    ''' 印刷保証書発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateInsatuHosyousyoHakDate As DateTime
    ''' <summary>
    ''' 印刷保証書発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 印刷保証書発行日</returns>
    ''' <remarks></remarks>
    <TableMap("insatu_hosyousyo_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property InsatuHosyousyoHakDate() As DateTime
        Get
            Return dateInsatuHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateInsatuHosyousyoHakDate = value
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
    <TableMap("hosyou_kaisi_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("hosyou_nasi_riyuu")> _
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
    <TableMap("hosyousyo_saihak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoSaihakDate() As DateTime
        Get
            Return dateHosyousyoSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDate = value
        End Set
    End Property
#End Region

#Region "調査報告書有無"
    ''' <summary>
    ''' 調査報告書有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHkksUmu As Integer
    ''' <summary>
    ''' 調査報告書有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査報告書有無</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TysHkksUmu() As Integer
        Get
            Return intTysHkksUmu
        End Get
        Set(ByVal value As Integer)
            intTysHkksUmu = value
        End Set
    End Property
#End Region

#Region "調査報告書受理詳細"
    ''' <summary>
    ''' 調査報告書受理詳細
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHkksJyuriSyousai As String
    ''' <summary>
    ''' 調査報告書受理詳細
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査報告書受理詳細</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_jyuri_syousai")> _
    Public Overrides Property TysHkksJyuriSyousai() As String
        Get
            Return strTysHkksJyuriSyousai
        End Get
        Set(ByVal value As String)
            strTysHkksJyuriSyousai = value
        End Set
    End Property
#End Region

#Region "調査報告書受理日"
    ''' <summary>
    ''' 調査報告書受理日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysHkksJyuriDate As DateTime
    ''' <summary>
    ''' 調査報告書受理日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査報告書受理日</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_jyuri_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysHkksJyuriDate() As DateTime
        Get
            Return dateTysHkksJyuriDate
        End Get
        Set(ByVal value As DateTime)
            dateTysHkksJyuriDate = value
        End Set
    End Property
#End Region

#Region "調査報告書発送日"
    ''' <summary>
    ''' 調査報告書発送日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysHkksHakDate As DateTime
    ''' <summary>
    ''' 調査報告書発送日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査報告書発送日</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysHkksHakDate() As DateTime
        Get
            Return dateTysHkksHakDate
        End Get
        Set(ByVal value As DateTime)
            dateTysHkksHakDate = value
        End Set
    End Property
#End Region

#Region "調査報告書再発行日"
    ''' <summary>
    ''' 調査報告書再発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysHkksSaihakDate As DateTime
    ''' <summary>
    ''' 調査報告書再発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査報告書再発行日</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_saihak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysHkksSaihakDate() As DateTime
        Get
            Return dateTysHkksSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateTysHkksSaihakDate = value
        End Set
    End Property
#End Region

#Region "工事報告書有無"
    ''' <summary>
    ''' 工事報告書有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojHkksUmu As Integer
    ''' <summary>
    ''' 工事報告書有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事報告書有無</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojHkksUmu() As Integer
        Get
            Return intKojHkksUmu
        End Get
        Set(ByVal value As Integer)
            intKojHkksUmu = value
        End Set
    End Property
#End Region

#Region "工事報告書受理詳細"
    ''' <summary>
    ''' 工事報告書受理詳細
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojHkksJuriSyousai As String
    ''' <summary>
    ''' 工事報告書受理詳細
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事報告書受理詳細</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_juri_syousai")> _
    Public Overrides Property KojHkksJuriSyousai() As String
        Get
            Return strKojHkksJuriSyousai
        End Get
        Set(ByVal value As String)
            strKojHkksJuriSyousai = value
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
    <TableMap("koj_hkks_juri_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojHkksJuriDate() As DateTime
        Get
            Return dateKojHkksJuriDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksJuriDate = value
        End Set
    End Property
#End Region

#Region "工事報告書発送日"
    ''' <summary>
    ''' 工事報告書発送日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojHkksHassouDate As DateTime
    ''' <summary>
    ''' 工事報告書発送日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事報告書発送日</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_hassou_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojHkksHassouDate() As DateTime
        Get
            Return dateKojHkksHassouDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksHassouDate = value
        End Set
    End Property
#End Region

#Region "工事報告書再発行日"
    ''' <summary>
    ''' 工事報告書再発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojHkksSaihakDate As DateTime
    ''' <summary>
    ''' 工事報告書再発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事報告書再発行日</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_saihak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojHkksSaihakDate() As DateTime
        Get
            Return dateKojHkksSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksSaihakDate = value
        End Set
    End Property
#End Region

#Region "工事会社コード"
    ''' <summary>
    ''' 工事会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaCd As String
    ''' <summary>
    ''' 工事会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社コード</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_cd")> _
    Public Overrides Property KojGaisyaCd() As String
        Get
            Return strKojGaisyaCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaCd = value
        End Set
    End Property
#End Region

#Region "工事会社事業所コード"
    ''' <summary>
    ''' 工事会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaJigyousyoCd As String
    ''' <summary>
    ''' 工事会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_jigyousyo_cd")> _
    Public Overrides Property KojGaisyaJigyousyoCd() As String
        Get
            Return strKojGaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "改良工事種別"
    ''' <summary>
    ''' 改良工事種別
    ''' </summary>
    ''' <remarks></remarks>
    Private intKairyKojSyubetu As Integer
    ''' <summary>
    ''' 改良工事種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事種別</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_syubetu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KairyKojSyubetu() As Integer
        Get
            Return intKairyKojSyubetu
        End Get
        Set(ByVal value As Integer)
            intKairyKojSyubetu = value
        End Set
    End Property
#End Region

#Region "改良工事完了予定日"
    ''' <summary>
    ''' 改良工事完了予定日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojKanryYoteiDate As DateTime
    ''' <summary>
    ''' 改良工事完了予定日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事完了予定日</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_kanry_yotei_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KairyKojKanryYoteiDate() As DateTime
        Get
            Return dateKairyKojKanryYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojKanryYoteiDate = value
        End Set
    End Property
#End Region

#Region "改良工事日"
    ''' <summary>
    ''' 改良工事日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojDate As DateTime
    ''' <summary>
    ''' 改良工事日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事日</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KairyKojDate() As DateTime
        Get
            Return dateKairyKojDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojDate = value
        End Set
    End Property
#End Region

#Region "改良工事完工速報着日"
    ''' <summary>
    ''' 改良工事完工速報着日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojSokuhouTykDate As DateTime
    ''' <summary>
    ''' 改良工事完工速報着日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事完工速報着日</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_sokuhou_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KairyKojSokuhouTykDate() As DateTime
        Get
            Return dateKairyKojSokuhouTykDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojSokuhouTykDate = value
        End Set
    End Property
#End Region

#Region "追加工事会社コード"
    ''' <summary>
    ''' 追加工事会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTKojKaisyaCd As String
    ''' <summary>
    ''' 追加工事会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 追加工事会社コード</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_cd")> _
    Public Overrides Property TKojKaisyaCd() As String
        Get
            Return strTKojKaisyaCd
        End Get
        Set(ByVal value As String)
            strTKojKaisyaCd = value
        End Set
    End Property
#End Region

#Region "追加工事会社事業所コード"
    ''' <summary>
    ''' 追加工事会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTKojKaisyaJigyousyoCd As String
    ''' <summary>
    ''' 追加工事会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 追加工事会社事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_jigyousyo_cd")> _
    Public Overrides Property TKojKaisyaJigyousyoCd() As String
        Get
            Return strTKojKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTKojKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "追加工事種別"
    ''' <summary>
    ''' 追加工事種別
    ''' </summary>
    ''' <remarks></remarks>
    Private intTKojSyubetu As Integer
    ''' <summary>
    ''' 追加工事種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 追加工事種別</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_syubetu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TKojSyubetu() As Integer
        Get
            Return intTKojSyubetu
        End Get
        Set(ByVal value As Integer)
            intTKojSyubetu = value
        End Set
    End Property
#End Region

#Region "追加工事完了予定日"
    ''' <summary>
    ''' 追加工事完了予定日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTKojKanryYoteiDate As DateTime
    ''' <summary>
    ''' 追加工事完了予定日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 追加工事完了予定日</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kanry_yotei_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TKojKanryYoteiDate() As DateTime
        Get
            Return dateTKojKanryYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateTKojKanryYoteiDate = value
        End Set
    End Property
#End Region

#Region "追加工事日"
    ''' <summary>
    ''' 追加工事日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTKojDate As DateTime
    ''' <summary>
    ''' 追加工事日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 追加工事日</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TKojDate() As DateTime
        Get
            Return dateTKojDate
        End Get
        Set(ByVal value As DateTime)
            dateTKojDate = value
        End Set
    End Property
#End Region

#Region "追加工事完工速報着日"
    ''' <summary>
    ''' 追加工事完工速報着日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTKojSokuhouTykDate As DateTime
    ''' <summary>
    ''' 追加工事完工速報着日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 追加工事完工速報着日</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_sokuhou_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TKojSokuhouTykDate() As DateTime
        Get
            Return dateTKojSokuhouTykDate
        End Get
        Set(ByVal value As DateTime)
            dateTKojSokuhouTykDate = value
        End Set
    End Property
#End Region

#Region "追加工事会社請求有無"
    ''' <summary>
    ''' 追加工事会社請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intTKojKaisyaSeikyuuUmu As Integer
    ''' <summary>
    ''' 追加工事会社請求有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 追加工事会社請求有無</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_seikyuu_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TKojKaisyaSeikyuuUmu() As Integer
        Get
            Return intTKojKaisyaSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intTKojKaisyaSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "調査結果登録日時"
    ''' <summary>
    ''' 調査結果登録日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKekkaAddDatetime As DateTime
    ''' <summary>
    ''' 調査結果登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査結果登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kekka_add_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysKekkaAddDatetime() As DateTime
        Get
            Return dateTysKekkaAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateTysKekkaAddDatetime = value
        End Set
    End Property
#End Region

#Region "調査結果更新日時"
    ''' <summary>
    ''' 調査結果更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKekkaUpdDatetime As DateTime
    ''' <summary>
    ''' 調査結果更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査結果更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kekka_upd_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysKekkaUpdDatetime() As DateTime
        Get
            Return dateTysKekkaUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateTysKekkaUpdDatetime = value
        End Set
    End Property
#End Region

#Region "同時依頼棟数"
    ''' <summary>
    ''' 同時依頼棟数
    ''' </summary>
    ''' <remarks></remarks>
    Private intDoujiIraiTousuu As Integer
    ''' <summary>
    ''' 同時依頼棟数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 同時依頼棟数</returns>
    ''' <remarks></remarks>
    <TableMap("douji_irai_tousuu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DoujiIraiTousuu() As Integer
        Get
            Return intDoujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuu = value
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

#Region "瑕疵有無"
    ''' <summary>
    ''' 瑕疵有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intKasiUmu As Integer
    ''' <summary>
    ''' 瑕疵有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 瑕疵有無</returns>
    ''' <remarks></remarks>
    <TableMap("kasi_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KasiUmu() As Integer
        Get
            Return intKasiUmu
        End Get
        Set(ByVal value As Integer)
            intKasiUmu = value
        End Set
    End Property
#End Region

#Region "工事会社請求有無"
    ''' <summary>
    ''' 工事会社請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojGaisyaSeikyuuUmu As Integer
    ''' <summary>
    ''' 工事会社請求有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社請求有無</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_seikyuu_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojGaisyaSeikyuuUmu() As Integer
        Get
            Return intKojGaisyaSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intKojGaisyaSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "返金処理FLG"
    ''' <summary>
    ''' 返金処理FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHenkinSyoriFlg As Integer
    ''' <summary>
    ''' 返金処理FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 返金処理FLG</returns>
    ''' <remarks></remarks>
    <TableMap("henkin_syori_flg", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HenkinSyoriFlg() As Integer
        Get
            Return intHenkinSyoriFlg
        End Get
        Set(ByVal value As Integer)
            intHenkinSyoriFlg = value
        End Set
    End Property
#End Region

#Region "返金処理日"
    ''' <summary>
    ''' 返金処理日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHenkinSyoriDate As DateTime
    ''' <summary>
    ''' 返金処理日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 返金処理日</returns>
    ''' <remarks></remarks>
    <TableMap("henkin_syori_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HenkinSyoriDate() As DateTime
        Get
            Return dateHenkinSyoriDate
        End Get
        Set(ByVal value As DateTime)
            dateHenkinSyoriDate = value
        End Set
    End Property
#End Region

#Region "工事担当者"
    ''' <summary>
    ''' 工事担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojTantousyaMei As String
    ''' <summary>
    ''' 工事担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事担当者</returns>
    ''' <remarks></remarks>
    <TableMap("koj_tantousya_mei")> _
    Public Overrides Property KojTantousyaMei() As String
        Get
            Return strKojTantousyaMei
        End Get
        Set(ByVal value As String)
            strKojTantousyaMei = value
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
    <TableMap("keiyu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Keiyu() As Integer
        Get
            Return intKeiyu
        End Get
        Set(ByVal value As Integer)
            intKeiyu = value
        End Set
    End Property
#End Region

#Region "工事仕様確認"
    ''' <summary>
    ''' 工事仕様確認
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojSiyouKakunin As Integer
    ''' <summary>
    ''' 工事仕様確認
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事仕様確認</returns>
    ''' <remarks></remarks>
    <TableMap("koj_siyou_kakunin", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojSiyouKakunin() As Integer
        Get
            Return intKojSiyouKakunin
        End Get
        Set(ByVal value As Integer)
            intKojSiyouKakunin = value
        End Set
    End Property
#End Region

#Region "工事仕様確認日"
    ''' <summary>
    ''' 工事仕様確認日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojSiyouKakuninDate As DateTime
    ''' <summary>
    ''' 工事仕様確認日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事仕様確認日</returns>
    ''' <remarks></remarks>
    <TableMap("koj_siyou_kakunin_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojSiyouKakuninDate() As DateTime
        Get
            Return dateKojSiyouKakuninDate
        End Get
        Set(ByVal value As DateTime)
            dateKojSiyouKakuninDate = value
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
    <TableMap("hosyousyo_hak_iraisyo_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("hosyousyo_hak_iraisyo_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakIraisyoTykDate() As DateTime
        Get
            Return dateHosyousyoHakIraisyoTykDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakIraisyoTykDate = value
        End Set
    End Property
#End Region

#Region "設計許容支持力"
    ''' <summary>
    ''' 設計許容支持力
    ''' </summary>
    ''' <remarks></remarks>
    Private decSekkeiKyoyouSijiryoku As Decimal
    ''' <summary>
    ''' 設計許容支持力
    ''' </summary>
    ''' <value></value>
    ''' <returns> 設計許容支持力</returns>
    ''' <remarks></remarks>
    <TableMap("sekkei_kyoyou_sijiryoku", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property SekkeiKyoyouSijiryoku() As Decimal
        Get
            Return decSekkeiKyoyouSijiryoku
        End Get
        Set(ByVal value As Decimal)
            decSekkeiKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "依頼予定棟数"
    ''' <summary>
    ''' 依頼予定棟数
    ''' </summary>
    ''' <remarks></remarks>
    Private intIraiYoteiTousuu As Integer
    ''' <summary>
    ''' 依頼予定棟数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼予定棟数</returns>
    ''' <remarks></remarks>
    <TableMap("irai_yotei_tousuu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property IraiYoteiTousuu() As Integer
        Get
            Return intIraiYoteiTousuu
        End Get
        Set(ByVal value As Integer)
            intIraiYoteiTousuu = value
        End Set
    End Property
#End Region

#Region "建物用途NO"
    ''' <summary>
    ''' 建物用途NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatemonoYoutoNo As Integer
    ''' <summary>
    ''' 建物用途NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建物用途NO</returns>
    ''' <remarks></remarks>
    <TableMap("tatemono_youto_no", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TatemonoYoutoNo() As Integer
        Get
            Return intTatemonoYoutoNo
        End Get
        Set(ByVal value As Integer)
            intTatemonoYoutoNo = value
        End Set
    End Property
#End Region

#Region "戸数"
    ''' <summary>
    ''' 戸数
    ''' </summary>
    ''' <remarks></remarks>
    Private intKosuu As Integer
    ''' <summary>
    ''' 戸数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 戸数</returns>
    ''' <remarks></remarks>
    <TableMap("kosuu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Kosuu() As Integer
        Get
            Return intKosuu
        End Get
        Set(ByVal value As Integer)
            intKosuu = value
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

#Region "調査連絡先_宛先"
    ''' <summary>
    ''' 調査連絡先_宛先
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiAtesakiMei As String
    ''' <summary>
    ''' 調査連絡先_宛先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_宛先</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_atesaki_mei", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=60)> _
    Public Overrides Property TysRenrakusakiAtesakiMei() As String
        Get
            Return strTysRenrakusakiAtesakiMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiAtesakiMei = value
        End Set
    End Property
#End Region

#Region "調査連絡先_TEL"
    ''' <summary>
    ''' 調査連絡先_TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTel As String
    ''' <summary>
    ''' 調査連絡先_TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_TEL</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tel", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiTel() As String
        Get
            Return strTysRenrakusakiTel
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTel = value
        End Set
    End Property
#End Region

#Region "調査連絡先_FAX"
    ''' <summary>
    ''' 調査連絡先_FAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiFax As String
    ''' <summary>
    ''' 調査連絡先_FAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_FAX</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_fax", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiFax() As String
        Get
            Return strTysRenrakusakiFax
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiFax = value
        End Set
    End Property
#End Region

#Region "調査連絡先_MAIL"
    ''' <summary>
    ''' 調査連絡先_MAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiMail As String
    ''' <summary>
    ''' 調査連絡先_MAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_MAIL</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_mail", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=64)> _
    Public Overrides Property TysRenrakusakiMail() As String
        Get
            Return strTysRenrakusakiMail
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiMail = value
        End Set
    End Property
#End Region

#Region "調査連絡先_担当者"
    ''' <summary>
    ''' 調査連絡先_担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String
    ''' <summary>
    ''' 調査連絡先_担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_担当者</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tantou_mei", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

#Region "登録ﾛｸﾞｲﾝﾕｰｻﾞｰID"
    ''' <summary>
    ''' 登録ﾛｸﾞｲﾝﾕｰｻﾞｰID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' 登録ﾛｸﾞｲﾝﾕｰｻﾞｰID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ﾛｸﾞｲﾝﾕｰｻﾞｰID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property AddLoginUserId() As String
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
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
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

#Region "商品コード１の邸別請求レコード"
    ''' <summary>
    ''' 商品コード１の邸別請求レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private objSyouhin1Record As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 商品コード１の邸別請求レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード１の邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Overrides Property Syouhin1Record() As TeibetuSeikyuuRecord
        Get
            Return objSyouhin1Record
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objSyouhin1Record = value
        End Set
    End Property
#End Region

#Region "商品コード２の邸別請求レコードDictionary"
    ''' <summary>
    ''' 商品コード２の邸別請求レコードDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecordのDictionaryである事</remarks>
    Private htbSyouhin2Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' 商品コード２の邸別請求Dictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード２の邸別請求レコードDictionary</returns>
    ''' <remarks>画面表示NOをKeyとしたTeibetuSeikyuuRecordのリストである事</remarks>
    Public Overrides Property Syouhin2Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            Return htbSyouhin2Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin2Records = value
        End Set
    End Property
#End Region

#Region "商品コード３の邸別請求レコードDictionary"
    ''' <summary>
    ''' 商品コード３の邸別請求レコードDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecordのDictionaryである事</remarks>
    Private htbSyouhin3Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' 商品コード３の邸別請求レコードDictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> 画面表示NOをKeyとした商品コード３の邸別請求レコードリスト</returns>
    ''' <remarks>TeibetuSeikyuuRecordのDictionaryである事</remarks>
    Public Overrides Property Syouhin3Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            Return htbSyouhin3Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin3Records = value
        End Set
    End Property
#End Region

#Region "商品コード４の邸別請求レコードDictionary"
    ''' <summary>
    ''' 商品コード４の邸別請求レコードDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecordのDictionaryである事</remarks>
    Private htbSyouhin4Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' 商品コード４の邸別請求レコードDictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> 画面表示NOをKeyとした商品コード４の邸別請求レコードリスト</returns>
    ''' <remarks>TeibetuSeikyuuRecordのDictionaryである事</remarks>
    Public Overrides Property Syouhin4Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            Return htbSyouhin4Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin4Records = value
        End Set
    End Property
#End Region

#Region "追加工事の邸別請求レコード"
    ''' <summary>
    ''' 追加工事の邸別請求レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private objTuikaKoujiRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 追加工事の邸別請求レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 追加工事の邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Overrides Property TuikaKoujiRecord() As TeibetuSeikyuuRecord
        Get
            Return objTuikaKoujiRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objTuikaKoujiRecord = value
        End Set
    End Property
#End Region

#Region "改良工事の邸別請求レコード"
    ''' <summary>
    ''' 改良工事の邸別請求レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private objKairyouKoujiRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 改良工事の邸別請求レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事の邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Overrides Property KairyouKoujiRecord() As TeibetuSeikyuuRecord
        Get
            Return objKairyouKoujiRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKairyouKoujiRecord = value
        End Set
    End Property
#End Region

#Region "調査報告書の邸別請求レコード(150:調査報告書再発行手数料)"
    ''' <summary>
    ''' 調査報告書の邸別請求レコード(150:調査報告書再発行手数料)
    ''' </summary>
    ''' <remarks></remarks>
    Private objTyousaHoukokusyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 調査報告書の邸別請求レコード(150:調査報告書再発行手数料)
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査報告書の邸別請求レコード(150:調査報告書再発行手数料)</returns>
    ''' <remarks></remarks>
    Public Overrides Property TyousaHoukokusyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objTyousaHoukokusyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objTyousaHoukokusyoRecord = value
        End Set
    End Property
#End Region

#Region "工事報告書の邸別請求レコード(160:工事報告書再発行手数料)"
    ''' <summary>
    ''' 工事報告書の邸別請求レコード(160:工事報告書再発行手数料)
    ''' </summary>
    ''' <remarks></remarks>
    Private objKoujiHoukokusyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 工事報告書の邸別請求レコード(160:工事報告書再発行手数料)
    ''' </summary>
    ''' <value></value>
    ''' <returns>工事報告書の邸別請求レコード(160:工事報告書再発行手数料)</returns>
    ''' <remarks></remarks>
    Public Overrides Property KoujiHoukokusyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objKoujiHoukokusyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKoujiHoukokusyoRecord = value
        End Set
    End Property
#End Region

#Region "保証書の邸別請求レコード(170:保証書再発行手数料)"
    ''' <summary>
    ''' 保証書の邸別請求レコード(170:保証書再発行手数料)
    ''' </summary>
    ''' <remarks></remarks>
    Private objHosyousyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 保証書の邸別請求レコード(170:保証書再発行手数料)
    ''' </summary>
    ''' <value></value>
    ''' <returns>保証書の邸別請求レコード(170:保証書再発行手数料)</returns>
    ''' <remarks></remarks>
    Public Overrides Property HosyousyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objHosyousyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objHosyousyoRecord = value
        End Set
    End Property
#End Region

#Region "解約払戻の邸別請求レコード(180:解約払戻)"
    ''' <summary>
    ''' 解約払戻の邸別請求レコード(180:解約払戻)
    ''' </summary>
    ''' <remarks></remarks>
    Private objKaiyakuHaraimodosiRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 解約払戻の邸別請求レコード(180:解約払戻)
    ''' </summary>
    ''' <value></value>
    ''' <returns>解約払戻の邸別請求レコード(180:解約払戻)</returns>
    ''' <remarks></remarks>
    Public Overrides Property KaiyakuHaraimodosiRecord() As TeibetuSeikyuuRecord
        Get
            Return objKaiyakuHaraimodosiRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKaiyakuHaraimodosiRecord = value
        End Set
    End Property
#End Region

#Region "上記以外の邸別請求レコードリスト"
    ''' <summary>
    ''' 上記以外の邸別請求レコードリスト
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecordのリストである事</remarks>
    Private arrOtherTeibetuSeikyuuRecords As List(Of TeibetuSeikyuuRecord)
    ''' <summary>
    ''' 上記以外の邸別請求レコードリスト
    ''' </summary>
    ''' <value></value>
    ''' <returns> 上記以外の邸別請求レコードリスト</returns>
    ''' <remarks>TeibetuSeikyuuRecordのリストである事</remarks>
    Public Overrides Property OtherTeibetuSeikyuuRecords() As List(Of TeibetuSeikyuuRecord)
        Get
            Return arrOtherTeibetuSeikyuuRecords
        End Get
        Set(ByVal value As List(Of TeibetuSeikyuuRecord))
            arrOtherTeibetuSeikyuuRecords = value
        End Set
    End Property
#End Region

#Region "邸別入金レコードDictionary"
    ''' <summary>
    ''' 邸別入金レコードDictionary
    ''' </summary>
    ''' <remarks></remarks>
    Private htbTeibetuNyuukinRecords As Dictionary(Of String, TeibetuNyuukinRecord)
    ''' <summary>
    ''' 邸別入金レコードDictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> 邸別入金レコードDictionary</returns>
    ''' <remarks>分類コードをKeyにTeibetuNyuukinRecordを保持します<br/>
    ''' 分類単位に入金額を保持してます。<br/>
    ''' 画面設定には以下を使用します<br/>
    ''' 100:商品１，２共通<br/>
    ''' 120:商品３<br/>
    ''' 130:追加工事<br/>
    ''' 140:改良工事<br/>
    ''' 150:調査報告書<br/>
    ''' 160:工事報告書<br/>
    ''' 170:保証書<br/>
    ''' 180:解約払戻</remarks>
    Public Overrides Property TeibetuNyuukinRecords() As Dictionary(Of String, TeibetuNyuukinRecord)
        Get
            Return htbTeibetuNyuukinRecords
        End Get
        Set(ByVal value As Dictionary(Of String, TeibetuNyuukinRecord))
            htbTeibetuNyuukinRecords = value
        End Set
    End Property
#End Region

#Region "邸別入金レコードList"
    ''' <summary>
    ''' 邸別入金レコードList
    ''' </summary>
    ''' <remarks></remarks>
    Private listTeibetuNyuukinUpdateRecords As List(Of TeibetuNyuukinUpdateRecord)

    ''' <summary>
    ''' 邸別入金レコードList
    ''' </summary>
    ''' <value>邸別入金レコードList</value>
    ''' <returns>邸別入金レコードList</returns>
    ''' <remarks></remarks>
    Public Overrides Property TeibetuNyuukinLists() As List(Of TeibetuNyuukinUpdateRecord)
        Get
            Return listTeibetuNyuukinUpdateRecords
        End Get
        Set(ByVal value As List(Of TeibetuNyuukinUpdateRecord))
            listTeibetuNyuukinUpdateRecords = value
        End Set
    End Property
#End Region

#Region "ReportIf 連携用項目"

#Region "ReportIF 設定用 構造名"
    Private strKouzouMeiIf As String = ""
    ''' <summary>
    ''' ReportIF 設定用 構造名
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF 設定用 構造名</returns>
    ''' <remarks></remarks>
    Public Overrides Property KouzouMeiIf() As String
        Get
            Return strKouzouMeiIf
        End Get
        Set(ByVal value As String)
            strKouzouMeiIf = value
        End Set
    End Property
#End Region

#Region "ReportIF 設定用 調査方法名"
    ''' <summary>
    ''' ReportIF 設定用 調査方法名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHouhouMeiIf As String = ""
    ''' <summary>
    ''' ReportIF 設定用 調査方法名
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF 設定用 調査方法名</returns>
    ''' <remarks></remarks>
    Public Overrides Property TysHouhouMeiIf() As String
        Get
            Return strTysHouhouMeiIf
        End Get
        Set(ByVal value As String)
            strTysHouhouMeiIf = value
        End Set
    End Property
#End Region

#Region "ReportIF 設定用 加盟店名"
    ''' <summary>
    ''' ReportIF 設定用 加盟店名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMeiIf As String = ""
    ''' <summary>
    ''' ReportIF 設定用 加盟店名
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF 設定用 加盟店名</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenMeiIf() As String
        Get
            Return strKameitenMeiIf
        End Get
        Set(ByVal value As String)
            strKameitenMeiIf = value
        End Set
    End Property
#End Region

#Region "ReportIF 設定用 加盟店TEL"
    ''' <summary>
    ''' ReportIF 設定用 加盟店TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenTelIf As String = ""
    ''' <summary>
    ''' ReportIF 設定用 加盟店TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF 設定用 加盟店TEL</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenTelIf() As String
        Get
            Return strKameitenTelIf
        End Get
        Set(ByVal value As String)
            strKameitenTelIf = value
        End Set
    End Property
#End Region

#Region "ReportIF 設定用 加盟店FAX"
    ''' <summary>
    ''' ReportIF 設定用 加盟店FAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenFaxIf As String = ""
    ''' <summary>
    ''' ReportIF 設定用 加盟店FAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF 設定用 加盟店FAX</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenFaxIf() As String
        Get
            Return strKameitenFaxIf
        End Get
        Set(ByVal value As String)
            strKameitenFaxIf = value
        End Set
    End Property
#End Region

#Region "ReportIF 設定用 加盟店MAIL"
    ''' <summary>
    ''' ReportIF 設定用 加盟店MAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMailIf As String = ""
    ''' <summary>
    ''' ReportIF 設定用 加盟店MAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF 設定用 加盟店MAIL</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenMailIf() As String
        Get
            Return strKameitenMailIf
        End Get
        Set(ByVal value As String)
            strKameitenMailIf = value
        End Set
    End Property
#End Region

#Region "ReportIF 設定用 調査報告書承認者"
    ''' <summary>
    ''' ReportIF 設定用 調査報告書承認者
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaHoukokusyoSyouninsyaIf As String = ""
    ''' <summary>
    ''' ReportIF 設定用 調査報告書承認者
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF 設定用 調査報告書承認者</returns>
    ''' <remarks></remarks>
    <TableMap("t_houkoku_syounin", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TyousaHoukokusyoSyouninsyaIf() As String
        Get
            Return strTyousaHoukokusyoSyouninsyaIf
        End Get
        Set(ByVal value As String)
            strTyousaHoukokusyoSyouninsyaIf = value
        End Set
    End Property
#End Region

#Region "ReportIF 設定用 進捗ステータス"
    ''' <summary>
    ''' ReportIF 設定用 進捗ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private strStatusIf As String = ""
    ''' <summary>
    ''' ReportIF 設定用 進捗ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF 設定用 進捗ステータス</returns>
    ''' <remarks></remarks>
    <TableMap("status", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overrides Property StatusIf() As String
        Get
            Return strStatusIf
        End Get
        Set(ByVal value As String)
            strStatusIf = value
        End Set
    End Property
#End Region

#Region "調査会社名"
    ''' <summary>
    ''' 調査会社名ﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaMeiIf As String = ""
    ''' <summary>
    ''' 調査会社名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社名</returns>
    ''' <remarks></remarks>
    Public Overrides Property TysKaisyaMeiIf() As String
        Get
            Return strTysKaisyaMeiIf
        End Get
        Set(ByVal value As String)
            strTysKaisyaMeiIf = value
        End Set
    End Property
#End Region

#End Region

#Region "置換工事 写真受理/写真コメント"

    ''' <summary>
    ''' 写真受理
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyasinJuri As String
    ''' <summary>
    ''' 写真受理
    ''' </summary>
    ''' <value></value>
    ''' <returns>写真受理</returns>
    ''' <remarks></remarks>
    <TableMap("syasin_jyuri", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property TikanKoujiSyasinJuri() As String
        Get
            Return strSyasinJuri
        End Get
        Set(ByVal value As String)
            strSyasinJuri = value
        End Set
    End Property

    ''' <summary>
    ''' 写真コメント
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyasinComment As String
    ''' <summary>
    ''' 写真コメント
    ''' </summary>
    ''' <value></value>
    ''' <returns>写真コメント</returns>
    ''' <remarks></remarks>
    <TableMap("syasin_comment", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overrides Property TikanKoujiSyasinComment() As String
        Get
            Return strSyasinComment
        End Get
        Set(ByVal value As String)
            strSyasinComment = value
        End Set
    End Property

#End Region

#Region "連棟物件対応"

#Region "処理件数"
    ''' <summary>
    ''' 処理件数ﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyoriKensuu As Integer = 0
    ''' <summary>
    ''' 処理件数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 処理件数</returns>
    ''' <remarks></remarks>
    Public Overrides Property SyoriKensuu() As Integer
        Get
            Return intSyoriKensuu
        End Get
        Set(ByVal value As Integer)
            intSyoriKensuu = value
        End Set
    End Property
#End Region

#Region "連棟物件数"
    ''' <summary>
    ''' 連棟物件数ﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intRentouBukkenSuu As Integer = 1
    ''' <summary>
    ''' 連棟物件数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 連棟物件数</returns>
    ''' <remarks></remarks>
    Public Overrides Property RentouBukkenSuu() As Integer
        Get
            Return intRentouBukkenSuu
        End Get
        Set(ByVal value As Integer)
            intRentouBukkenSuu = value
        End Set
    End Property
#End Region

#End Region

#Region "予約済FLG"
    ''' <summary>
    ''' 予約済FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoyakuZumiFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 予約済FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>予約済FLG</returns>
    ''' <remarks></remarks>
    <TableMap("yoyaku_zumi_flg", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YoyakuZumiFlg() As Integer
        Get
            Return intYoyakuZumiFlg
        End Get
        Set(ByVal value As Integer)
            intYoyakuZumiFlg = value
        End Set
    End Property
#End Region

#Region "案内図"
    ''' <summary>
    ''' 案内図
    ''' </summary>
    ''' <remarks></remarks>
    Private intAnnaiZu As Integer = Integer.MinValue
    ''' <summary>
    ''' 案内図
    ''' </summary>
    ''' <value></value>
    ''' <returns>案内図</returns>
    ''' <remarks></remarks>
    <TableMap("annaizu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property AnnaiZu() As Integer
        Get
            Return intAnnaiZu
        End Get
        Set(ByVal value As Integer)
            intAnnaiZu = value
        End Set
    End Property
#End Region

#Region "配置図"
    ''' <summary>
    ''' 配置図
    ''' </summary>
    ''' <remarks></remarks>
    Private intHaitiZu As Integer = Integer.MinValue
    ''' <summary>
    ''' 配置図
    ''' </summary>
    ''' <value></value>
    ''' <returns>配置図</returns>
    ''' <remarks></remarks>
    <TableMap("haitizu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HaitiZu() As Integer
        Get
            Return intHaitiZu
        End Get
        Set(ByVal value As Integer)
            intHaitiZu = value
        End Set
    End Property
#End Region

#Region "各階平面図"
    ''' <summary>
    ''' 各階平面図
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakukaiHeimenZu As Integer = Integer.MinValue
    ''' <summary>
    ''' 各階平面図
    ''' </summary>
    ''' <value></value>
    ''' <returns>各階平面図</returns>
    ''' <remarks></remarks>
    <TableMap("kakukai_heimenzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KakukaiHeimenZu() As Integer
        Get
            Return intKakukaiHeimenZu
        End Get
        Set(ByVal value As Integer)
            intKakukaiHeimenZu = value
        End Set
    End Property
#End Region

#Region "基礎伏図"
    ''' <summary>
    ''' 基礎伏図
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsHuseZu As Integer = Integer.MinValue
    ''' <summary>
    ''' 基礎伏図
    ''' </summary>
    ''' <value></value>
    ''' <returns>基礎伏図</returns>
    ''' <remarks></remarks>
    <TableMap("ks_husezu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsHuseZu() As Integer
        Get
            Return intKsHuseZu
        End Get
        Set(ByVal value As Integer)
            intKsHuseZu = value
        End Set
    End Property
#End Region

#Region "基礎断面図"
    ''' <summary>
    ''' 基礎断面図
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsDanmenZu As Integer = Integer.MinValue
    ''' <summary>
    ''' 基礎断面図
    ''' </summary>
    ''' <value></value>
    ''' <returns>基礎断面図</returns>
    ''' <remarks></remarks>
    <TableMap("ks_danmenzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsDanmenZu() As Integer
        Get
            Return intKsDanmenZu
        End Get
        Set(ByVal value As Integer)
            intKsDanmenZu = value
        End Set
    End Property
#End Region

#Region "造成計画図"
    ''' <summary>
    ''' 造成計画図
    ''' </summary>
    ''' <remarks></remarks>
    Private intZouseiKeikakuZu As Integer = Integer.MinValue
    ''' <summary>
    ''' 造成計画図
    ''' </summary>
    ''' <value></value>
    ''' <returns>造成計画図</returns>
    ''' <remarks></remarks>
    <TableMap("zousei_keikakuzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property ZouseiKeikakuZu() As Integer
        Get
            Return intZouseiKeikakuZu
        End Get
        Set(ByVal value As Integer)
            intZouseiKeikakuZu = value
        End Set
    End Property
#End Region

#Region "基礎着工予定日FROM"
    ''' <summary>
    ''' 基礎着工予定日FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsTyakkouYoteiFromDate As DateTime
    ''' <summary>
    ''' 基礎着工予定日FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎着工予定日FROM</returns>
    ''' <remarks></remarks>
    <TableMap("ks_tyakkou_yotei_from_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KsTyakkouYoteiFromDate() As DateTime
        Get
            Return dateKsTyakkouYoteiFromDate
        End Get
        Set(ByVal value As DateTime)
            dateKsTyakkouYoteiFromDate = value
        End Set
    End Property
#End Region

#Region "基礎着工予定日TO"
    ''' <summary>
    ''' 基礎着工予定日TO
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsTyakkouYoteiToDate As DateTime
    ''' <summary>
    ''' 基礎着工予定日TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎着工予定日TO</returns>
    ''' <remarks></remarks>
    <TableMap("ks_tyakkou_yotei_to_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KsTyakkouYoteiToDate() As DateTime
        Get
            Return dateKsTyakkouYoteiToDate
        End Get
        Set(ByVal value As DateTime)
            dateKsTyakkouYoteiToDate = value
        End Set
    End Property
#End Region

#Region "新規登録元区分"
    ''' <summary>
    ''' 新規登録元区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intSinkiTourokuMotoKbn As Integer = Integer.MinValue
    ''' <summary>
    ''' 新規登録元区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>新規登録元区分</returns>
    ''' <remarks></remarks>
    <TableMap("sinki_touroku_moto_kbn", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SinkiTourokuMotoKbn() As Integer
        Get
            Return intSinkiTourokuMotoKbn
        End Get
        Set(ByVal value As Integer)
            intSinkiTourokuMotoKbn = value
        End Set
    End Property
#End Region

#Region "工事判定結果FLG"
    ''' <summary>
    ''' 工事判定結果FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojHanteiKekkaFlg As Integer = 0
    ''' <summary>
    ''' 工事判定結果FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>工事判定結果FLG</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hantei_kekka_flg", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojHanteiKekkaFlg() As Integer
        Get
            Return intKojHanteiKekkaFlg
        End Get
        Set(ByVal value As Integer)
            intKojHanteiKekkaFlg = value
        End Set
    End Property
#End Region

#Region "施主名有無"
    ''' <summary>
    ''' 施主名有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intSesyuMeiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 施主名有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 施主名有無</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SesyuMeiUmu() As Integer
        Get
            Return intSesyuMeiUmu
        End Get
        Set(ByVal value As Integer)
            intSesyuMeiUmu = value
        End Set
    End Property
#End Region

End Class