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
Public Class JibanRecordBase

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
    Public Overridable Property Kbn() As String
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
    Public Overridable Property HosyousyoNo() As String
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
    <TableMap("data_haki_syubetu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property DataHakiSyubetu() As Integer
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
    <TableMap("data_haki_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property DataHakiDate() As DateTime
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
    <TableMap("sesyu_mei", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Overridable Property SesyuMei() As String
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
    <TableMap("jutyuu_bukken_mei", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Overridable Property JyutyuuBukkenMei() As String
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
    <TableMap("bukken_jyuusyo1", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overridable Property BukkenJyuusyo1() As String
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
    <TableMap("bukken_jyuusyo2", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overridable Property BukkenJyuusyo2() As String
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
    <TableMap("bukken_jyuusyo3", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=54)> _
    Public Overridable Property BukkenJyuusyo3() As String
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
    <TableMap("bunjou_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property BunjouCd() As Integer
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
    <TableMap("bukken_nayose_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=11)> _
    Public Overridable Property BukkenNayoseCd() As String
        Get
            Return strBukkenNayoseCd
        End Get
        Set(ByVal value As String)
            strBukkenNayoseCd = value
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
    <TableMap("kameiten_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
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
    ''' <returns>系列ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("keiretu_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property KeiretuCd() As String
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
    ''' 営業所ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoCd As String
    ''' <summary>
    ''' 営業所ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns>営業所ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("eigyousyo_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property EigyousyoCd() As String
        Get
            Return strEigyousyoCd
        End Get
        Set(ByVal value As String)
            strEigyousyoCd = value
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
    <TableMap("syouhin_kbn", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SyouhinKbn() As Integer
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
    <TableMap("bikou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property Bikou() As String
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
    <TableMap("bikou2", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=512)> _
    Public Overridable Property Bikou2() As String
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
    Private dateIraiDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 依頼日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼日</returns>
    ''' <remarks></remarks>
    <TableMap("irai_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property IraiDate() As DateTime
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
    <TableMap("irai_tantousya_mei", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property IraiTantousyaMei() As String
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
    <TableMap("keiyaku_no", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overridable Property KeiyakuNo() As String
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
    Public Overridable Property ThKasiUmu() As Integer
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
    <TableMap("kaisou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Kaisou() As Integer
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
    <TableMap("sintiku_tatekae", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SintikuTatekae() As Integer
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
    <TableMap("kouzou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Kouzou() As Integer
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
    <TableMap("kouzou_memo", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overridable Property KouzouMemo() As String
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
    <TableMap("syako", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Syako() As Integer
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
    <TableMap("negiri_hukasa", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overridable Property NegiriHukasa() As Decimal
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
    <TableMap("yotei_morituti_atusa", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overridable Property YoteiMoritutiAtusa() As Decimal
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
    <TableMap("yotei_ks", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YoteiKs() As Integer
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
    <TableMap("yotei_ks_memo", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overridable Property YoteiKsMemo() As String
        Get
            Return strYoteiKsMemo
        End Get
        Set(ByVal value As String)
            strYoteiKsMemo = value
        End Set
    End Property
#End Region

#Region "調査会社ｺｰﾄﾞ"
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
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "調査会社事業所ｺｰﾄﾞ"
    ''' <summary>
    ''' 調査会社事業所ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' 調査会社事業所ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社事業所ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property TysKaisyaJigyousyoCd() As String
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
    <TableMap("tys_houhou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TysHouhou() As Integer
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
    <TableMap("tys_gaiyou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TysGaiyou() As Integer
        Get
            Return intTysGaiyou
        End Get
        Set(ByVal value As Integer)
            intTysGaiyou = value
        End Set
    End Property
#End Region

#Region "商品ｺｰﾄﾞ1"
    ''' <summary>
    ''' 商品ｺｰﾄﾞ1
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd1 As String
    ''' <summary>
    ''' 商品ｺｰﾄﾞ1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overridable Property SyouhinCd1() As String
        Get
            Return strSyouhinCd1
        End Get
        Set(ByVal value As String)
            strSyouhinCd1 = value
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
    Public Overridable Property FcBuilderHanbaiGaku() As Integer
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
    Private dateTysKibouDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 調査希望日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査希望日</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property TysKibouDate() As DateTime
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
    <TableMap("tys_kibou_jikan", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=26)> _
    Public Overridable Property TysKibouJikan() As String
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
    <TableMap("tatiai_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TatiaiUmu() As Integer
        Get
            Return intTatiaiUmu
        End Get
        Set(ByVal value As Integer)
            intTatiaiUmu = value
        End Set
    End Property
#End Region

#Region "立会者ｺｰﾄﾞ"
    ''' <summary>
    ''' 立会者ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatiaisyaCd As Integer
    ''' <summary>
    ''' 立会者ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 立会者ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TatiaisyaCd() As Integer
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
    <TableMap("tenpu_heimenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TenpuHeimenzu() As Integer
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
    Public Overridable Property TenpuRitumenzu() As Integer
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
    Public Overridable Property TenpuKsHusezu() As Integer
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
    Public Overridable Property TenpuDanmenzu() As Integer
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
    Public Overridable Property TenpuKukeizu() As Integer
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
    <TableMap("syoudakusyo_tys_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property SyoudakusyoTysDate() As DateTime
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
    Public Overridable Property TysJissiDate() As DateTime
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
    <TableMap("dositu")> _
    Public Overridable Property Dositu() As String
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
    <TableMap("kyoyou_sijiryoku")> _
    Public Overridable Property KyoyouSijiryoku() As String
        Get
            Return strKyoyouSijiryoku
        End Get
        Set(ByVal value As String)
            strKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "判定ｺｰﾄﾞ1"
    ''' <summary>
    ''' 判定ｺｰﾄﾞ1
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiCd1 As Integer
    ''' <summary>
    ''' 判定ｺｰﾄﾞ1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 判定ｺｰﾄﾞ1</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_cd1", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HanteiCd1() As Integer
        Get
            Return intHanteiCd1
        End Get
        Set(ByVal value As Integer)
            intHanteiCd1 = value
        End Set
    End Property
#End Region

#Region "判定ｺｰﾄﾞ2"
    ''' <summary>
    ''' 判定ｺｰﾄﾞ2
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiCd2 As Integer
    ''' <summary>
    ''' 判定ｺｰﾄﾞ2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 判定ｺｰﾄﾞ2</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_cd2", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HanteiCd2() As Integer
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
    Public Overridable Property HanteiSetuzokuMoji() As Integer
        Get
            Return intHanteiSetuzokuMoji
        End Get
        Set(ByVal value As Integer)
            intHanteiSetuzokuMoji = value
        End Set
    End Property
#End Region

#Region "担当者ｺｰﾄﾞ"
    ''' <summary>
    ''' 担当者ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTantousyaCd As Integer
    ''' <summary>
    ''' 担当者ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当者ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TantousyaCd() As Integer
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
    Public Overridable Property TantousyaMei() As String
        Get
            Return strTantousyaMei
        End Get
        Set(ByVal value As String)
            strTantousyaMei = value
        End Set
    End Property
#End Region

#Region "承認者ｺｰﾄﾞ"
    ''' <summary>
    ''' 承認者ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouninsyaCd As Integer
    ''' <summary>
    ''' 承認者ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 承認者ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("syouninsya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SyouninsyaCd() As Integer
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
    Public Overridable Property SyouninsyaMei() As String
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
    Public Overridable Property KeikakusyoSakuseiDate() As DateTime
        Get
            Return dateKeikakusyoSakuseiDate
        End Get
        Set(ByVal value As DateTime)
            dateKeikakusyoSakuseiDate = value
        End Set
    End Property
#End Region

#Region "基礎断面ｺｰﾄﾞ"
    ''' <summary>
    ''' 基礎断面ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsDanmenCd As Integer
    ''' <summary>
    ''' 基礎断面ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎断面ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("ks_danmen_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KsDanmenCd() As Integer
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
    Public Overridable Property DanmenzuSetumei() As String
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
    Public Overridable Property Kousatu() As String
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
    Public Overridable Property KsHkksUmu() As Integer
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
    Public Overridable Property KsKojKanryHkksTykDate() As DateTime
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
    <TableMap("hosyousyo_hak_jyky", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HosyousyoHakJyky() As Integer
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
    <TableMap("hosyousyo_hak_jyky_settei_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HosyousyoHakJykySetteiDate() As DateTime
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
    Public Overridable Property HosyousyoHakDate() As DateTime
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
    Public Overridable Property InsatuHosyousyoHakDate() As DateTime
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
    <TableMap("hosyou_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HosyouUmu() As Integer
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
    Public Overridable Property HosyouKaisiDate() As DateTime
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
    <TableMap("hosyou_kikan", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HosyouKikan() As Integer
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
    Public Overridable Property HosyouNasiRiyuu() As String
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
    <TableMap("hosyou_syouhin_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HosyouSyouhinUmu() As Integer
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
    Public Overridable Property HokenKaisya() As Integer
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
    Public Overridable Property HokenSinseiTuki() As DateTime
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
    Public Overridable Property HosyousyoSaihakDate() As DateTime
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
    Public Overridable Property TysHkksUmu() As Integer
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
    Public Overridable Property TysHkksJyuriSyousai() As String
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
    Public Overridable Property TysHkksJyuriDate() As DateTime
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
    Public Overridable Property TysHkksHakDate() As DateTime
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
    Public Overridable Property TysHkksSaihakDate() As DateTime
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
    Public Overridable Property KojHkksUmu() As Integer
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
    Public Overridable Property KojHkksJuriSyousai() As String
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
    Public Overridable Property KojHkksJuriDate() As DateTime
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
    Public Overridable Property KojHkksHassouDate() As DateTime
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
    Public Overridable Property KojHkksSaihakDate() As DateTime
        Get
            Return dateKojHkksSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksSaihakDate = value
        End Set
    End Property
#End Region

#Region "工事会社ｺｰﾄﾞ"
    ''' <summary>
    ''' 工事会社ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaCd As String
    ''' <summary>
    ''' 工事会社ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_cd")> _
    Public Overridable Property KojGaisyaCd() As String
        Get
            Return strKojGaisyaCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaCd = value
        End Set
    End Property
#End Region

#Region "工事会社事業所ｺｰﾄﾞ"
    ''' <summary>
    ''' 工事会社事業所ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaJigyousyoCd As String
    ''' <summary>
    ''' 工事会社事業所ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社事業所ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_jigyousyo_cd")> _
    Public Overridable Property KojGaisyaJigyousyoCd() As String
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
    Public Overridable Property KairyKojSyubetu() As Integer
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
    Public Overridable Property KairyKojKanryYoteiDate() As DateTime
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
    Public Overridable Property KairyKojDate() As DateTime
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
    Public Overridable Property KairyKojSokuhouTykDate() As DateTime
        Get
            Return dateKairyKojSokuhouTykDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojSokuhouTykDate = value
        End Set
    End Property
#End Region

#Region "追加工事会社ｺｰﾄﾞ"
    ''' <summary>
    ''' 追加工事会社ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTKojKaisyaCd As String
    ''' <summary>
    ''' 追加工事会社ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 追加工事会社ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_cd")> _
    Public Overridable Property TKojKaisyaCd() As String
        Get
            Return strTKojKaisyaCd
        End Get
        Set(ByVal value As String)
            strTKojKaisyaCd = value
        End Set
    End Property
#End Region

#Region "追加工事会社事業所ｺｰﾄﾞ"
    ''' <summary>
    ''' 追加工事会社事業所ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTKojKaisyaJigyousyoCd As String
    ''' <summary>
    ''' 追加工事会社事業所ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 追加工事会社事業所ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_jigyousyo_cd")> _
    Public Overridable Property TKojKaisyaJigyousyoCd() As String
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
    Public Overridable Property TKojSyubetu() As Integer
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
    Public Overridable Property TKojKanryYoteiDate() As DateTime
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
    Public Overridable Property TKojDate() As DateTime
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
    Public Overridable Property TKojSokuhouTykDate() As DateTime
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
    Public Overridable Property TKojKaisyaSeikyuuUmu() As Integer
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
    Public Overridable Property TysKekkaAddDatetime() As DateTime
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
    Public Overridable Property TysKekkaUpdDatetime() As DateTime
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
    <TableMap("douji_irai_tousuu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property DoujiIraiTousuu() As Integer
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
    Public Overridable Property HokenSinseiKbn() As Integer
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
    <TableMap("kasi_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KasiUmu() As Integer
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
    <TableMap("koj_gaisya_seikyuu_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KojGaisyaSeikyuuUmu() As Integer
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
    Public Overridable Property HenkinSyoriFlg() As Integer
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
    Public Overridable Property HenkinSyoriDate() As DateTime
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
    Public Overridable Property KojTantousyaMei() As String
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
    <TableMap("keiyu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Keiyu() As Integer
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
    Public Overridable Property KojSiyouKakunin() As Integer
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
    Public Overridable Property KojSiyouKakuninDate() As DateTime
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
    Public Overridable Property HosyousyoHakIraisyoUmu() As Integer
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
    Public Overridable Property HosyousyoHakIraisyoTykDate() As DateTime
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
    <TableMap("sekkei_kyoyou_sijiryoku", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overridable Property SekkeiKyoyouSijiryoku() As Decimal
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
    <TableMap("irai_yotei_tousuu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property IraiYoteiTousuu() As Integer
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
    <TableMap("tatemono_youto_no", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TatemonoYoutoNo() As Integer
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
    <TableMap("kosuu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Kosuu() As Integer
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
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property Kousinsya() As String
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
    <TableMap("tys_renrakusaki_atesaki_mei", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=60)> _
    Public Overridable Property TysRenrakusakiAtesakiMei() As String
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
    <TableMap("tys_renrakusaki_tel", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overridable Property TysRenrakusakiTel() As String
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
    <TableMap("tys_renrakusaki_fax", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overridable Property TysRenrakusakiFax() As String
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
    <TableMap("tys_renrakusaki_mail", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=64)> _
    Public Overridable Property TysRenrakusakiMail() As String
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
    <TableMap("tys_renrakusaki_tantou_mei", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overridable Property TysRenrakusakiTantouMei() As String
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
    Public Overridable Property AddLoginUserId() As String
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
    Public Overridable Property AddDatetime() As DateTime
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
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property UpdLoginUserId() As String
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
    <TableMap("upd_datetime", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UpdDatetime() As DateTime
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
    Public Overridable Property Syouhin1Record() As TeibetuSeikyuuRecord
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
    Public Overridable Property Syouhin2Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
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
    Public Overridable Property Syouhin3Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
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
    Public Overridable Property Syouhin4Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
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
    Public Overridable Property TuikaKoujiRecord() As TeibetuSeikyuuRecord
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
    Public Overridable Property KairyouKoujiRecord() As TeibetuSeikyuuRecord
        Get
            Return objKairyouKoujiRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKairyouKoujiRecord = value
        End Set
    End Property
#End Region

#Region "調査報告書の邸別請求レコード"
    ''' <summary>
    ''' 調査報告書の邸別請求レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private objTyousaHoukokusyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 調査報告書の邸別請求レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査報告書の邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Overridable Property TyousaHoukokusyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objTyousaHoukokusyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objTyousaHoukokusyoRecord = value
        End Set
    End Property
#End Region

#Region "工事報告書の邸別請求レコード"
    ''' <summary>
    ''' 工事報告書の邸別請求レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private objKoujiHoukokusyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 工事報告書の邸別請求レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns>工事報告書の邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Overridable Property KoujiHoukokusyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objKoujiHoukokusyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKoujiHoukokusyoRecord = value
        End Set
    End Property
#End Region

#Region "保証書の邸別請求レコード"
    ''' <summary>
    ''' 保証書の邸別請求レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private objHosyousyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 保証書の邸別請求レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns>保証書の邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Overridable Property HosyousyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objHosyousyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objHosyousyoRecord = value
        End Set
    End Property
#End Region

#Region "解約払戻の邸別請求レコード"
    ''' <summary>
    ''' 解約払戻の邸別請求レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private objKaiyakuHaraimodosiRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 解約払戻の邸別請求レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns>解約払戻の邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Overridable Property KaiyakuHaraimodosiRecord() As TeibetuSeikyuuRecord
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
    Public Overridable Property OtherTeibetuSeikyuuRecords() As List(Of TeibetuSeikyuuRecord)
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
    Public Overridable Property TeibetuNyuukinRecords() As Dictionary(Of String, TeibetuNyuukinRecord)
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
    Public Overridable Property TeibetuNyuukinLists() As List(Of TeibetuNyuukinUpdateRecord)
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
    ''' <summary>
    ''' ReportIF 設定用 構造名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKouzouMeiIf As String = ""
    ''' <summary>
    ''' ReportIF 設定用 構造名
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF 設定用 構造名</returns>
    ''' <remarks></remarks>
    Public Overridable Property KouzouMeiIf() As String
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
    Public Overridable Property TysHouhouMeiIf() As String
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
    Public Overridable Property KameitenMeiIf() As String
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
    Public Overridable Property KameitenTelIf() As String
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
    Public Overridable Property KameitenFaxIf() As String
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
    Public Overridable Property KameitenMailIf() As String
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
    Public Overridable Property TyousaHoukokusyoSyouninsyaIf() As String
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
    Public Overridable Property StatusIf() As String
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
    Public Overridable Property TysKaisyaMeiIf() As String
        Get
            Return strTysKaisyaMeiIf
        End Get
        Set(ByVal value As String)
            strTysKaisyaMeiIf = value
        End Set
    End Property
#End Region

#End Region

#Region "入金額取得"
    ''' <summary>
    ''' 分類コードをキーに入金額を取得します<br/>
    ''' 存在しない場合は0を返します
    ''' </summary>
    ''' <param name="bunruiCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getNyuukinGaku(ByVal bunruiCd As String) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getNyuukinGaku", _
                                                    bunruiCd)

        Dim nyuukinGaku As Integer = 0

        If Not Me.TeibetuNyuukinRecords Is Nothing Then
            ' 商品３の入金額を取得
            If Me.TeibetuNyuukinRecords.ContainsKey(bunruiCd) = True Then
                Dim record As New TeibetuNyuukinRecord
                record = Me.TeibetuNyuukinRecords.Item(bunruiCd)
                nyuukinGaku = record.NyuukinGaku
            End If
        End If

        Return nyuukinGaku

    End Function

#End Region

#Region "税込金額取得"
    ''' <summary>
    ''' 分類コードをキーに税込金額を取得します<br/>
    ''' 分類コードを複数指定した場合、税込金額を加算します<br/>
    ''' 存在しない場合は0を返します
    ''' </summary>
    ''' <param name="bunruiCodes">分類コードの配列</param>
    ''' <returns>税込金額（分類コードの配列分加算）</returns>
    ''' <remarks>
    ''' 商品２は分類コード"110","115"の何れかで可<br/>
    ''' ※両方指定すると重複加算されます
    ''' </remarks>
    Public Function getZeikomiGaku(ByVal bunruiCodes() As String) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getZeikomiGaku", _
                                                    bunruiCodes)

        Dim zeikomi As Integer = 0
        Dim i As Integer

        For Each bunruiCd As String In bunruiCodes
            If bunruiCd = "100" Then
                ' 商品１
                If Not Me.Syouhin1Record Is Nothing Then
                    zeikomi = zeikomi + Me.Syouhin1Record.ZeikomiUriGaku
                End If

            ElseIf bunruiCd = "110" Or _
                   bunruiCd = "115" Then
                ' 商品２
                If Not Me.Syouhin2Records Is Nothing Then
                    For i = 1 To 4
                        If Me.Syouhin2Records.ContainsKey(i) = True Then
                            Dim syouhin2Rec As New TeibetuSeikyuuRecord
                            syouhin2Rec = Me.Syouhin2Records.Item(i)
                            zeikomi = zeikomi + syouhin2Rec.ZeikomiUriGaku
                        End If
                    Next
                End If
            ElseIf bunruiCd = "120" Then
                ' 商品３
                If Not Me.Syouhin3Records Is Nothing Then
                    For i = 1 To 9
                        If Me.Syouhin3Records.ContainsKey(i) = True Then
                            Dim syouhin3Rec As New TeibetuSeikyuuRecord
                            syouhin3Rec = Me.Syouhin3Records.Item(i)
                            zeikomi = zeikomi + syouhin3Rec.ZeikomiUriGaku
                        End If
                    Next
                End If
            ElseIf bunruiCd = "130" Then
                ' 改良工事
                If Not Me.KairyouKoujiRecord Is Nothing Then
                    zeikomi = zeikomi + Me.KairyouKoujiRecord.ZeikomiUriGaku
                End If
            ElseIf bunruiCd = "140" Then
                ' 追加工事
                If Not Me.TuikaKoujiRecord Is Nothing Then
                    zeikomi = zeikomi + Me.TuikaKoujiRecord.ZeikomiUriGaku
                End If
            ElseIf bunruiCd = "150" Then
                ' 調査報告書
                If Not Me.TyousaHoukokusyoRecord Is Nothing Then
                    zeikomi = zeikomi + Me.TyousaHoukokusyoRecord.ZeikomiUriGaku
                End If
            ElseIf bunruiCd = "160" Then
                ' 工事報告書
                If Not Me.KoujiHoukokusyoRecord Is Nothing Then
                    zeikomi = zeikomi + Me.KoujiHoukokusyoRecord.ZeikomiUriGaku
                End If
            ElseIf bunruiCd = "170" Then
                ' 保証書
                If Not Me.HosyousyoRecord Is Nothing Then
                    zeikomi = zeikomi + Me.HosyousyoRecord.ZeikomiUriGaku
                End If
            ElseIf bunruiCd = "180" Then
                ' 解約払戻
                If Not Me.KaiyakuHaraimodosiRecord Is Nothing Then
                    zeikomi = zeikomi + Me.KaiyakuHaraimodosiRecord.ZeikomiUriGaku
                End If
            End If
        Next

        Return zeikomi

    End Function

#End Region

#Region "商品１～３発注書金額合計取得"
    ''' <summary>
    ''' 商品１～３までの発注書金額をサマリーし返却します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinHattyusyoKingaku() As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getHattyusyoKingaku")

        Dim hattyuuGaku As Integer = 0

        If Not Me.Syouhin1Record Is Nothing Then
            '　商品１の発注書金額
            Dim work As Decimal = IIf(Me.Syouhin1Record.HattyuusyoGaku = _
                                      Integer.MinValue, 0, Me.Syouhin1Record.HattyuusyoGaku)
            hattyuuGaku = Fix(work * (1 + Me.Syouhin1Record.Zeiritu))
        End If

        If Not Me.Syouhin2Records Is Nothing Then
            Dim i As Integer
            For i = 1 To 4
                If Me.Syouhin2Records.ContainsKey(i) Then
                    '　商品２の発注書金額
                    Dim work As Decimal = IIf(Me.Syouhin2Records.Item(i).HattyuusyoGaku = _
                                              Integer.MinValue, _
                                              0, _
                                              Me.Syouhin2Records.Item(i).HattyuusyoGaku)
                    hattyuuGaku = hattyuuGaku + Fix(work * (1 + Me.Syouhin2Records.Item(i).Zeiritu))
                End If
            Next
        End If

        If Not Me.Syouhin3Records Is Nothing Then
            Dim i As Integer
            For i = 1 To 9
                If Me.Syouhin3Records.ContainsKey(i) Then
                    '　商品３の発注書金額
                    Dim work As Decimal = IIf(Me.Syouhin3Records.Item(i).HattyuusyoGaku = _
                                              Integer.MinValue, _
                                              0, _
                                              Me.Syouhin3Records.Item(i).HattyuusyoGaku)
                    hattyuuGaku = hattyuuGaku + Fix(work * (1 + Me.Syouhin3Records.Item(i).Zeiritu))
                End If
            Next
        End If

        Return hattyuuGaku

    End Function

#End Region

#Region "工事発注書金額合計取得"
    ''' <summary>
    ''' 工事の発注書金額をサマリーし返却します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKoujiHattyusyoKingaku() As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getHattyusyoKingaku")

        Dim hattyuuGaku As Integer = 0

        If Not Me.KairyouKoujiRecord Is Nothing Then
            '　改良工事の発注書金額
            Dim work As Decimal = IIf(Me.KairyouKoujiRecord.HattyuusyoGaku = _
                                      Integer.MinValue, 0, Me.KairyouKoujiRecord.HattyuusyoGaku)
            hattyuuGaku = Fix(work * (1 + Me.KairyouKoujiRecord.Zeiritu))
        End If

        If Not Me.TuikaKoujiRecord Is Nothing Then
            '　追加工事の発注書金額
            Dim work As Decimal = IIf(Me.TuikaKoujiRecord.HattyuusyoGaku = _
                                      Integer.MinValue, 0, Me.TuikaKoujiRecord.HattyuusyoGaku)
            hattyuuGaku = hattyuuGaku + Fix(work * (1 + Me.TuikaKoujiRecord.Zeiritu))
        End If

        Return hattyuuGaku

    End Function

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
    <TableMap("syasin_jyuri", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property TikanKoujiSyasinJuri() As String
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
    <TableMap("syasin_comment", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overridable Property TikanKoujiSyasinComment() As String
        Get
            Return strSyasinComment
        End Get
        Set(ByVal value As String)
            strSyasinComment = value
        End Set
    End Property

#End Region

#Region "日付マスタ"

#Region "保証書発行日"
    ''' <summary>
    ''' 保証書発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakkouDate As DateTime
    ''' <summary>
    ''' 保証書発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行日</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HosyousyoHakkouDate() As DateTime
        Get
            Return dateHosyousyoHakkouDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakkouDate = value
        End Set
    End Property
#End Region

#Region "報告書発送日"
    ''' <summary>
    ''' 報告書発送日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHoukokusyoHassouDate As DateTime
    ''' <summary>
    ''' 報告書発送日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 報告書発送日</returns>
    ''' <remarks></remarks>
    <TableMap("hkks_hassou_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HoukokusyoHassouDate() As DateTime
        Get
            Return dateHoukokusyoHassouDate
        End Get
        Set(ByVal value As DateTime)
            dateHoukokusyoHassouDate = value
        End Set
    End Property
#End Region

#End Region

#Region "★保証画面固有のプロパティ"

#Region "入金確認条件名"
    ''' <summary>
    ''' 入金確認条件名
    ''' </summary>
    ''' <remarks></remarks>
    Private _nyuukinKakuninJyoukenMei As String
    ''' <summary>
    ''' 入金確認条件名
    ''' </summary>
    ''' <value></value>
    ''' <returns>入金確認条件名</returns>
    ''' <remarks>加盟店マスタの入金確認条件をKeyに名称マスタの種別"05"より取得した値を設定します</remarks>
    <TableMap("nyuukin_kakunin_jyouken_mei")> _
    Public Property NyuukinKakuninJyoukenMei() As String
        Get
            Return _nyuukinKakuninJyoukenMei
        End Get
        Set(ByVal value As String)
            _nyuukinKakuninJyoukenMei = value
        End Set
    End Property
#End Region

#Region "報告書受理状況"
    ''' <summary>
    ''' 報告書受理状況
    ''' </summary>
    ''' <remarks></remarks>
    Private _hkksJyuriJyky As String
    ''' <summary>
    ''' 報告書受理状況
    ''' </summary>
    ''' <value></value>
    ''' <returns>報告書受理状況</returns>
    ''' <remarks>地盤テーブル．工事報告書有無＝報告書受理マスタ.報告書受理NOで報告書受理状況を設定します</remarks>
    <TableMap("hkks_jyuri_jyky")> _
    Public Property HkksJyuriJyky() As String
        Get
            Return _hkksJyuriJyky
        End Get
        Set(ByVal value As String)
            _hkksJyuriJyky = value
        End Set
    End Property
#End Region

#Region "改良工事種別名"
    ''' <summary>
    ''' 改良工事種別名
    ''' </summary>
    ''' <remarks></remarks>
    Private _kairyKojSyubetuMei As String
    ''' <summary>
    ''' 改良工事種別名
    ''' </summary>
    ''' <value></value>
    ''' <returns>改良工事種別名</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_syubetu_mei")> _
    Public Property KairyKojSyubetuMei() As String
        Get
            Return _kairyKojSyubetuMei
        End Get
        Set(ByVal value As String)
            _kairyKojSyubetuMei = value
        End Set
    End Property
#End Region

#Region "販促品請求先"
    ''' <summary>
    ''' 販促品請求先
    ''' </summary>
    ''' <remarks></remarks>
    Private _hansokuhinSeikyuusaki As String
    ''' <summary>
    ''' 販促品請求先
    ''' </summary>
    ''' <value></value>
    ''' <returns>販促品請求先</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuusaki")> _
    Public Property HansokuhinSeikyuusaki() As String
        Get
            Return _hansokuhinSeikyuusaki
        End Get
        Set(ByVal value As String)
            _hansokuhinSeikyuusaki = value
        End Set
    End Property
#End Region

#Region "調査請求先"
    ''' <summary>
    ''' 調査請求先
    ''' </summary>
    ''' <remarks></remarks>
    Private _tysSeikyuuSaki As String
    ''' <summary>
    ''' 調査請求先
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査請求先</returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki")> _
    Public Property TysSeikyuuSaki() As String
        Get
            Return _tysSeikyuuSaki
        End Get
        Set(ByVal value As String)
            _tysSeikyuuSaki = value
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
    <TableMap("fuho_syoumeisyo_flg", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property FuhoSyoumeisyoFlg() As Integer
        Get
            Return intFuhoSyoumeisyoFlg
        End Get
        Set(ByVal value As Integer)
            intFuhoSyoumeisyoFlg = value
        End Set
    End Property
#End Region

#Region "付保証明書発送日"
    ''' <summary>
    ''' 付保証明書発送日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateFuhoSyoumeisyoHassouDate As DateTime
    ''' <summary>
    ''' 付保証明書発送日
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("fuho_syoumeisyo_hassou_date")> _
    Public Property FuhoSyoumeisyoHassouDate() As DateTime
        Get
            Return dateFuhoSyoumeisyoHassouDate
        End Get
        Set(ByVal value As DateTime)
            dateFuhoSyoumeisyoHassouDate = value
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
    Public Overridable Property SyoriKensuu() As Integer
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
    Public Overridable Property RentouBukkenSuu() As Integer
        Get
            Return intRentouBukkenSuu
        End Get
        Set(ByVal value As Integer)
            intRentouBukkenSuu = value
        End Set
    End Property
#End Region

#End Region

#End Region

#Region "予約済FLG(予定書手動)"
    ''' <summary>
    ''' 予約済FLG(予定書手動)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoyakuZumiFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 予約済FLG(予定書手動)
    ''' </summary>
    ''' <value></value>
    ''' <returns>予約済FLG(予定書手動)</returns>
    ''' <remarks></remarks>
    <TableMap("yoyaku_zumi_flg", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YoyakuZumiFlg() As Integer
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
    <TableMap("annaizu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property AnnaiZu() As Integer
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
    <TableMap("haitizu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HaitiZu() As Integer
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
    <TableMap("kakukai_heimenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KakukaiHeimenZu() As Integer
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
    <TableMap("ks_husezu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KsHuseZu() As Integer
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
    <TableMap("ks_danmenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KsDanmenZu() As Integer
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
    <TableMap("zousei_keikakuzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property ZouseiKeikakuZu() As Integer
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
    <TableMap("ks_tyakkou_yotei_from_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property KsTyakkouYoteiFromDate() As DateTime
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
    <TableMap("ks_tyakkou_yotei_to_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property KsTyakkouYoteiToDate() As DateTime
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
    Public Overridable Property SinkiTourokuMotoKbn() As Integer
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
    Public Overridable Property KojHanteiKekkaFlg() As Integer
        Get
            Return intKojHanteiKekkaFlg
        End Get
        Set(ByVal value As Integer)
            intKojHanteiKekkaFlg = value
        End Set
    End Property
#End Region

#Region "連棟時の物件名寄コードのセット判断FLG"
    ''' <summary>
    ''' 連棟時の物件名寄コードのセット判断FLG
    ''' </summary>
    ''' <remarks>True:セットする,False：セットしない</remarks>
    Private blnBukkenNayoseCd As Boolean = False
    ''' <summary>
    ''' 連棟時の物件名寄コードのセット判断FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>連棟時の物件名寄コードのセット判断FLG</returns>
    ''' <remarks></remarks>
    Public Property BukkenNayoseCdFlg() As Boolean
        Get
            Return blnBukkenNayoseCd
        End Get
        Set(ByVal value As Boolean)
            blnBukkenNayoseCd = value
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
    <TableMap("sesyu_mei_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SesyuMeiUmu() As Integer
        Get
            Return intSesyuMeiUmu
        End Get
        Set(ByVal value As Integer)
            intSesyuMeiUmu = value
        End Set
    End Property
#End Region

#Region "★商品価格設定用プロパティ"

#Region "系列コード"
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhinKkk_KeiretuCd As String
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>系列コード</returns>
    ''' <remarks></remarks>
    Public Property SyouhinKkk_KeiretuCd() As String
        Get
            Return _syouhinKkk_KeiretuCd
        End Get
        Set(ByVal value As String)
            _syouhinKkk_KeiretuCd = value
        End Set
    End Property
#End Region

#Region "営業所コード"
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhinKkk_EigyousyoCd As String
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>営業所コード</returns>
    ''' <remarks></remarks>
    Public Property SyouhinKkk_EigyousyoCd() As String
        Get
            Return _syouhinKkk_EigyousyoCd
        End Get
        Set(ByVal value As String)
            _syouhinKkk_EigyousyoCd = value
        End Set
    End Property
#End Region

#Region "価格設定場所"
    ''' <summary>
    ''' 価格設定場所
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhinKkk_KkkSetteiBasyo As Integer = Integer.MinValue
    ''' <summary>
    ''' 価格設定場所
    ''' </summary>
    ''' <value></value>
    ''' <returns>価格設定場所</returns>
    ''' <remarks></remarks>
    Public Overridable Property SyouhinKkk_SetteiBasyo() As Integer
        Get
            Return intSyouhinKkk_KkkSetteiBasyo
        End Get
        Set(ByVal value As Integer)
            intSyouhinKkk_KkkSetteiBasyo = value
        End Set
    End Property
#End Region

#Region "直接請求/他請求"
    ''' <summary>
    ''' 直接請求/他請求
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhinKkk_SeikyuuSaki As String
    ''' <summary>
    ''' 直接請求/他請求
    ''' </summary>
    ''' <value></value>
    ''' <returns>直接請求/他請求</returns>
    ''' <remarks></remarks>
    Public Property SyouhinKkk_SeikyuuSaki() As String
        Get
            Return _syouhinKkk_SeikyuuSaki
        End Get
        Set(ByVal value As String)
            _syouhinKkk_SeikyuuSaki = value
        End Set
    End Property
#End Region

#Region "請求先タイプ"
    ''' <summary>
    ''' 請求先タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhinKkk_SeikyuuSakiType As String
    ''' <summary>
    ''' 請求先タイプ
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先タイプ</returns>
    ''' <remarks></remarks>
    Public Property SyouhinKkk_SeikyuuSakiType() As String
        Get
            Return _syouhinKkk_SeikyuuSakiType
        End Get
        Set(ByVal value As String)
            _syouhinKkk_SeikyuuSakiType = value
        End Set
    End Property
#End Region

#Region "エラーメッセージ"
    ''' <summary>
    ''' エラーメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhinKkk_ErrMsg As String = String.Empty
    ''' <summary>
    ''' エラーメッセージ
    ''' </summary>
    ''' <value></value>
    ''' <returns>エラーメッセージ</returns>
    ''' <remarks></remarks>
    Public Property SyouhinKkk_ErrMsg() As String
        Get
            Return _syouhinKkk_ErrMsg
        End Get
        Set(ByVal value As String)
            _syouhinKkk_ErrMsg = value
        End Set
    End Property
#End Region

#End Region

#Region "変更予定加盟店コード"
    ''' <summary>
    ''' 変更予定加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strHenkouYoteiKameitenCd As String = String.Empty
    ''' <summary>
    ''' 変更予定加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 変更予定加盟店コード</returns>
    ''' <remarks></remarks>
    <TableMap("henkou_yotei_kameiten_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property HenkouYoteiKameitenCd() As String
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
    Private strHosyouNasiRiyuuCd As String = String.Empty
    ''' <summary>
    ''' 保証なし理由コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証なし理由コード</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_nasi_riyuu_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property HosyouNasiRiyuuCd() As String
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
    <TableMap("hak_irai_uke_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HakIraiUkeDatetime() As DateTime
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
    Public Overridable Property HakIraiCanDatetime() As DateTime
        Get
            Return dateHakIraiCanDatetime
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiCanDatetime = value
        End Set
    End Property
#End Region

#Region "保証書発送日"
    ''' <summary>
    ''' 保証書発送日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHassouDate As DateTime
    ''' <summary>
    ''' 保証書発送日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発送日</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hassou_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HosyousyoHassouDate() As DateTime
        Get
            Return dateHosyousyoHassouDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHassouDate = value
        End Set
    End Property
#End Region


#Region "保証書発行方法"
    ''' <summary>
    ''' 保証書発行方法
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakHouhou As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証書発行方法
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書発行方法</returns>
    ''' <remarks>画面名称は「発行依頼方法」</remarks>
    <TableMap("hosyousyo_hak_houhou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HosyousyoHakHouhou() As Integer
        Get
            Return intHosyousyoHakHouhou
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakHouhou = value
        End Set
    End Property
#End Region


End Class