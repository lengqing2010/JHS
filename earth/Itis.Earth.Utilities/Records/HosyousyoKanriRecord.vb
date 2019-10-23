''' <summary>
''' 保証書管理テーブルのレコードクラスです
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_hosyousyo_kanri")> _
Public Class HosyousyoKanriRecord

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
    <TableMap("kbn", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property Kbn() As String
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
    <TableMap("hosyousyo_no", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "物件状況"
    ''' <summary>
    ''' 物件状況
    ''' </summary>
    ''' <remarks></remarks>
    Private intBukkenJyky As Integer = Integer.MinValue
    ''' <summary>
    ''' 物件状況
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件状況</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyky", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property BukkenJyky() As Integer
        Get
            Return intBukkenJyky
        End Get
        Set(ByVal value As Integer)
            intBukkenJyky = value
        End Set
    End Property
#End Region

#Region "解析完了"
    ''' <summary>
    ''' 解析完了
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisekiKanry As Integer = Integer.MinValue
    ''' <summary>
    ''' 解析完了
    ''' </summary>
    ''' <value></value>
    ''' <returns> 解析完了</returns>
    ''' <remarks></remarks>
    <TableMap("kaiseki_kanry", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property KaisekiKanry() As Integer
        Get
            Return intKaisekiKanry
        End Get
        Set(ByVal value As Integer)
            intKaisekiKanry = value
        End Set
    End Property
#End Region

#Region "工事有無"
    ''' <summary>
    ''' 工事有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 工事有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事有無</returns>
    ''' <remarks></remarks>
    <TableMap("koj_umu", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property KojUmu() As Integer
        Get
            Return intKojUmu
        End Get
        Set(ByVal value As Integer)
            intKojUmu = value
        End Set
    End Property
#End Region

#Region "工事完了"
    ''' <summary>
    ''' 工事完了
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojKanry As Integer = Integer.MinValue
    ''' <summary>
    ''' 工事完了
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事完了</returns>
    ''' <remarks></remarks>
    <TableMap("koj_kanry", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property KojKanry() As Integer
        Get
            Return intKojKanry
        End Get
        Set(ByVal value As Integer)
            intKojKanry = value
        End Set
    End Property
#End Region

#Region "入金確認条件"
    ''' <summary>
    ''' 入金確認条件
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinKakuninJyouken As Integer = Integer.MinValue
    ''' <summary>
    ''' 入金確認条件
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金確認条件</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_kakunin_jyouken", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property NyuukinKakuninJyouken() As Integer
        Get
            Return intNyuukinKakuninJyouken
        End Get
        Set(ByVal value As Integer)
            intNyuukinKakuninJyouken = value
        End Set
    End Property
#End Region

#Region "入金状況"
    ''' <summary>
    ''' 入金状況
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinJyky As Integer = Integer.MinValue
    ''' <summary>
    ''' 入金状況
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金状況</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_jyky", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property NyuukinJyky() As Integer
        Get
            Return intNyuukinJyky
        End Get
        Set(ByVal value As Integer)
            intNyuukinJyky = value
        End Set
    End Property
#End Region

#Region "瑕疵"
    ''' <summary>
    ''' 瑕疵
    ''' </summary>
    ''' <remarks></remarks>
    Private intKasi As Integer = Integer.MinValue
    ''' <summary>
    ''' 瑕疵
    ''' </summary>
    ''' <value></value>
    ''' <returns> 瑕疵</returns>
    ''' <remarks></remarks>
    <TableMap("kasi", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Kasi() As Integer
        Get
            Return intKasi
        End Get
        Set(ByVal value As Integer)
            intKasi = value
        End Set
    End Property
#End Region

#Region "保険会社"
    ''' <summary>
    ''' 保険会社
    ''' </summary>
    ''' <remarks></remarks>
    Private intHokenKaisya As Integer = Integer.MinValue
    ''' <summary>
    ''' 保険会社
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保険会社</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_kaisya", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("hoken_sinsei_tuki", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HokenSinseiTuki() As DateTime
        Get
            Return dateHokenSinseiTuki
        End Get
        Set(ByVal value As DateTime)
            dateHokenSinseiTuki = value
        End Set
    End Property
#End Region

#Region "保険申請区分"
    ''' <summary>
    ''' 保険申請区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intHokenSinseiKbn As Integer = Integer.MinValue
    ''' <summary>
    ''' 保険申請区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保険申請区分</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_sinsei_kbn", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HokenSinseiKbn() As Integer
        Get
            Return intHokenSinseiKbn
        End Get
        Set(ByVal value As Integer)
            intHokenSinseiKbn = value
        End Set
    End Property
#End Region

#Region "引渡し前保険"
    ''' <summary>
    ''' 引渡し前保険
    ''' </summary>
    ''' <remarks></remarks>
    Private intHwMaeHkn As Integer = Integer.MinValue
    ''' <summary>
    ''' 引渡し前保険
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引渡し前保険</returns>
    ''' <remarks></remarks>
    <TableMap("hw_mae_hkn", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HwMaeHkn() As Integer
        Get
            Return intHwMaeHkn
        End Get
        Set(ByVal value As Integer)
            intHwMaeHkn = value
        End Set
    End Property
#End Region

#Region "引渡し前保険年月日"
    ''' <summary>
    ''' 引渡し前保険年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwMaeHknDate As DateTime
    ''' <summary>
    ''' 引渡し前保険年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引渡し前保険年月日</returns>
    ''' <remarks></remarks>
    <TableMap("hw_mae_hkn_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwMaeHknDate() As DateTime
        Get
            Return dateHwMaeHknDate
        End Get
        Set(ByVal value As DateTime)
            dateHwMaeHknDate = value
        End Set
    End Property
#End Region

#Region "引渡し前保険実施日"
    ''' <summary>
    ''' 引渡し前保険実施日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwMaeHknJissiDate As DateTime
    ''' <summary>
    ''' 引渡し前保険実施日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引渡し前保険実施日</returns>
    ''' <remarks></remarks>
    <TableMap("hw_mae_hkn_jissi_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwMaeHknJissiDate() As DateTime
        Get
            Return dateHwMaeHknJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateHwMaeHknJissiDate = value
        End Set
    End Property
#End Region

#Region "引渡し前保険適用予定実施日"
    ''' <summary>
    ''' 引渡し前保険適用予定実施日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwMaeHknTekiyouYoteiJissiDate As DateTime
    ''' <summary>
    ''' 引渡し前保険適用予定実施日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引渡し前保険適用予定実施日</returns>
    ''' <remarks></remarks>
    <TableMap("hw_mae_hkn_tekiyou_yotei_jissi_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwMaeHknTekiyouYoteiJissiDate() As DateTime
        Get
            Return dateHwMaeHknTekiyouYoteiJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateHwMaeHknTekiyouYoteiJissiDate = value
        End Set
    End Property
#End Region

#Region "引渡し後保険"
    ''' <summary>
    ''' 引渡し後保険
    ''' </summary>
    ''' <remarks></remarks>
    Private intHwAtoHkn As Integer = Integer.MinValue
    ''' <summary>
    ''' 引渡し後保険
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引渡し後保険</returns>
    ''' <remarks></remarks>
    <TableMap("hw_ato_hkn", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HwAtoHkn() As Integer
        Get
            Return intHwAtoHkn
        End Get
        Set(ByVal value As Integer)
            intHwAtoHkn = value
        End Set
    End Property
#End Region

#Region "引渡し後保険年月日"
    ''' <summary>
    ''' 引渡し後保険年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwAtoHknDate As DateTime
    ''' <summary>
    ''' 引渡し後保険年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引渡し後保険年月日</returns>
    ''' <remarks></remarks>
    <TableMap("hw_ato_hkn_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwAtoHknDate() As DateTime
        Get
            Return dateHwAtoHknDate
        End Get
        Set(ByVal value As DateTime)
            dateHwAtoHknDate = value
        End Set
    End Property
#End Region

#Region "引渡し後保険実施日"
    ''' <summary>
    ''' 引渡し後保険実施日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwAtoHknJissiDate As DateTime
    ''' <summary>
    ''' 引渡し後保険実施日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引渡し後保険実施日</returns>
    ''' <remarks></remarks>
    <TableMap("hw_ato_hkn_jissi_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwAtoHknJissiDate() As DateTime
        Get
            Return dateHwAtoHknJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateHwAtoHknJissiDate = value
        End Set
    End Property
#End Region

#Region "引渡し後保険適用予定実施日"
    ''' <summary>
    ''' 引渡し後保険適用予定実施日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwAtoHknTekiyouYoteiJissiDate As DateTime
    ''' <summary>
    ''' 引渡し後保険適用予定実施日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引渡し後保険適用予定実施日</returns>
    ''' <remarks></remarks>
    <TableMap("hw_ato_hkn_tekiyou_yotei_jissi_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwAtoHknTekiyouYoteiJissiDate() As DateTime
        Get
            Return dateHwAtoHknTekiyouYoteiJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateHwAtoHknTekiyouYoteiJissiDate = value
        End Set
    End Property
#End Region

#Region "引渡し後保険取消種別"
    ''' <summary>
    ''' 引渡し後保険取消種別
    ''' </summary>
    ''' <remarks></remarks>
    Private intHwAtoHknTorikesiSyubetsu As Integer = Integer.MinValue
    ''' <summary>
    ''' 引渡し後保険取消種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引渡し後保険取消種別</returns>
    ''' <remarks></remarks>
    <TableMap("hw_ato_hkn_torikesi_syubetsu", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HwAtoHknTorikesiSyubetsu() As Integer
        Get
            Return intHwAtoHknTorikesiSyubetsu
        End Get
        Set(ByVal value As Integer)
            intHwAtoHknTorikesiSyubetsu = value
        End Set
    End Property
#End Region

#Region "処理フラグ"
    ''' <summary>
    ''' 処理フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyoriFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 処理フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 処理フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("syori_flg", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SyoriFlg() As Integer
        Get
            Return intSyoriFlg
        End Get
        Set(ByVal value As Integer)
            intSyoriFlg = value
        End Set
    End Property
#End Region

#Region "処理日時"
    ''' <summary>
    ''' 処理日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoriDateTime As DateTime
    ''' <summary>
    ''' 処理日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 処理日時</returns>
    ''' <remarks></remarks>
    <TableMap("syori_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property SyoriDateTime() As DateTime
        Get
            Return dateSyoriDateTime
        End Get
        Set(ByVal value As DateTime)
            dateSyoriDateTime = value
        End Set
    End Property
#End Region

#Region "保証書タイプ"
    ''' <summary>
    ''' 保証書タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoType As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証書タイプ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書タイプ</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_type", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HosyousyoType() As Integer
        Get
            Return intHosyousyoType
        End Get
        Set(ByVal value As Integer)
            intHosyousyoType = value
        End Set
    End Property
#End Region

#Region "保証期間"
    ''' <summary>
    ''' 保証期間
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikan As Integer = Integer.MinValue
    ''' <summary>
    ''' 保証期間
    ''' </summary>
    ''' <value></value>
    ''' <returns>保証期間</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kikan", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HosyouKikan() As Integer
        Get
            Return intHosyouKikan
        End Get
        Set(ByVal value As Integer)
            intHosyouKikan = value
        End Set
    End Property
#End Region

#Region "業務完了日"
    ''' <summary>
    ''' 業務完了日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateGyoumuKanryDate As DateTime
    ''' <summary>
    ''' 業務完了日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 業務完了日</returns>
    ''' <remarks></remarks>
    <TableMap("gyoumu_kanry_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property GyoumuKanryDate() As DateTime
        Get
            Return dateGyoumuKanryDate
        End Get
        Set(ByVal value As DateTime)
            dateGyoumuKanryDate = value
        End Set
    End Property
#End Region

#Region "保証開始業務内容"
    ''' <summary>
    ''' 保証開始業務内容
    ''' </summary>
    ''' <remarks></remarks>
    Private strGyoumuKaisiNaiyou As String
    ''' <summary>
    ''' 保証開始業務内容
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証開始業務内容</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kaisi_gyoumu_naiyou", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=18)> _
    Public Property GyoumuKaisiNaiyou() As String
        Get
            Return strGyoumuKaisiNaiyou
        End Get
        Set(ByVal value As String)
            strGyoumuKaisiNaiyou = value
        End Set
    End Property
#End Region


#Region "登録ログインユーザID"
    ''' <summary>
    ''' 登録ログインユーザID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' 登録ログインユーザID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ログインユーザID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    Private dateAddDateTime As DateTime
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property AddDateTime() As DateTime
        Get
            Return dateAddDateTime
        End Get
        Set(ByVal value As DateTime)
            dateAddDateTime = value
        End Set
    End Property
#End Region

#Region "更新ログインユーザID"
    ''' <summary>
    ''' 更新ログインユーザID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' 更新ログインユーザID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    Private dateUpdDateTime As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UpdDateTime() As DateTime
        Get
            Return dateUpdDateTime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDateTime = value
        End Set
    End Property
#End Region

End Class