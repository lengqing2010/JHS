''' <summary>
''' ReportIf取得データ設定用のレコードクラスです
''' </summary>
''' <remarks></remarks>
Public Class ReportIfGetRecord

#Region "顧客番号"
    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuNo As String
    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 顧客番号</returns>
    ''' <remarks></remarks>
    <TableMap("kokyaku_no", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=20)> _
    Public Overridable Property KokyakuNo() As String
        Get
            Return strKokyakuNo
        End Get
        Set(ByVal value As String)
            strKokyakuNo = value
        End Set
    End Property
#End Region

#Region "解析完了内容"
    ''' <summary>
    ''' 解析完了内容
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisekiKanryNaiyou As String = String.Empty
    ''' <summary>
    ''' 解析完了内容
    ''' </summary>
    ''' <value></value>
    ''' <returns>解析完了内容</returns>
    ''' <remarks></remarks>
    <TableMap("kaiseki_kanry_naiyou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overridable Property KaisekiKanryNaiyou() As String
        Get
            Return strKaisekiKanryNaiyou
        End Get
        Set(ByVal value As String)
            strKaisekiKanryNaiyou = value
        End Set
    End Property
#End Region

#Region "工事完了判断"
    ''' <summary>
    ''' 工事完了判断
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojKanryHandan As String = String.Empty
    ''' <summary>
    ''' 工事完了判断
    ''' </summary>
    ''' <value></value>
    ''' <returns>工事完了判断</returns>
    ''' <remarks></remarks>
    <TableMap("koj_kanry_handan", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property KojKanryHandan() As String
        Get
            Return strKojKanryHandan
        End Get
        Set(ByVal value As String)
            strKojKanryHandan = value
        End Set
    End Property
#End Region

#Region "入金状況判断"
    ''' <summary>
    ''' 入金状況判断
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuukinJykyHandan As String = String.Empty
    ''' <summary>
    ''' 入金状況判断
    ''' </summary>
    ''' <value></value>
    ''' <returns>入金状況判断</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_jyky_handan", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property NyuukinJykyHandan() As String
        Get
            Return strNyuukinJykyHandan
        End Get
        Set(ByVal value As String)
            strNyuukinJykyHandan = value
        End Set
    End Property
#End Region

#Region "工事完了内容"
    ''' <summary>
    ''' 工事完了内容
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojKanryNaiyou As String = String.Empty
    ''' <summary>
    ''' 工事完了内容
    ''' </summary>
    ''' <value></value>
    ''' <returns>工事完了内容</returns>
    ''' <remarks></remarks>
    <TableMap("koj_kanry_naiyou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overridable Property KojKanryNaiyou() As String
        Get
            Return strKojKanryNaiyou
        End Get
        Set(ByVal value As String)
            strKojKanryNaiyou = value
        End Set
    End Property
#End Region

#Region "入金状況内容"
    ''' <summary>
    ''' 入金状況内容
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuukinJykyNaiyou As String = String.Empty
    ''' <summary>
    ''' 入金状況内容
    ''' </summary>
    ''' <value></value>
    ''' <returns>入金状況内容</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_jyky_naiyou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overridable Property NyuukinJykyNaiyou() As String
        Get
            Return strNyuukinJykyNaiyou
        End Get
        Set(ByVal value As String)
            strNyuukinJykyNaiyou = value
        End Set
    End Property
#End Region

#Region "発行依頼日時"
    ''' <summary>
    ''' 発行依頼日時
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiTime As DateTime
    ''' <summary>
    ''' 発行依頼日時
    ''' </summary>
    ''' <value></value>
    ''' <returns>発行依頼日時</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_time", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HakIraiTime() As DateTime
        Get
            Return strHakIraiTime
        End Get
        Set(ByVal value As DateTime)
            strHakIraiTime = value
        End Set
    End Property
#End Region

#Region "発行依頼受付日時"
    ''' <summary>
    ''' 発行依頼受付日時
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiUkeDatetime As DateTime
    ''' <summary>
    ''' 発行依頼受付日時
    ''' </summary>
    ''' <value></value>
    ''' <returns>発行依頼受付日時</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_uke_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HakIraiUkeDatetime() As DateTime
        Get
            Return strHakIraiUkeDatetime
        End Get
        Set(ByVal value As DateTime)
            strHakIraiUkeDatetime = value
        End Set
    End Property
#End Region

#Region "発行依頼キャンセル日時"
    ''' <summary>
    ''' 発行依頼キャンセル日時
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiCanDatetime As DateTime
    ''' <summary>
    ''' 発行依頼キャンセル日時
    ''' </summary>
    ''' <value></value>
    ''' <returns>発行依頼キャンセル日時</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_can_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HakIraiCanDatetime() As DateTime
        Get
            Return strHakIraiCanDatetime
        End Get
        Set(ByVal value As DateTime)
            strHakIraiCanDatetime = value
        End Set
    End Property
#End Region

#Region "保証書再発行日"
    ''' <summary>
    ''' 保証書再発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoSaihakDate As DateTime
    ''' <summary>
    ''' 保証書再発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns>保証書再発行日</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_time", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HosyousyoSaihakDate() As DateTime
        Get
            Return strHosyousyoSaihakDate
        End Get
        Set(ByVal value As DateTime)
            strHosyousyoSaihakDate = value
        End Set
    End Property
#End Region


#Region "発行依頼物件名称"
    ''' <summary>
    ''' 発行依頼物件名称
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknName As String
    ''' <summary>
    ''' 発行依頼物件名称
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼物件名称</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_name", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=50)> _
    Public Overridable Property HakIraiBknName() As String
        Get
            Return strHakIraiBknName
        End Get
        Set(ByVal value As String)
            strHakIraiBknName = value
        End Set
    End Property
#End Region


#Region "発行依頼物件所在地１"
    ''' <summary>
    ''' 発行依頼物件所在地１
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr1 As String = String.Empty
    ''' <summary>
    ''' 発行依頼物件所在地１
    ''' </summary>
    ''' <value></value>
    ''' <returns>発行依頼物件所在地１</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr1", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overridable Property HakIraiBknAdr1() As String
        Get
            Return strHakIraiBknAdr1
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr1 = value
        End Set
    End Property
#End Region


#Region "発行依頼物件所在地２"
    ''' <summary>
    ''' 発行依頼物件所在地２
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr2 As String = String.Empty
    ''' <summary>
    ''' 発行依頼物件所在地２
    ''' </summary>
    ''' <value></value>
    ''' <returns>発行依頼物件所在地２</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr2", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overridable Property HakIraiBknAdr2() As String
        Get
            Return strHakIraiBknAdr2
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr2 = value
        End Set
    End Property
#End Region


#Region "発行依頼物件所在地３"
    ''' <summary>
    ''' 発行依頼物件所在地３
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr3 As String = String.Empty
    ''' <summary>
    ''' 発行依頼物件所在地３
    ''' </summary>
    ''' <value></value>
    ''' <returns>発行依頼物件所在地３</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr3", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=54)> _
    Public Overridable Property HakIraiBknAdr3() As String
        Get
            Return strHakIraiBknAdr3
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr3 = value
        End Set
    End Property
#End Region

#Region "発行依頼引渡日"
    ''' <summary>
    ''' 発行依頼引渡日
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiHwDate As DateTime
    ''' <summary>
    ''' 発行依頼引渡日
    ''' </summary>
    ''' <value></value>
    ''' <returns>発行依頼引渡日</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_hw_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HakIraiHwDate() As DateTime
        Get
            Return strHakIraiHwDate
        End Get
        Set(ByVal value As DateTime)
            strHakIraiHwDate = value
        End Set
    End Property
#End Region


#Region "発行依頼担当者"
    ''' <summary>
    ''' 発行依頼担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiTanto As String
    ''' <summary>
    ''' 発行依頼担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼担当者</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_tanto", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=50)> _
    Public Overridable Property HakIraiTanto() As String
        Get
            Return strHakIraiTanto
        End Get
        Set(ByVal value As String)
            strHakIraiTanto = value
        End Set
    End Property
#End Region


#Region "発行依頼担当者連絡先"
    ''' <summary>
    ''' 発行依頼担当者連絡先
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiTantoTel As String
    ''' <summary>
    ''' 発行依頼担当者連絡先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼担当者連絡先</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_tanto_tel", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=255)> _
    Public Overridable Property HakIraiTantoTel() As String
        Get
            Return strHakIraiTantoTel
        End Get
        Set(ByVal value As String)
            strHakIraiTantoTel = value
        End Set
    End Property
#End Region


#Region "発行依頼入力ログインID"
    ''' <summary>
    ''' 発行依頼入力ログインID
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiLogin As String
    ''' <summary>
    ''' 発行依頼入力ログインID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼入力ログインID</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_login", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=50)> _
    Public Overridable Property HakIraiLogin() As String
        Get
            Return strHakIraiLogin
        End Get
        Set(ByVal value As String)
            strHakIraiLogin = value
        End Set
    End Property
#End Region

#Region "発行依頼その他情報"
    ''' <summary>
    ''' 発行依頼その他情報
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiSonota As String
    ''' <summary>
    ''' 発行依頼その他情報
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発行依頼その他情報</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_sonota", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=255)> _
    Public Overridable Property HakIraiSonota() As String
        Get
            Return strHakIraiSonota
        End Get
        Set(ByVal value As String)
            strHakIraiSonota = value
        End Set
    End Property
#End Region


End Class