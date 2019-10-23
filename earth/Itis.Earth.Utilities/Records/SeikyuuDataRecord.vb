''' <summary>
''' 請求データレコードクラス/請求書一覧画面、過去請求書一覧画面
''' </summary>
''' <remarks>請求データの格納時に使用します</remarks>
<TableClassMap("t_seikyuu_kagami")> _
Public Class SeikyuuDataRecord

#Region "請求書NO"
    ''' <summary>
    ''' 請求書NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusyoNo As String
    ''' <summary>
    ''' 請求書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=15)> _
    Public Overridable Property SeikyuusyoNo() As String
        Get
            Return strSeikyuusyoNo
        End Get
        Set(ByVal value As String)
            strSeikyuusyoNo = value
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
    <TableMap("torikesi", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
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
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property SeikyuuSakiCd() As String
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
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property SeikyuuSakiBrc() As String
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
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overridable Property SeikyuuSakiKbn() As String
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
    <TableMap("seikyuu_saki_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overridable Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "請求先名2"
    ''' <summary>
    ''' 請求先名2
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei2 As String
    ''' <summary>
    ''' 請求先名2
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei2", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overridable Property SeikyuuSakiMei2() As String
        Get
            Return strSeikyuuSakiMei2
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei2 = value
        End Set
    End Property
#End Region

#Region "郵便番号"
    ''' <summary>
    ''' 郵便番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strYuubinNo As String
    ''' <summary>
    ''' 郵便番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 郵便番号</returns>
    ''' <remarks></remarks>
    <TableMap("yuubin_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overridable Property YuubinNo() As String
        Get
            Return strYuubinNo
        End Get
        Set(ByVal value As String)
            strYuubinNo = value
        End Set
    End Property
#End Region

#Region "住所1"
    ''' <summary>
    ''' 住所1
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyo1 As String
    ''' <summary>
    ''' 住所1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 住所1</returns>
    ''' <remarks></remarks>
    <TableMap("jyuusyo1", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overridable Property Jyuusyo1() As String
        Get
            Return strJyuusyo1
        End Get
        Set(ByVal value As String)
            strJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "住所2"
    ''' <summary>
    ''' 住所2
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyo2 As String
    ''' <summary>
    ''' 住所2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 住所2</returns>
    ''' <remarks></remarks>
    <TableMap("jyuusyo2", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overridable Property Jyuusyo2() As String
        Get
            Return strJyuusyo2
        End Get
        Set(ByVal value As String)
            strJyuusyo2 = value
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
    <TableMap("tel_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=16)> _
    Public Overridable Property TelNo() As String
        Get
            Return strTelNo
        End Get
        Set(ByVal value As String)
            strTelNo = value
        End Set
    End Property
#End Region

#Region "前回御請求額"
    ''' <summary>
    ''' 前回御請求額
    ''' </summary>
    ''' <remarks></remarks>
    Private intZenkaiGoseikyuuGaku As Integer
    ''' <summary>
    ''' 前回御請求額
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("zenkai_goseikyuu_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property ZenkaiGoseikyuuGaku() As Integer
        Get
            Return intZenkaiGoseikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intZenkaiGoseikyuuGaku = value
        End Set
    End Property
#End Region

#Region "御入金額"
    ''' <summary>
    ''' 御入金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intGonyuukinGaku As Integer
    ''' <summary>
    ''' 御入金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("gonyuukin_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property GonyuukinGaku() As Integer
        Get
            Return intGonyuukinGaku
        End Get
        Set(ByVal value As Integer)
            intGonyuukinGaku = value
        End Set
    End Property
#End Region

#Region "相殺額"
    ''' <summary>
    ''' 相殺額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSousaiGaku As Integer
    ''' <summary>
    ''' 相殺額
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("sousai_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SousaiGaku() As Integer
        Get
            Return intSousaiGaku
        End Get
        Set(ByVal value As Integer)
            intSousaiGaku = value
        End Set
    End Property
#End Region

#Region "調整額"
    ''' <summary>
    ''' 調整額
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyouseiGaku As Integer
    ''' <summary>
    ''' 調整額
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("tyousei_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TyouseiGaku() As Integer
        Get
            Return intTyouseiGaku
        End Get
        Set(ByVal value As Integer)
            intTyouseiGaku = value
        End Set
    End Property
#End Region

#Region "繰越金額"
    ''' <summary>
    ''' 繰越金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intKurikosiGaku As Integer
    ''' <summary>
    ''' 繰越金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kurikosi_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KurikosiGaku() As Integer
        Get
            Return intKurikosiGaku
        End Get
        Set(ByVal value As Integer)
            intKurikosiGaku = value
        End Set
    End Property
#End Region

#Region "今回御請求金額"
    ''' <summary>
    ''' 今回御請求金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intKonkaiGoseikyuuGaku As Integer
    ''' <summary>
    ''' 今回御請求金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("konkai_goseikyuu_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KonkaiGoseikyuuGaku() As Integer
        Get
            Return intKonkaiGoseikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intKonkaiGoseikyuuGaku = value
        End Set
    End Property
#End Region

#Region "今回繰越金額"
    ''' <summary>
    ''' 今回繰越金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intKonkaiKurikosiGaku As Integer
    ''' <summary>
    ''' 今回繰越金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("konkai_kurikosi_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KonkaiKurikosiGaku() As Integer
        Get
            Return intKonkaiKurikosiGaku
        End Get
        Set(ByVal value As Integer)
            intKonkaiKurikosiGaku = value
        End Set
    End Property
#End Region

#Region "今回回収予定日"
    ''' <summary>
    ''' 今回回収予定日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKonkaiKaisyuuYoteiDate As DateTime
    ''' <summary>
    ''' 今回回収予定日
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("konkai_kaisyuu_yotei_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property KonkaiKaisyuuYoteiDate() As DateTime
        Get
            Return dateKonkaiKaisyuuYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateKonkaiKaisyuuYoteiDate = value
        End Set
    End Property
#End Region

#Region "請求書印刷日"
    ''' <summary>
    ''' 請求書印刷日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoInsatuDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 請求書印刷日
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_insatu_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property SeikyuusyoInsatuDate() As DateTime
        Get
            Return dateSeikyuusyoInsatuDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoInsatuDate = value
        End Set
    End Property
#End Region

#Region "請求書発行日"
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoHakDate As DateTime
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書発行日</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_hak_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property SeikyuusyoHakDate() As DateTime
        Get
            Return dateSeikyuusyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoHakDate = value
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
    <TableMap("tantousya_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overridable Property TantousyaMei() As String
        Get
            Return strTantousyaMei
        End Get
        Set(ByVal value As String)
            strTantousyaMei = value
        End Set
    End Property
#End Region

#Region "請求書印字物件名フラグ"
    ''' <summary>
    ''' 請求書印字物件名フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuusyoInjiBukkenMeiFlg As Integer
    ''' <summary>
    ''' 請求書印字物件名フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_inji_bukken_mei_flg", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SeikyuusyoInjiBukkenMeiFlg() As Integer
        Get
            Return intSeikyuusyoInjiBukkenMeiFlg
        End Get
        Set(ByVal value As Integer)
            intSeikyuusyoInjiBukkenMeiFlg = value
        End Set
    End Property
#End Region

#Region "入金口座番号"
    ''' <summary>
    ''' 入金口座番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuukinKouzaNo As String
    ''' <summary>
    ''' 入金口座番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_kouza_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property NyuukinKouzaNo() As String
        Get
            Return strNyuukinKouzaNo
        End Get
        Set(ByVal value As String)
            strNyuukinKouzaNo = value
        End Set
    End Property
#End Region

#Region "請求締め日"
    ''' <summary>
    ''' 請求締め日
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSimeDate As String
    ''' <summary>
    ''' 請求締め日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求締め日</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_sime_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property SeikyuuSimeDate() As String
        Get
            Return strSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "先方請求締め日"
    ''' <summary>
    ''' 先方請求締め日
    ''' </summary>
    ''' <remarks></remarks>
    Private strSenpouSeikyuuSimeDate As String
    ''' <summary>
    ''' 先方請求締め日
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("senpou_seikyuu_sime_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property SenpouSeikyuuSimeDate() As String
        Get
            Return strSenpouSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strSenpouSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "相殺フラグ"
    ''' <summary>
    ''' 相殺フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSousaiFlg As Integer
    ''' <summary>
    ''' 相殺フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("sousai_flg", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SousaiFlg() As Integer
        Get
            Return intSousaiFlg
        End Get
        Set(ByVal value As Integer)
            intSousaiFlg = value
        End Set
    End Property
#End Region

#Region "回収予定月数"
    ''' <summary>
    ''' 回収予定月数
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisyuuYoteiGessuu As Integer
    ''' <summary>
    ''' 回収予定月数
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_yotei_gessuu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisyuuYoteiGessuu() As Integer
        Get
            Return intKaisyuuYoteiGessuu
        End Get
        Set(ByVal value As Integer)
            intKaisyuuYoteiGessuu = value
        End Set
    End Property
#End Region

#Region "回収予定日"
    ''' <summary>
    ''' 回収予定日
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuYoteiDate As String
    ''' <summary>
    ''' 回収予定日
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_yotei_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property KaisyuuYoteiDate() As String
        Get
            Return strKaisyuuYoteiDate
        End Get
        Set(ByVal value As String)
            strKaisyuuYoteiDate = value
        End Set
    End Property
#End Region

#Region "請求書必着日"
    ''' <summary>
    ''' 請求書必着日
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusyoHittykDate As String
    ''' <summary>
    ''' 請求書必着日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書必着日</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_hittyk_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property SeikyuusyoHittykDate() As String
        Get
            Return strSeikyuusyoHittykDate
        End Get
        Set(ByVal value As String)
            strSeikyuusyoHittykDate = value
        End Set
    End Property
#End Region

#Region "種別1"
    ''' <summary>
    ''' 種別1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSyubetu1 As String
    ''' <summary>
    ''' 種別1
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_syubetu1", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property KaisyuuSyubetu1() As String
        Get
            Return strKaisyuuSyubetu1
        End Get
        Set(ByVal value As String)
            strKaisyuuSyubetu1 = value
        End Set
    End Property
#End Region

#Region "割合1"
    ''' <summary>
    ''' 割合1
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisyuuWariai1 As Integer
    ''' <summary>
    ''' 割合1
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_wariai1", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisyuuWariai1() As Integer
        Get
            Return intKaisyuuWariai1
        End Get
        Set(ByVal value As Integer)
            intKaisyuuWariai1 = value
        End Set
    End Property
#End Region

#Region "手形サイト月数"
    ''' <summary>
    ''' 手形サイト月数
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisyuuTegataSiteGessuu As Integer
    ''' <summary>
    ''' 手形サイト月数
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_tegata_site_gessuu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisyuuTegataSiteGessuu() As Integer
        Get
            Return intKaisyuuTegataSiteGessuu
        End Get
        Set(ByVal value As Integer)
            intKaisyuuTegataSiteGessuu = value
        End Set
    End Property
#End Region

#Region "手形サイト日"
    ''' <summary>
    ''' 手形サイト日
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuTegataSiteDate As String
    ''' <summary>
    ''' 手形サイト日
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_tegata_site_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property KaisyuuTegataSiteDate() As String
        Get
            Return strKaisyuuTegataSiteDate
        End Get
        Set(ByVal value As String)
            strKaisyuuTegataSiteDate = value
        End Set
    End Property
#End Region

#Region "請求書用紙"
    ''' <summary>
    ''' 請求書用紙
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSeikyuusyoYousi As String
    ''' <summary>
    ''' 請求書用紙
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_seikyuusyo_yousi", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property KaisyuuSeikyuusyoYousi() As String
        Get
            Return strKaisyuuSeikyuusyoYousi
        End Get
        Set(ByVal value As String)
            strKaisyuuSeikyuusyoYousi = value
        End Set
    End Property
#End Region

#Region "請求書用紙汎用コード"
    ''' <summary>
    ''' 請求書用紙汎用コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSeikyuusyoYousiHannyouCd As String
    ''' <summary>
    ''' 請求書用紙汎用コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_seikyuusyo_yousi_hannyou_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overridable Property KaisyuuSeikyuusyoYousiHannyouCd() As String
        Get
            Return strKaisyuuSeikyuusyoYousiHannyouCd
        End Get
        Set(ByVal value As String)
            strKaisyuuSeikyuusyoYousiHannyouCd = value
        End Set
    End Property
#End Region

#Region "種別2"
    ''' <summary>
    ''' 種別2
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSyubetu2 As String
    ''' <summary>
    ''' 種別2
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_syubetu2", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property KaisyuuSyubetu2() As String
        Get
            Return strKaisyuuSyubetu2
        End Get
        Set(ByVal value As String)
            strKaisyuuSyubetu2 = value
        End Set
    End Property
#End Region

#Region "割合2"
    ''' <summary>
    ''' 割合2
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisyuuWariai2 As Integer
    ''' <summary>
    ''' 割合2
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_wariai2", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisyuuWariai2() As Integer
        Get
            Return intKaisyuuWariai2
        End Get
        Set(ByVal value As Integer)
            intKaisyuuWariai2 = value
        End Set
    End Property
#End Region

#Region "種別3"
    ''' <summary>
    ''' 種別3
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSyubetu3 As String
    ''' <summary>
    ''' 種別3
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_syubetu3", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property KaisyuuSyubetu3() As String
        Get
            Return strKaisyuuSyubetu3
        End Get
        Set(ByVal value As String)
            strKaisyuuSyubetu3 = value
        End Set
    End Property
#End Region

#Region "割合3"
    ''' <summary>
    ''' 割合3
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisyuuWariai3 As Integer
    ''' <summary>
    ''' 割合3
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_wariai3", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisyuuWariai3() As Integer
        Get
            Return intKaisyuuWariai3
        End Get
        Set(ByVal value As Integer)
            intKaisyuuWariai3 = value
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
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "請求先M.請求締め日"
    ''' <summary>
    ''' 請求先M.請求締め日
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSimeDateMst As String
    ''' <summary>
    ''' 請求先M.請求締め日
    ''' </summary>
    ''' <remarks></remarks>
    <TableMap("mst_seikyuu_sime_date")> _
    Public Overridable Property SeikyuuSimeDateMst() As String
        Get
            Return strSeikyuuSimeDateMst
        End Get
        Set(ByVal value As String)
            strSeikyuuSimeDateMst = value
        End Set
    End Property
#End Region

#Region "VIEW 請求先名"
    ''' <summary>
    ''' VIEW 請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMeiView As String
    ''' <summary>
    ''' VIEW 請求先名
    ''' </summary>
    ''' <value></value>
    ''' <returns> VIEW 請求先名</returns>
    ''' <remarks></remarks>
    <TableMap("view_seikyuu_saki_mei")> _
    Public Property SeikyuuSakiMeiView() As String
        Get
            Return strSeikyuuSakiMeiView
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMeiView = value
        End Set
    End Property
#End Region

#Region "請求鑑T.請求書発行日と売上データT.請求年月日との差異フラグ"
    ''' <summary>
    ''' 請求鑑T.請求書発行日と売上データT.請求年月日との差異フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuuDateSaiFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 請求鑑T.請求書発行日と売上データT.請求年月日との差異フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求鑑T.請求書発行日と売上データT.請求年月日のいずれかが異なる場合、1を返す
    ''' ※印字対象フラグ=1</returns>
    ''' <remarks>1:差異あり,0:差異なし</remarks>
    <TableMap("seikyuu_date_sai_flg")> _
    Public Property SeikyuuDateSaiFlg() As Integer
        Get
            Return intSeikyuuDateSaiFlg
        End Get
        Set(ByVal value As Integer)
            intSeikyuuDateSaiFlg = value
        End Set
    End Property
#End Region

#Region "印刷対象外フラグ"
    ''' <summary>
    ''' 印刷対象外フラグ(請求書用紙汎用コードに'9～'が含まれている場合1をセットする。以外かNULLの場合は0)
    ''' </summary>
    ''' <remarks></remarks>
    Private intPrintTaigyougaiFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 印刷対象外フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("print_taigyougai_flg")> _
    Public Overridable Property PrintTaisyougaiFlg() As Integer
        Get
            Return intPrintTaigyougaiFlg
        End Get
        Set(ByVal value As Integer)
            intPrintTaigyougaiFlg = value
        End Set
    End Property
#End Region

#Region "明細件数"
    ''' <summary>
    ''' 明細件数
    ''' </summary>
    ''' <remarks></remarks>
    Private intMeisaiKensuu As Integer = 0
    ''' <summary>
    ''' 明細件数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 明細件数</returns>
    ''' <remarks></remarks>
    <TableMap("meisai_kensuu")> _
    Public Property MeisaiKensuu() As Integer
        Get
            Return intMeisaiKensuu
        End Get
        Set(ByVal value As Integer)
            intMeisaiKensuu = value
        End Set
    End Property
#End Region

#Region "拡張名称M.請求書用紙名称"
    ''' <summary>
    ''' 請求書用紙名称
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSeikyuusyoYousiMei As String
    ''' <summary>
    ''' 請求書用紙名称
    ''' </summary>
    ''' <remarks></remarks>
    <TableMap("mst_meisyou")> _
    Public Overridable Property KaisyuuSeikyuusyoYousiMei() As String
        Get
            Return strKaisyuuSeikyuusyoYousiMei
        End Get
        Set(ByVal value As String)
            strKaisyuuSeikyuusyoYousiMei = value
        End Set
    End Property
#End Region

End Class
