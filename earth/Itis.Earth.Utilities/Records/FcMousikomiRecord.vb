''' <summary>
''' FC申込データレコードクラス/FC申込検索画面、FC申込修正画面
''' </summary>
''' <remarks>FC申込データの格納時に使用します</remarks>
<TableClassMap("FcMousikomiIF")> _
Public Class FcMousikomiRecord

#Region "申込NO"
    ''' <summary>
    ''' 申込NO
    ''' </summary>
    ''' <remarks></remarks>
    Private lngMousikomiNo As Long = Long.MinValue
    ''' <summary>
    ''' 申込NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 申込NO</returns>
    ''' <remarks></remarks>
    <TableMap("mousikomi_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Overridable Property MousikomiNo() As Long
        Get
            Return lngMousikomiNo
        End Get
        Set(ByVal value As Long)
            lngMousikomiNo = value
        End Set
    End Property
#End Region

#Region "ステータス"
    ''' <summary>
    ''' ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private strStatus As String = String.Empty
    ''' <summary>
    ''' ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns> ステータス</returns>
    ''' <remarks></remarks>
    <TableMap("status", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=3)> _
    Public Overridable Property Status() As String
        Get
            Return strStatus
        End Get
        Set(ByVal value As String)
            strStatus = value
        End Set
    End Property
#End Region

#Region "WEB受付送信完了認証コード"
    ''' <summary>
    ''' WEB受付送信完了認証コード
    ''' </summary>
    ''' <remarks></remarks>
    Private lngReportSendCode As Long = Long.MinValue
    ''' <summary>
    ''' WEB受付送信完了認証コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> WEB受付送信完了認証コード</returns>
    ''' <remarks></remarks>
    <TableMap("report_send_code", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Overridable Property ReportSendCode() As Long
        Get
            Return lngReportSendCode
        End Get
        Set(ByVal value As Long)
            lngReportSendCode = value
        End Set
    End Property
#End Region

#Region "WEB受付受信完了認証コード"
    ''' <summary>
    ''' WEB受付受信完了認証コード
    ''' </summary>
    ''' <remarks></remarks>
    Private lngReportRecvCode As Long = Long.MinValue
    ''' <summary>
    ''' WEB受付受信完了認証コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> WEB受付受信完了認証コード</returns>
    ''' <remarks></remarks>
    <TableMap("report_recv_code", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Overridable Property ReportRecvCode() As Long
        Get
            Return lngReportRecvCode
        End Get
        Set(ByVal value As Long)
            lngReportRecvCode = value
        End Set
    End Property
#End Region

#Region "申込データ送信ステータス"
    ''' <summary>
    ''' 申込データ送信ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private strSendSts As String = String.Empty
    ''' <summary>
    ''' 申込データ送信ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns> 申込データ送信ステータス</returns>
    ''' <remarks></remarks>
    <TableMap("send_sts", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=2)> _
    Public Overridable Property SendSts() As String
        Get
            Return strSendSts
        End Get
        Set(ByVal value As String)
            strSendSts = value
        End Set
    End Property
#End Region

#Region "申込データ受信ステータス"
    ''' <summary>
    ''' 申込データ受信ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private strRecvSts As String = String.Empty
    ''' <summary>
    ''' 申込データ受信ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns> 申込データ受信ステータス</returns>
    ''' <remarks></remarks>
    <TableMap("recv_sts", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=2)> _
    Public Overridable Property RecvSts() As String
        Get
            Return strRecvSts
        End Get
        Set(ByVal value As String)
            strRecvSts = value
        End Set
    End Property
#End Region

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String = String.Empty
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分</returns>
    ''' <remarks></remarks>
    <TableMap("kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    Private strHosyousyoNo As String = String.Empty
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String = String.Empty
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String = String.Empty
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overridable Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
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
    <TableMap("irai_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property IraiDate() As DateTime
        Get
            Return dateIraiDate
        End Get
        Set(ByVal value As DateTime)
            dateIraiDate = value
        End Set
    End Property
#End Region

#Region "経由"
    ''' <summary>
    ''' 経由
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiyu As Integer = Integer.MinValue
    ''' <summary>
    ''' 経由
    ''' </summary>
    ''' <value></value>
    ''' <returns> 経由</returns>
    ''' <remarks></remarks>
    <TableMap("keiyu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Keiyu() As Integer
        Get
            Return intKeiyu
        End Get
        Set(ByVal value As Integer)
            intKeiyu = value
        End Set
    End Property
#End Region

#Region "契約NO"
    ''' <summary>
    ''' 契約NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiyakuNo As String = String.Empty
    ''' <summary>
    ''' 契約NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 契約NO</returns>
    ''' <remarks></remarks>
    <TableMap("keiyaku_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overridable Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
        End Set
    End Property
#End Region

#Region "調査連絡先_宛先"
    ''' <summary>
    ''' 調査連絡先_宛先
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiAtesakiMei As String = String.Empty
    ''' <summary>
    ''' 調査連絡先_宛先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_宛先</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_atesaki_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=60)> _
    Public Overridable Property TysRenrakusakiAtesakiMei() As String
        Get
            Return strTysRenrakusakiAtesakiMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiAtesakiMei = value
        End Set
    End Property
#End Region

#Region "調査連絡先_担当者"
    ''' <summary>
    ''' 調査連絡先_担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String = String.Empty
    ''' <summary>
    ''' 調査連絡先_担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_担当者</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tantou_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overridable Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

#Region "担当者連絡先TEL"
    ''' <summary>
    ''' 担当者連絡先TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private intTantousyaRenrakusakiTel As Integer = Integer.MinValue
    ''' <summary>
    ''' 担当者連絡先TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当者連絡先TEL</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_renrakusaki_tel", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TantousyaRenrakusakiTel() As Integer
        Get
            Return intTantousyaRenrakusakiTel
        End Get
        Set(ByVal value As Integer)
            intTantousyaRenrakusakiTel = value
        End Set
    End Property
#End Region

#Region "調査連絡先_TEL"
    ''' <summary>
    ''' 調査連絡先_TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTel As String = String.Empty
    ''' <summary>
    ''' 調査連絡先_TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_TEL</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tel", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
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
    Private strTysRenrakusakiFax As String = String.Empty
    ''' <summary>
    ''' 調査連絡先_FAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_FAX</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_fax", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
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
    Private strTysRenrakusakiMail As String = String.Empty
    ''' <summary>
    ''' 調査連絡先_MAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_MAIL</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_mail", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=64)> _
    Public Overridable Property TysRenrakusakiMail() As String
        Get
            Return strTysRenrakusakiMail
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiMail = value
        End Set
    End Property
#End Region

#Region "施主名"
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String = String.Empty
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 施主名</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Overridable Property SesyuMei() As String
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
    Private strBukkenJyuusyo1 As String = String.Empty
    ''' <summary>
    ''' 物件住所1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件住所1</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo1", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
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
    Private strBukkenJyuusyo2 As String = String.Empty
    ''' <summary>
    ''' 物件住所2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件住所2</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo2", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
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
    Private strBukkenJyuusyo3 As String = String.Empty
    ''' <summary>
    ''' 物件住所3
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件住所3</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo3", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=54)> _
    Public Overridable Property BukkenJyuusyo3() As String
        Get
            Return strBukkenJyuusyo3
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo3 = value
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
    <TableMap("tys_kibou_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property TysKibouDate() As DateTime
        Get
            Return dateTysKibouDate
        End Get
        Set(ByVal value As DateTime)
            dateTysKibouDate = value
        End Set
    End Property
#End Region

#Region "調査希望区分"
    ''' <summary>
    ''' 調査希望区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKibouKbn As String = String.Empty
    ''' <summary>
    ''' 調査希望区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査希望区分</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Overridable Property TysKibouKbn() As String
        Get
            Return strTysKibouKbn
        End Get
        Set(ByVal value As String)
            strTysKibouKbn = value
        End Set
    End Property
#End Region

#Region "調査開始希望時間"
    ''' <summary>
    ''' 調査開始希望時間
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisiKibouJikan As String = String.Empty
    ''' <summary>
    ''' 調査開始希望時間
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査開始希望時間</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisi_kibou_jikan", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overridable Property TysKaisiKibouJikan() As String
        Get
            Return strTysKaisiKibouJikan
        End Get
        Set(ByVal value As String)
            strTysKaisiKibouJikan = value
        End Set
    End Property
#End Region

#Region "基礎着工予定日FROM"
    ''' <summary>
    ''' 基礎着工予定日FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsTyakkouYoteiFromDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 基礎着工予定日FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎着工予定日FROM</returns>
    ''' <remarks></remarks>
    <TableMap("ks_tyakkou_yotei_from_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    Private dateKsTyakkouYoteiToDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 基礎着工予定日TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎着工予定日TO</returns>
    ''' <remarks></remarks>
    <TableMap("ks_tyakkou_yotei_to_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property KsTyakkouYoteiToDate() As DateTime
        Get
            Return dateKsTyakkouYoteiToDate
        End Get
        Set(ByVal value As DateTime)
            dateKsTyakkouYoteiToDate = value
        End Set
    End Property
#End Region

#Region "立会有無"
    ''' <summary>
    ''' 立会有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatiaiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 立会有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 立会有無</returns>
    ''' <remarks></remarks>
    <TableMap("tatiai_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TatiaiUmu() As Integer
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
    Private intTtsyCd As Integer = Integer.MinValue
    ''' <summary>
    ''' 立会者コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 立会者コード</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TtsyCd() As Integer
        Get
            Return intTtsyCd
        End Get
        Set(ByVal value As Integer)
            intTtsyCd = value
        End Set
    End Property
#End Region

#Region "立会者(その他補足)"
    ''' <summary>
    ''' 立会者(その他補足)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTtsySonotaHosoku As String = String.Empty
    ''' <summary>
    ''' 立会者(その他補足)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 立会者(その他補足)</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_sonota_hosoku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property TtsySonotaHosoku() As String
        Get
            Return strTtsySonotaHosoku
        End Get
        Set(ByVal value As String)
            strTtsySonotaHosoku = value
        End Set
    End Property
#End Region

#Region "構造"
    ''' <summary>
    ''' 構造
    ''' </summary>
    ''' <remarks></remarks>
    Private intKouzou As Integer = Integer.MinValue
    ''' <summary>
    ''' 構造
    ''' </summary>
    ''' <value></value>
    ''' <returns> 構造</returns>
    ''' <remarks></remarks>
    <TableMap("kouzou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    Private strKouzouMemo As String = String.Empty
    ''' <summary>
    ''' 構造MEMO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 構造MEMO</returns>
    ''' <remarks></remarks>
    <TableMap("kouzou_memo", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overridable Property KouzouMemo() As String
        Get
            Return strKouzouMemo
        End Get
        Set(ByVal value As String)
            strKouzouMemo = value
        End Set
    End Property
#End Region

#Region "新築建替"
    ''' <summary>
    ''' 新築建替
    ''' </summary>
    ''' <remarks></remarks>
    Private intSintikuTatekae As Integer = Integer.MinValue
    ''' <summary>
    ''' 新築建替
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新築建替</returns>
    ''' <remarks></remarks>
    <TableMap("sintiku_tatekae", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SintikuTatekae() As Integer
        Get
            Return intSintikuTatekae
        End Get
        Set(ByVal value As Integer)
            intSintikuTatekae = value
        End Set
    End Property
#End Region

#Region "階層(地上)"
    ''' <summary>
    ''' 階層(地上)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisouTijyou As Integer = Integer.MinValue
    ''' <summary>
    ''' 階層(地上)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 階層(地上)</returns>
    ''' <remarks></remarks>
    <TableMap("kaisou_tijyou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisouTijyou() As Integer
        Get
            Return intKaisouTijyou
        End Get
        Set(ByVal value As Integer)
            intKaisouTijyou = value
        End Set
    End Property
#End Region

#Region "建物用途NO"
    ''' <summary>
    ''' 建物用途NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTmytNo As Integer = Integer.MinValue
    ''' <summary>
    ''' 建物用途NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建物用途NO</returns>
    ''' <remarks></remarks>
    <TableMap("tatemono_youto_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TmytNo() As Integer
        Get
            Return intTmytNo
        End Get
        Set(ByVal value As Integer)
            intTmytNo = value
        End Set
    End Property
#End Region

#Region "設計許容支持力"
    ''' <summary>
    ''' 設計許容支持力
    ''' </summary>
    ''' <remarks></remarks>
    Private decSekkeiKyoyouSijiryoku As Decimal = Decimal.MinValue
    ''' <summary>
    ''' 設計許容支持力
    ''' </summary>
    ''' <value></value>
    ''' <returns> 設計許容支持力</returns>
    ''' <remarks></remarks>
    <TableMap("sekkei_kyoyou_sijiryoku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overridable Property SekkeiKyoyouSijiryoku() As Decimal
        Get
            Return decSekkeiKyoyouSijiryoku
        End Get
        Set(ByVal value As Decimal)
            decSekkeiKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "根切り深さ"
    ''' <summary>
    ''' 根切り深さ
    ''' </summary>
    ''' <remarks></remarks>
    Private decNegiriHukasa As Decimal = Decimal.MinValue
    ''' <summary>
    ''' 根切り深さ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 根切り深さ</returns>
    ''' <remarks></remarks>
    <TableMap("negiri_hukasa", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overridable Property NegiriHukasa() As Decimal
        Get
            Return decNegiriHukasa
        End Get
        Set(ByVal value As Decimal)
            decNegiriHukasa = value
        End Set
    End Property
#End Region

#Region "予定基礎"
    ''' <summary>
    ''' 予定基礎
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoteiKs As Integer = Integer.MinValue
    ''' <summary>
    ''' 予定基礎
    ''' </summary>
    ''' <value></value>
    ''' <returns> 予定基礎</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    Private strYoteiKsMemo As String = String.Empty
    ''' <summary>
    ''' 予定基礎MEMO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 予定基礎MEMO</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks_memo", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overridable Property YoteiKsMemo() As String
        Get
            Return strYoteiKsMemo
        End Get
        Set(ByVal value As String)
            strYoteiKsMemo = value
        End Set
    End Property
#End Region

#Region "備考"
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String = String.Empty
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <value></value>
    ''' <returns> 備考</returns>
    ''' <remarks></remarks>
    <TableMap("bikou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "同時依頼棟数"
    ''' <summary>
    ''' 同時依頼棟数
    ''' </summary>
    ''' <remarks></remarks>
    Private intDoujiIraiTousuu As Integer = Integer.MinValue
    ''' <summary>
    ''' 同時依頼棟数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 同時依頼棟数</returns>
    ''' <remarks></remarks>
    <TableMap("douji_irai_tousuu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property DoujiIraiTousuu() As Integer
        Get
            Return intDoujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuu = value
        End Set
    End Property
#End Region

#Region "予定盛土厚さ"
    ''' <summary>
    ''' 予定盛土厚さ
    ''' </summary>
    ''' <remarks></remarks>
    Private decYoteiMoritutiAtusa As Decimal = Decimal.MinValue
    ''' <summary>
    ''' 予定盛土厚さ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 予定盛土厚さ</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_morituti_atusa", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overridable Property YoteiMoritutiAtusa() As Decimal
        Get
            Return decYoteiMoritutiAtusa
        End Get
        Set(ByVal value As Decimal)
            decYoteiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "担当調査会社コード"
    ''' <summary>
    ''' 担当調査会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantouTysKaisyaCd As String = String.Empty
    ''' <summary>
    ''' 担当調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当調査会社コード</returns>
    ''' <remarks></remarks>
    <TableMap("tantou_tys_kaisya_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property TantouTysKaisyaCd() As String
        Get
            Return strTantouTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTantouTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "担当調査会社事業所コード"
    ''' <summary>
    ''' 担当調査会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantouTysKaisyaJigyousyoCd As String = String.Empty
    ''' <summary>
    ''' 担当調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 担当調査会社事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("tantou_tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property TantouTysKaisyaJigyousyoCd() As String
        Get
            Return strTantouTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTantouTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "申込調査会社コード"
    ''' <summary>
    ''' 申込調査会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strMousikomiTysKaisyaCd As String = String.Empty
    ''' <summary>
    ''' 申込調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 申込調査会社コード</returns>
    ''' <remarks></remarks>
    <TableMap("mousikomi_tys_kaisya_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property MousikomiTysKaisyaCd() As String
        Get
            Return strMousikomiTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strMousikomiTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "申込調査会社事業所コード"
    ''' <summary>
    ''' 申込調査会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strMousikomiTysKaisyaJigyousyoCd As String = String.Empty
    ''' <summary>
    ''' 申込調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 申込調査会社事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("mousikomi_tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property MousikomiTysKaisyaJigyousyoCd() As String
        Get
            Return strMousikomiTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strMousikomiTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "調査会社担当者"
    ''' <summary>
    ''' 調査会社担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaTantousya As String = String.Empty
    ''' <summary>
    ''' 調査会社担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社担当者</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd_tantousya", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property TysKaisyaTantousya() As String
        Get
            Return strTysKaisyaTantousya
        End Get
        Set(ByVal value As String)
            strTysKaisyaTantousya = value
        End Set
    End Property
#End Region

#Region "要注意情報"
    ''' <summary>
    ''' 要注意情報
    ''' </summary>
    ''' <remarks></remarks>
    Private strYouTyuuiJouhou As String = String.Empty
    ''' <summary>
    ''' 要注意情報
    ''' </summary>
    ''' <value></value>
    ''' <returns> 要注意情報</returns>
    ''' <remarks></remarks>
    <TableMap("you_tyuui_jouhou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property YouTyuuiJouhou() As String
        Get
            Return strYouTyuuiJouhou
        End Get
        Set(ByVal value As String)
            strYouTyuuiJouhou = value
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
    <TableMap("tys_houhou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TysHouhou() As Integer
        Get
            Return intTysHouhou
        End Get
        Set(ByVal value As Integer)
            intTysHouhou = value
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
    <TableMap("sesyu_mei_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SesyuMeiUmu() As Integer
        Get
            Return intSesyuMeiUmu
        End Get
        Set(ByVal value As Integer)
            intSesyuMeiUmu = value
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
    <TableMap("bukken_nayose_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=11)> _
    Public Overridable Property BukkenNayoseCd() As String
        Get
            Return strBukkenNayoseCd
        End Get
        Set(ByVal value As String)
            strBukkenNayoseCd = value
        End Set
    End Property
#End Region

#Region "更新者"
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String = String.Empty
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新者</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

#Region "登録日時"
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
        End Set
    End Property
#End Region

#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "登録ログインユーザーID"
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String = String.Empty
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "更新ログインユーザーID"
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String = String.Empty
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "★プロパティ"

#Region "加盟店マスタの項目"

#Region "加盟店名"
    ''' <summary>
    ''' 加盟店名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei As String = String.Empty
    ''' <summary>
    ''' 加盟店名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店名</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei1", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overridable Property KameitenMei() As String
        Get
            Return strKameitenMei
        End Get
        Set(ByVal value As String)
            strKameitenMei = value
        End Set
    End Property
#End Region

#End Region

#Region "商品マスタの項目"

#Region "商品名"
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinNm As String = String.Empty
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品名</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Overridable Property SyouhinNm() As String
        Get
            Return strSyouhinNm
        End Get
        Set(ByVal value As String)
            strSyouhinNm = value
        End Set
    End Property
#End Region

#End Region

#Region "調査方法マスタの項目"

#Region "調査方法名"
    ''' <summary>
    ''' 調査方法名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHouhouMei As String = String.Empty
    ''' <summary>
    ''' 調査方法名
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査方法名</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_mei")> _
    Public Overridable Property TysHouhouMei() As String
        Get
            Return strTysHouhouMei
        End Get
        Set(ByVal value As String)
            strTysHouhouMei = value
        End Set
    End Property
#End Region

#End Region

#Region "調査会社マスタの項目"

#Region "担当調査会社名"
    ''' <summary>
    ''' 担当調査会社名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantouTysKaiysaMei As String = String.Empty
    ''' <summary>
    ''' 担当調査会社名
    ''' </summary>
    ''' <value></value>
    ''' <returns>担当調査会社名</returns>
    ''' <remarks></remarks>
    <TableMap("tantou_tys_kaisya_mei")> _
    Public Overridable Property TantouTysKaisyaMei() As String
        Get
            Return strTantouTysKaiysaMei
        End Get
        Set(ByVal value As String)
            strTantouTysKaiysaMei = value
        End Set
    End Property
#End Region

#Region "申込調査会社名"
    ''' <summary>
    ''' 申込調査会社名
    ''' </summary>
    ''' <remarks></remarks>
    Private strMousikomiTysKaiysaMei As String = String.Empty
    ''' <summary>
    ''' 申込調査会社名
    ''' </summary>
    ''' <value></value>
    ''' <returns>申込調査会社名</returns>
    ''' <remarks></remarks>
    <TableMap("mousikomi_tys_kaisya_mei")> _
    Public Overridable Property MousikomiTysKaisyaMei() As String
        Get
            Return strMousikomiTysKaiysaMei
        End Get
        Set(ByVal value As String)
            strMousikomiTysKaiysaMei = value
        End Set
    End Property
#End Region

#End Region

#End Region

End Class