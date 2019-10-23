''' <summary>
''' 申込データレコードクラス/申込検索画面、申込修正画面
''' </summary>
''' <remarks>申込データの格納時に使用します</remarks>
<TableClassMap("MousikomiIF")> _
Public Class MousikomiRecord

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

#Region "物件名称フリガナ"
    ''' <summary>
    ''' 物件名称フリガナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenMeiKana As String = String.Empty
    ''' <summary>
    ''' 物件名称フリガナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件名称フリガナ</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_mei_kana", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overridable Property BukkenMeiKana() As String
        Get
            Return strBukkenMeiKana
        End Get
        Set(ByVal value As String)
            strBukkenMeiKana = value
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

#Region "調査場所(郵便番号)１"
    ''' <summary>
    ''' 調査場所(郵便番号)１
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysBasyoYuubin1 As String = String.Empty
    ''' <summary>
    ''' 調査場所(郵便番号)１
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査場所(郵便番号)１</returns>
    ''' <remarks></remarks>
    <TableMap("tys_basyo_yuubin_1", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overridable Property TysBasyoYuubin1() As String
        Get
            Return strTysBasyoYuubin1
        End Get
        Set(ByVal value As String)
            strTysBasyoYuubin1 = value
        End Set
    End Property
#End Region

#Region "調査場所(郵便番号)２"
    ''' <summary>
    ''' 調査場所(郵便番号)２
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysBasyoYuubin2 As String = String.Empty
    ''' <summary>
    ''' 調査場所(郵便番号)２
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査場所(郵便番号)２</returns>
    ''' <remarks></remarks>
    <TableMap("tys_basyo_yuubin_2", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Overridable Property TysBasyoYuubin2() As String
        Get
            Return strTysBasyoYuubin2
        End Get
        Set(ByVal value As String)
            strTysBasyoYuubin2 = value
        End Set
    End Property
#End Region

#Region "調査場所(都道府県)"
    ''' <summary>
    ''' 調査場所(都道府県)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysBasyoTodoufuken As String = String.Empty
    ''' <summary>
    ''' 調査場所(都道府県)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査場所(都道府県)</returns>
    ''' <remarks></remarks>
    <TableMap("tys_basyo_todoufuken", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property TysBasyoTodoufuken() As String
        Get
            Return strTysBasyoTodoufuken
        End Get
        Set(ByVal value As String)
            strTysBasyoTodoufuken = value
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

#Region "SDS希望"
    ''' <summary>
    ''' SDS希望
    ''' </summary>
    ''' <remarks></remarks>
    Private intSdsKibou As Integer = Integer.MinValue
    ''' <summary>
    ''' SDS希望
    ''' </summary>
    ''' <value></value>
    ''' <returns> SDS希望</returns>
    ''' <remarks></remarks>
    <TableMap("sds_kibou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SdsKibou() As Integer
        Get
            Return intSdsKibou
        End Get
        Set(ByVal value As Integer)
            intSdsKibou = value
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

#Region "延べ床面積"
    ''' <summary>
    ''' 延べ床面積
    ''' </summary>
    ''' <remarks></remarks>
    Private intNobeyukaMenseki As Integer = Integer.MinValue
    ''' <summary>
    ''' 延べ床面積
    ''' </summary>
    ''' <value></value>
    ''' <returns> 延べ床面積</returns>
    ''' <remarks></remarks>
    <TableMap("nobeyuka_menseki", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property NobeyukaMenseki() As Integer
        Get
            Return intNobeyukaMenseki
        End Get
        Set(ByVal value As Integer)
            intNobeyukaMenseki = value
        End Set
    End Property
#End Region

#Region "建築面積"
    ''' <summary>
    ''' 建築面積
    ''' </summary>
    ''' <remarks></remarks>
    Private intKentikuMenseki As Integer = Integer.MinValue
    ''' <summary>
    ''' 建築面積
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建築面積</returns>
    ''' <remarks></remarks>
    <TableMap("kentiku_menseki", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KentikuMenseki() As Integer
        Get
            Return intKentikuMenseki
        End Get
        Set(ByVal value As Integer)
            intKentikuMenseki = value
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

#Region "階層(地下)"
    ''' <summary>
    ''' 階層(地下)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisouTika As Integer = Integer.MinValue
    ''' <summary>
    ''' 階層(地下)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 階層(地下)</returns>
    ''' <remarks></remarks>
    <TableMap("kaisou_tika", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisouTika() As Integer
        Get
            Return intKaisouTika
        End Get
        Set(ByVal value As Integer)
            intKaisouTika = value
        End Set
    End Property
#End Region

#Region "車庫(地下車庫計画)"
    ''' <summary>
    ''' 車庫(地下車庫計画)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyako As Integer = Integer.MinValue
    ''' <summary>
    ''' 車庫(地下車庫計画)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 車庫(地下車庫計画)</returns>
    ''' <remarks></remarks>
    <TableMap("syako", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Syako() As Integer
        Get
            Return intSyako
        End Get
        Set(ByVal value As Integer)
            intSyako = value
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

#Region "建物用途(店舗用途)"
    ''' <summary>
    ''' 建物用途(店舗用途)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTmytNoTenpoYouto As String = String.Empty
    ''' <summary>
    ''' 建物用途(店舗用途)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建物用途(店舗用途)</returns>
    ''' <remarks></remarks>
    <TableMap("tmyt_no_tenpo_youto", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property TmytNoTenpoYouto() As String
        Get
            Return strTmytNoTenpoYouto
        End Get
        Set(ByVal value As String)
            strTmytNoTenpoYouto = value
        End Set
    End Property
#End Region

#Region "建物用途(その他用途)"
    ''' <summary>
    ''' 建物用途(その他用途)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTmytNoSonotaYouto As String = String.Empty
    ''' <summary>
    ''' 建物用途(その他用途)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建物用途(その他用途)</returns>
    ''' <remarks></remarks>
    <TableMap("tmyt_no_sonota_youto", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property TmytNoSonotaYouto() As String
        Get
            Return strTmytNoSonotaYouto
        End Get
        Set(ByVal value As String)
            strTmytNoSonotaYouto = value
        End Set
    End Property
#End Region

#Region "地域特性"
    ''' <summary>
    ''' 地域特性
    ''' </summary>
    ''' <remarks></remarks>
    Private strTiikiTokusei As String = String.Empty
    ''' <summary>
    ''' 地域特性
    ''' </summary>
    ''' <value></value>
    ''' <returns> 地域特性</returns>
    ''' <remarks></remarks>
    <TableMap("tiiki_tokusei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property TiikiTokusei() As String
        Get
            Return strTiikiTokusei
        End Get
        Set(ByVal value As String)
            strTiikiTokusei = value
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

#Region "布基礎ベースW"
    ''' <summary>
    ''' 布基礎ベースW
    ''' </summary>
    ''' <remarks></remarks>
    Private intNunoKsBaseW As Integer = Integer.MinValue
    ''' <summary>
    ''' 布基礎ベースW
    ''' </summary>
    ''' <value></value>
    ''' <returns> 布基礎ベースW</returns>
    ''' <remarks></remarks>
    <TableMap("nuno_ks_base_w", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property NunoKsBaseW() As Integer
        Get
            Return intNunoKsBaseW
        End Get
        Set(ByVal value As Integer)
            intNunoKsBaseW = value
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

#Region "予定基礎立ち上がり高さ"
    ''' <summary>
    ''' 予定基礎立ち上がり高さ
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoteiKsTatiagariTakasa As Integer = Integer.MinValue
    ''' <summary>
    ''' 予定基礎立ち上がり高さ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 予定基礎立ち上がり高さ</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks_tatiagari_takasa", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YoteiKsTatiagariTakasa() As Integer
        Get
            Return intYoteiKsTatiagariTakasa
        End Get
        Set(ByVal value As Integer)
            intYoteiKsTatiagariTakasa = value
        End Set
    End Property
#End Region

#Region "敷地道路幅"
    ''' <summary>
    ''' 敷地道路幅
    ''' </summary>
    ''' <remarks></remarks>
    Private decSktDouroHaba As Decimal = Decimal.MinValue
    ''' <summary>
    ''' 敷地道路幅
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地道路幅</returns>
    ''' <remarks></remarks>
    <TableMap("skt_douro_haba", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overridable Property SktDouroHaba() As Decimal
        Get
            Return decSktDouroHaba
        End Get
        Set(ByVal value As Decimal)
            decSktDouroHaba = value
        End Set
    End Property
#End Region

#Region "通行不可車両フラグ"
    ''' <summary>
    ''' 通行不可車両フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTuukouFukaSyaryouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 通行不可車両フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 通行不可車両フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("tuukou_fuka_syaryou_flg", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TuukouFukaSyaryouFlg() As Integer
        Get
            Return intTuukouFukaSyaryouFlg
        End Get
        Set(ByVal value As Integer)
            intTuukouFukaSyaryouFlg = value
        End Set
    End Property
#End Region

#Region "道路規制有無"
    ''' <summary>
    ''' 道路規制有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intDouroKiseiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 道路規制有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 道路規制有無</returns>
    ''' <remarks></remarks>
    <TableMap("douro_kisei_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property DouroKiseiUmu() As Integer
        Get
            Return intDouroKiseiUmu
        End Get
        Set(ByVal value As Integer)
            intDouroKiseiUmu = value
        End Set
    End Property
#End Region

#Region "高さ障害有無"
    ''' <summary>
    ''' 高さ障害有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intTakasaSyougaiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 高さ障害有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 高さ障害有無</returns>
    ''' <remarks></remarks>
    <TableMap("takasa_syougai_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TakasaSyougaiUmu() As Integer
        Get
            Return intTakasaSyougaiUmu
        End Get
        Set(ByVal value As Integer)
            intTakasaSyougaiUmu = value
        End Set
    End Property
#End Region

#Region "電線有無"
    ''' <summary>
    ''' 電線有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intDensenUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 電線有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 電線有無</returns>
    ''' <remarks></remarks>
    <TableMap("densen_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property DensenUmu() As Integer
        Get
            Return intDensenUmu
        End Get
        Set(ByVal value As Integer)
            intDensenUmu = value
        End Set
    End Property
#End Region

#Region "トンネル有無"
    ''' <summary>
    ''' トンネル有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intTonneruUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' トンネル有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> トンネル有無</returns>
    ''' <remarks></remarks>
    <TableMap("tonneru_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TonneruUmu() As Integer
        Get
            Return intTonneruUmu
        End Get
        Set(ByVal value As Integer)
            intTonneruUmu = value
        End Set
    End Property
#End Region

#Region "敷地内高低差"
    ''' <summary>
    ''' 敷地内高低差
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktnKouteisa As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地内高低差
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地内高低差</returns>
    ''' <remarks></remarks>
    <TableMap("sktn_kouteisa", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktnKouteisa() As Integer
        Get
            Return intSktnKouteisa
        End Get
        Set(ByVal value As Integer)
            intSktnKouteisa = value
        End Set
    End Property
#End Region

#Region "敷地内高低差(補足)"
    ''' <summary>
    ''' 敷地内高低差(補足)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSktnKouteisaHosoku As String = String.Empty
    ''' <summary>
    ''' 敷地内高低差(補足)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地内高低差(補足)</returns>
    ''' <remarks></remarks>
    <TableMap("sktn_kouteisa_hosoku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property SktnKouteisaHosoku() As String
        Get
            Return strSktnKouteisaHosoku
        End Get
        Set(ByVal value As String)
            strSktnKouteisaHosoku = value
        End Set
    End Property
#End Region

#Region "スロープ有無"
    ''' <summary>
    ''' スロープ有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intSlopeUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' スロープ有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> スロープ有無</returns>
    ''' <remarks></remarks>
    <TableMap("slope_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SlopeUmu() As Integer
        Get
            Return intSlopeUmu
        End Get
        Set(ByVal value As Integer)
            intSlopeUmu = value
        End Set
    End Property
#End Region

#Region "スロープ(補足)"
    ''' <summary>
    ''' スロープ(補足)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSlopeHosoku As String = String.Empty
    ''' <summary>
    ''' スロープ(補足)
    ''' </summary>
    ''' <value></value>
    ''' <returns> スロープ(補足)</returns>
    ''' <remarks></remarks>
    <TableMap("slope_hosoku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property SlopeHosoku() As String
        Get
            Return strSlopeHosoku
        End Get
        Set(ByVal value As String)
            strSlopeHosoku = value
        End Set
    End Property
#End Region

#Region "搬入条件(その他)"
    ''' <summary>
    ''' 搬入条件(その他)
    ''' </summary>
    ''' <remarks></remarks>
    Private strHannyuuJyknSonota As String = String.Empty
    ''' <summary>
    ''' 搬入条件(その他)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 搬入条件(その他)</returns>
    ''' <remarks></remarks>
    <TableMap("hannyuu_jykn_sonota", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property HannyuuJyknSonota() As String
        Get
            Return strHannyuuJyknSonota
        End Get
        Set(ByVal value As String)
            strHannyuuJyknSonota = value
        End Set
    End Property
#End Region

#Region "敷地前歴(宅地)"
    ''' <summary>
    ''' 敷地前歴(宅地)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiTakuti As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地前歴(宅地)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地前歴(宅地)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_takuti", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktZenrekiTakuti() As Integer
        Get
            Return intSktZenrekiTakuti
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiTakuti = value
        End Set
    End Property
#End Region

#Region "敷地前歴(田)"
    ''' <summary>
    ''' 敷地前歴(田)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiTa As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地前歴(田)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地前歴(田)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_ta", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktZenrekiTa() As Integer
        Get
            Return intSktZenrekiTa
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiTa = value
        End Set
    End Property
#End Region

#Region "敷地前歴(畑)"
    ''' <summary>
    ''' 敷地前歴(畑)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiHatake As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地前歴(畑)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地前歴(畑)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_hatake", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktZenrekiHatake() As Integer
        Get
            Return intSktZenrekiHatake
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiHatake = value
        End Set
    End Property
#End Region

#Region "敷地前歴(植樹畑)"
    ''' <summary>
    ''' 敷地前歴(植樹畑)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiSyokuju As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地前歴(植樹畑)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地前歴(植樹畑)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_syokuju", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktZenrekiSyokuju() As Integer
        Get
            Return intSktZenrekiSyokuju
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiSyokuju = value
        End Set
    End Property
#End Region

#Region "敷地前歴(雑木林)"
    ''' <summary>
    ''' 敷地前歴(雑木林)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiZouki As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地前歴(雑木林)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地前歴(雑木林)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_zouki", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktZenrekiZouki() As Integer
        Get
            Return intSktZenrekiZouki
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiZouki = value
        End Set
    End Property
#End Region

#Region "敷地前歴(駐車場)"
    ''' <summary>
    ''' 敷地前歴(駐車場)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiTyuusyajyou As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地前歴(駐車場)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地前歴(駐車場)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_tyuusyajyou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktZenrekiTyuusyajyou() As Integer
        Get
            Return intSktZenrekiTyuusyajyou
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiTyuusyajyou = value
        End Set
    End Property
#End Region

#Region "敷地前歴(干拓地)"
    ''' <summary>
    ''' 敷地前歴(干拓地)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiKantakuti As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地前歴(干拓地)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地前歴(干拓地)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_kantakuti", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktZenrekiKantakuti() As Integer
        Get
            Return intSktZenrekiKantakuti
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiKantakuti = value
        End Set
    End Property
#End Region

#Region "敷地前歴(工場跡)"
    ''' <summary>
    ''' 敷地前歴(工場跡)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiKoujyouato As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地前歴(工場跡)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地前歴(工場跡)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_koujyouato", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktZenrekiKoujyouato() As Integer
        Get
            Return intSktZenrekiKoujyouato
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiKoujyouato = value
        End Set
    End Property
#End Region

#Region "敷地前歴(その他)"
    ''' <summary>
    ''' 敷地前歴(その他)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiSonota As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地前歴(その他)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地前歴(その他)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_sonota", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktZenrekiSonota() As Integer
        Get
            Return intSktZenrekiSonota
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiSonota = value
        End Set
    End Property
#End Region

#Region "敷地前歴(その他補足)"
    ''' <summary>
    ''' 敷地前歴(その他補足)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSktZenrekiSonotaHosoku As String = String.Empty
    ''' <summary>
    ''' 敷地前歴(その他補足)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地前歴(その他補足)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_sonota_hosoku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property SktZenrekiSonotaHosoku() As String
        Get
            Return strSktZenrekiSonotaHosoku
        End Get
        Set(ByVal value As String)
            strSktZenrekiSonotaHosoku = value
        End Set
    End Property
#End Region

#Region "宅地造成機関"
    ''' <summary>
    ''' 宅地造成機関
    ''' </summary>
    ''' <remarks></remarks>
    Private intTakutiZouseiKikan As Integer = Integer.MinValue
    ''' <summary>
    ''' 宅地造成機関
    ''' </summary>
    ''' <value></value>
    ''' <returns> 宅地造成機関</returns>
    ''' <remarks></remarks>
    <TableMap("takuti_zousei_kikan", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TakutiZouseiKikan() As Integer
        Get
            Return intTakutiZouseiKikan
        End Get
        Set(ByVal value As Integer)
            intTakutiZouseiKikan = value
        End Set
    End Property
#End Region

#Region "造成月数"
    ''' <summary>
    ''' 造成月数
    ''' </summary>
    ''' <remarks></remarks>
    Private intZouseiGessuu As Integer = Integer.MinValue
    ''' <summary>
    ''' 造成月数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 造成月数</returns>
    ''' <remarks></remarks>
    <TableMap("zousei_gessuu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property ZouseiGessuu() As Integer
        Get
            Return intZouseiGessuu
        End Get
        Set(ByVal value As Integer)
            intZouseiGessuu = value
        End Set
    End Property
#End Region

#Region "切土盛土区分"
    ''' <summary>
    ''' 切土盛土区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intKiriMoriKbn As Integer = Integer.MinValue
    ''' <summary>
    ''' 切土盛土区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 切土盛土区分</returns>
    ''' <remarks></remarks>
    <TableMap("kiri_mori_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KiriMoriKbn() As Integer
        Get
            Return intKiriMoriKbn
        End Get
        Set(ByVal value As Integer)
            intKiriMoriKbn = value
        End Set
    End Property
#End Region

#Region "既存建物有無"
    ''' <summary>
    ''' 既存建物有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intKisonTatemonoUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 既存建物有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 既存建物有無</returns>
    ''' <remarks></remarks>
    <TableMap("kison_tatemono_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KisonTatemonoUmu() As Integer
        Get
            Return intKisonTatemonoUmu
        End Get
        Set(ByVal value As Integer)
            intKisonTatemonoUmu = value
        End Set
    End Property
#End Region

#Region "井戸有無"
    ''' <summary>
    ''' 井戸有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intIdoUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 井戸有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 井戸有無</returns>
    ''' <remarks></remarks>
    <TableMap("ido_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property IdoUmu() As Integer
        Get
            Return intIdoUmu
        End Get
        Set(ByVal value As Integer)
            intIdoUmu = value
        End Set
    End Property
#End Region

#Region "浄化槽現況有無"
    ''' <summary>
    ''' 浄化槽現況有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intJyoukaGenkyouUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 浄化槽現況有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 浄化槽現況有無</returns>
    ''' <remarks></remarks>
    <TableMap("jyouka_genkyou_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property JyoukaGenkyouUmu() As Integer
        Get
            Return intJyoukaGenkyouUmu
        End Get
        Set(ByVal value As Integer)
            intJyoukaGenkyouUmu = value
        End Set
    End Property
#End Region

#Region "浄化槽予定有無"
    ''' <summary>
    ''' 浄化槽予定有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intJyoukaYoteiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 浄化槽予定有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 浄化槽予定有無</returns>
    ''' <remarks></remarks>
    <TableMap("jyouka_yotei_umu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property JyoukaYoteiUmu() As Integer
        Get
            Return intJyoukaYoteiUmu
        End Get
        Set(ByVal value As Integer)
            intJyoukaYoteiUmu = value
        End Set
    End Property
#End Region

#Region "地縄"
    ''' <summary>
    ''' 地縄
    ''' </summary>
    ''' <remarks></remarks>
    Private intJinawa As Integer = Integer.MinValue
    ''' <summary>
    ''' 地縄
    ''' </summary>
    ''' <value></value>
    ''' <returns> 地縄</returns>
    ''' <remarks></remarks>
    <TableMap("jinawa", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Jinawa() As Integer
        Get
            Return intJinawa
        End Get
        Set(ByVal value As Integer)
            intJinawa = value
        End Set
    End Property
#End Region

#Region "境界杭"
    ''' <summary>
    ''' 境界杭
    ''' </summary>
    ''' <remarks></remarks>
    Private intKyoukaiKui As Integer = Integer.MinValue
    ''' <summary>
    ''' 境界杭
    ''' </summary>
    ''' <value></value>
    ''' <returns> 境界杭</returns>
    ''' <remarks></remarks>
    <TableMap("kyoukai_kui", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KyoukaiKui() As Integer
        Get
            Return intKyoukaiKui
        End Get
        Set(ByVal value As Integer)
            intKyoukaiKui = value
        End Set
    End Property
#End Region

#Region "敷地の現況(更地)"
    ''' <summary>
    ''' 敷地の現況(更地)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouSarati As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地の現況(更地)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地の現況(更地)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_sarati", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktGenkyouSarati() As Integer
        Get
            Return intSktGenkyouSarati
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouSarati = value
        End Set
    End Property
#End Region

#Region "敷地の現況(駐車場)"
    ''' <summary>
    ''' 敷地の現況(駐車場)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouTyuusyajyou As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地の現況(駐車場)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地の現況(駐車場)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_tyuusyajyou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktGenkyouTyuusyajyou() As Integer
        Get
            Return intSktGenkyouTyuusyajyou
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouTyuusyajyou = value
        End Set
    End Property
#End Region

#Region "敷地の現況(農耕地)"
    ''' <summary>
    ''' 敷地の現況(農耕地)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouNoukouti As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地の現況(農耕地)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地の現況(農耕地)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_noukouti", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktGenkyouNoukouti() As Integer
        Get
            Return intSktGenkyouNoukouti
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouNoukouti = value
        End Set
    End Property
#End Region

#Region "敷地の現況(その他)"
    ''' <summary>
    ''' 敷地の現況(その他)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouSonota As Integer = Integer.MinValue
    ''' <summary>
    ''' 敷地の現況(その他)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地の現況(その他)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_sonota", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SktGenkyouSonota() As Integer
        Get
            Return intSktGenkyouSonota
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouSonota = value
        End Set
    End Property
#End Region

#Region "敷地の現況(その他補足)"
    ''' <summary>
    ''' 敷地の現況(その他補足)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSktGenkyouSonotaHosoku As String = String.Empty
    ''' <summary>
    ''' 敷地の現況(その他補足)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 敷地の現況(その他補足)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_sonota_hosoku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property SktGenkyouSonotaHosoku() As String
        Get
            Return strSktGenkyouSonotaHosoku
        End Get
        Set(ByVal value As String)
            strSktGenkyouSonotaHosoku = value
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

#Region "盛土に関して(調査前実施済盛土厚)"
    ''' <summary>
    ''' 盛土に関して(調査前実施済盛土厚)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysMaeJissiZumiMoritutiAtusa As String = String.Empty
    ''' <summary>
    ''' 盛土に関して(調査前実施済盛土厚)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 盛土に関して(調査前実施済盛土厚)</returns>
    ''' <remarks></remarks>
    <TableMap("mrtt_tys_mae_jissi_zumi_morituti_atusa", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property TysMaeJissiZumiMoritutiAtusa() As String
        Get
            Return strTysMaeJissiZumiMoritutiAtusa
        End Get
        Set(ByVal value As String)
            strTysMaeJissiZumiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "盛土に関して(調査後予定盛土厚)"
    ''' <summary>
    ''' 盛土に関して(調査後予定盛土厚)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysAtoYoteiMoritutiAtusa As String = String.Empty
    ''' <summary>
    ''' 盛土に関して(調査後予定盛土厚)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 盛土に関して(調査後予定盛土厚)</returns>
    ''' <remarks></remarks>
    <TableMap("mrtt_tys_ato_yotei_morituti_atusa", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overridable Property TysAtoYoteiMoritutiAtusa() As String
        Get
            Return strTysAtoYoteiMoritutiAtusa
        End Get
        Set(ByVal value As String)
            strTysAtoYoteiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "擁壁(プレキャスト)"
    ''' <summary>
    ''' 擁壁(プレキャスト)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkPreCast As Integer = Integer.MinValue
    ''' <summary>
    ''' 擁壁(プレキャスト)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 擁壁(プレキャスト)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_pre_cast", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YhkPreCast() As Integer
        Get
            Return intYhkPreCast
        End Get
        Set(ByVal value As Integer)
            intYhkPreCast = value
        End Set
    End Property
#End Region

#Region "擁壁(現場打ち)"
    ''' <summary>
    ''' 擁壁(現場打ち)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkGenbaUti As Integer = Integer.MinValue
    ''' <summary>
    ''' 擁壁(現場打ち)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 擁壁(現場打ち)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_genba_uti", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YhkGenbaUti() As Integer
        Get
            Return intYhkGenbaUti
        End Get
        Set(ByVal value As Integer)
            intYhkGenbaUti = value
        End Set
    End Property
#End Region

#Region "擁壁(間知ブロック)"
    ''' <summary>
    ''' 擁壁(間知ブロック)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkKantiBlock As Integer = Integer.MinValue
    ''' <summary>
    ''' 擁壁(間知ブロック)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 擁壁(間知ブロック)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_kanti_block", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YhkKantiBlock() As Integer
        Get
            Return intYhkKantiBlock
        End Get
        Set(ByVal value As Integer)
            intYhkKantiBlock = value
        End Set
    End Property
#End Region

#Region "擁壁(CB)"
    ''' <summary>
    ''' 擁壁(CB)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkCb As Integer = Integer.MinValue
    ''' <summary>
    ''' 擁壁(CB)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 擁壁(CB)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_cb", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YhkCb() As Integer
        Get
            Return intYhkCb
        End Get
        Set(ByVal value As Integer)
            intYhkCb = value
        End Set
    End Property
#End Region

#Region "擁壁(既設済み)"
    ''' <summary>
    ''' 擁壁(既設済み)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkKisetuZumi As Integer = Integer.MinValue
    ''' <summary>
    ''' 擁壁(既設済み)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 擁壁(既設済み)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_kisetu_zumi", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YhkKisetuZumi() As Integer
        Get
            Return intYhkKisetuZumi
        End Get
        Set(ByVal value As Integer)
            intYhkKisetuZumi = value
        End Set
    End Property
#End Region

#Region "擁壁(新設予定)"
    ''' <summary>
    ''' 擁壁(新設予定)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkSinsetuYotei As Integer = Integer.MinValue
    ''' <summary>
    ''' 擁壁(新設予定)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 擁壁(新設予定)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_sinsetu_yotei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YhkSinsetuYotei() As Integer
        Get
            Return intYhkSinsetuYotei
        End Get
        Set(ByVal value As Integer)
            intYhkSinsetuYotei = value
        End Set
    End Property
#End Region

#Region "擁壁(設置経過年数)"
    ''' <summary>
    ''' 擁壁(設置経過年数)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkSettiKeikaNensuu As Integer = Integer.MinValue
    ''' <summary>
    ''' 擁壁(設置経過年数)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 擁壁(設置経過年数)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_setti_keika_nensuu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YhkSettiKeikaNensuu() As Integer
        Get
            Return intYhkSettiKeikaNensuu
        End Get
        Set(ByVal value As Integer)
            intYhkSettiKeikaNensuu = value
        End Set
    End Property
#End Region

#Region "擁壁(高さ)"
    ''' <summary>
    ''' 擁壁(高さ)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkTakasa As Integer = Integer.MinValue
    ''' <summary>
    ''' 擁壁(高さ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 擁壁(高さ)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_takasa", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YhkTakasa() As Integer
        Get
            Return intYhkTakasa
        End Get
        Set(ByVal value As Integer)
            intYhkTakasa = value
        End Set
    End Property
#End Region

#Region "擁壁(計画高さ)"
    ''' <summary>
    ''' 擁壁(計画高さ)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkKeikakuTakasa As Integer = Integer.MinValue
    ''' <summary>
    ''' 擁壁(計画高さ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 擁壁(計画高さ)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_keikaku_takasa", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YhkKeikakuTakasa() As Integer
        Get
            Return intYhkKeikakuTakasa
        End Get
        Set(ByVal value As Integer)
            intYhkKeikakuTakasa = value
        End Set
    End Property
#End Region

#Region "擁壁(役所確認)"
    ''' <summary>
    ''' 擁壁(役所確認)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkYakusyoKakunin As Integer = Integer.MinValue
    ''' <summary>
    ''' 擁壁(役所確認)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 擁壁(役所確認)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_yakusyo_kakunin", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property YhkYakusyoKakunin() As Integer
        Get
            Return intYhkYakusyoKakunin
        End Get
        Set(ByVal value As Integer)
            intYhkYakusyoKakunin = value
        End Set
    End Property
#End Region

#Region "ハツリの要否"
    ''' <summary>
    ''' ハツリの要否
    ''' </summary>
    ''' <remarks></remarks>
    Private intHaturiYouhi As Integer = Integer.MinValue
    ''' <summary>
    ''' ハツリの要否
    ''' </summary>
    ''' <value></value>
    ''' <returns> ハツリの要否</returns>
    ''' <remarks></remarks>
    <TableMap("haturi_youhi", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HaturiYouhi() As Integer
        Get
            Return intHaturiYouhi
        End Get
        Set(ByVal value As Integer)
            intHaturiYouhi = value
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
    <TableMap("syouhin_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
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

#Region "調査会社未決定チェックボックス"
    ''' <summary>
    ''' 調査会社未決定チェックボックス
    ''' </summary>
    ''' <remarks></remarks>
    Private blnTysKaisyaTaisyou As Boolean = False
    ''' <summary>
    ''' 調査会社未決定チェックボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査会社未決定チェックボックス</returns>
    ''' <remarks></remarks>
    Public Overridable Property TysKaisyaTaisyou() As Boolean
        Get
            Return blnTysKaisyaTaisyou
        End Get
        Set(ByVal value As Boolean)
            blnTysKaisyaTaisyou = value
        End Set
    End Property
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

End Class