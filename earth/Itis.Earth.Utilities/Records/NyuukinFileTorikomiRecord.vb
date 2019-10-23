''' <summary>
''' 入金ファイル取込テーブル用レコードクラス
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_nyuukin_file_torikomi")> _
Public Class NyuukinFileTorikomiRecord

#Region "入金取込ユニークNO"
    ''' <summary>
    ''' 入金取込ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinTorikomiUniqueNo As Integer = Integer.MinValue
    ''' <summary>
    ''' 入金取込ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金取込ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_torikomi_unique_no", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property NyuukinTorikomiUniqueNo() As Integer
        Get
            Return intNyuukinTorikomiUniqueNo
        End Get
        Set(ByVal value As Integer)
            intNyuukinTorikomiUniqueNo = value
        End Set
    End Property
#End Region

#Region "EDI情報作成日"
    ''' <summary>
    ''' EDI情報作成日
    ''' </summary>
    ''' <remarks></remarks>
    Private intEdiJouhouSakuseiDate As String
    ''' <summary>
    ''' EDI情報作成日
    ''' </summary>
    ''' <value></value>
    ''' <returns> EDI情報作成日</returns>
    ''' <remarks></remarks>
    <TableMap("edi_jouhou_sakusei_date", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Property EdiJouhouSakuseiDate() As String
        Get
            Return intEdiJouhouSakuseiDate
        End Get
        Set(ByVal value As String)
            intEdiJouhouSakuseiDate = value
        End Set
    End Property
#End Region

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "取込伝票番号"
    ''' <summary>
    ''' 取込伝票番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strTorikomiDenpyouNo As String
    ''' <summary>
    ''' 取込伝票番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取込伝票番号</returns>
    ''' <remarks></remarks>
    <TableMap("torikomi_denpyou_no", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Char, SqlLength:=6)> _
    Public Property TorikomiDenpyouNo() As String
        Get
            Return strTorikomiDenpyouNo
        End Get
        Set(ByVal value As String)
            strTorikomiDenpyouNo = value
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
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Property SeikyuuSakiCd() As String
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
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Property SeikyuuSakiBrc() As String
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
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property SeikyuuSakiKbn() As String
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
    <TableMap("seikyuu_saki_mei", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "照合口座No"
    ''' <summary>
    ''' 照合口座No
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyougouKouzaNo As String
    ''' <summary>
    ''' 照合口座No
    ''' </summary>
    ''' <value></value>
    ''' <returns> 照合口座No</returns>
    ''' <remarks></remarks>
    <TableMap("syougou_kouza_no", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property SyougouKouzaNo() As String
        Get
            Return strSyougouKouzaNo
        End Get
        Set(ByVal value As String)
            strSyougouKouzaNo = value
        End Set
    End Property
#End Region

#Region "入金日"
    ''' <summary>
    ''' 入金日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuukinDate As DateTime
    ''' <summary>
    ''' 入金日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金日</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property NyuukinDate() As DateTime
        Get
            Return dateNyuukinDate
        End Get
        Set(ByVal value As DateTime)
            dateNyuukinDate = value
        End Set
    End Property
#End Region

#Region "入金額 [現金]"
    ''' <summary>
    ''' 入金額 [現金]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngGenkin As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [現金]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [現金]</returns>
    ''' <remarks></remarks>
    <TableMap("genkin", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property Genkin() As Long
        Get
            Return lngGenkin
        End Get
        Set(ByVal value As Long)
            lngGenkin = value
        End Set
    End Property
#End Region

#Region "入金額 [小切手]"
    ''' <summary>
    ''' 入金額 [小切手]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKogitte As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [小切手]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [小切手]</returns>
    ''' <remarks></remarks>
    <TableMap("kogitte", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property Kogitte() As Long
        Get
            Return lngKogitte
        End Get
        Set(ByVal value As Long)
            lngKogitte = value
        End Set
    End Property
#End Region

#Region "入金額 [振込]"
    ''' <summary>
    ''' 入金額 [振込]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomi As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [振込]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [振込]</returns>
    ''' <remarks></remarks>
    <TableMap("furikomi", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property Furikomi() As Long
        Get
            Return lngFurikomi
        End Get
        Set(ByVal value As Long)
            lngFurikomi = value
        End Set
    End Property
#End Region

#Region "入金額 [手形]"
    ''' <summary>
    ''' 入金額 [手形]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTegata As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [手形]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [手形]</returns>
    ''' <remarks></remarks>
    <TableMap("tegata", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property Tegata() As Long
        Get
            Return lngTegata
        End Get
        Set(ByVal value As Long)
            lngTegata = value
        End Set
    End Property
#End Region

#Region "入金額 [相殺]"
    ''' <summary>
    ''' 入金額 [相殺]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSousai As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [相殺]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [相殺]</returns>
    ''' <remarks></remarks>
    <TableMap("sousai", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property Sousai() As Long
        Get
            Return lngSousai
        End Get
        Set(ByVal value As Long)
            lngSousai = value
        End Set
    End Property
#End Region

#Region "入金額 [値引]"
    ''' <summary>
    ''' 入金額 [値引]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngNebiki As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [値引]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [値引]</returns>
    ''' <remarks></remarks>
    <TableMap("nebiki", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property Nebiki() As Long
        Get
            Return lngNebiki
        End Get
        Set(ByVal value As Long)
            lngNebiki = value
        End Set
    End Property
#End Region

#Region "入金額 [その他]"
    ''' <summary>
    ''' 入金額 [その他]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSonota As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [その他]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [その他]</returns>
    ''' <remarks></remarks>
    <TableMap("sonota", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property Sonota() As Long
        Get
            Return lngSonota
        End Get
        Set(ByVal value As Long)
            lngSonota = value
        End Set
    End Property
#End Region

#Region "入金額 [協力会費]"
    ''' <summary>
    ''' 入金額 [協力会費]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKyouryokuKaihi As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [協力会費]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [協力会費]</returns>
    ''' <remarks></remarks>
    <TableMap("kyouryoku_kaihi", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property KyouryokuKaihi() As Long
        Get
            Return lngKyouryokuKaihi
        End Get
        Set(ByVal value As Long)
            lngKyouryokuKaihi = value
        End Set
    End Property
#End Region

#Region "入金額 [口座振替]"
    ''' <summary>
    ''' 入金額 [口座振替]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKouzaFurikae As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [口座振替]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [口座振替]</returns>
    ''' <remarks></remarks>
    <TableMap("kouza_furikae", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property KouzaFurikae() As Long
        Get
            Return lngKouzaFurikae
        End Get
        Set(ByVal value As Long)
            lngKouzaFurikae = value
        End Set
    End Property
#End Region

#Region "入金額 [振込手数料]"
    ''' <summary>
    ''' 入金額 [振込手数料]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomiTesuuryou As Long = Long.MinValue
    ''' <summary>
    ''' 入金額 [振込手数料]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [振込手数料]</returns>
    ''' <remarks></remarks>
    <TableMap("furikomi_tesuuryou", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property FurikomiTesuuryou() As Long
        Get
            Return lngFurikomiTesuuryou
        End Get
        Set(ByVal value As Long)
            lngFurikomiTesuuryou = value
        End Set
    End Property
#End Region

#Region "伝票合計金額[サマリー]"
    ''' <summary>
    ''' 伝票合計金額[サマリー]
    ''' </summary>
    ''' <value></value>
    ''' <returns>伝票合計金額[サマリー]</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DenpyouGoukeiGaku() As Long
        Get
            Dim tmpGoukei As Long = 0

            Dim tmpGenkin As Long = IIf(lngGenkin = Long.MinValue, 0, lngGenkin)
            Dim tmpKogitte As Long = IIf(lngKogitte = Long.MinValue, 0, lngKogitte)
            Dim tmpFurikomi As Long = IIf(lngFurikomi = Long.MinValue, 0, lngFurikomi)
            Dim tmpTegata As Long = IIf(lngTegata = Long.MinValue, 0, lngTegata)
            Dim tmpSousai As Long = IIf(lngSousai = Long.MinValue, 0, lngSousai)
            Dim tmpNebiki As Long = IIf(lngNebiki = Long.MinValue, 0, lngNebiki)
            Dim tmpSonota As Long = IIf(lngSonota = Long.MinValue, 0, lngSonota)
            Dim tmpKyouryokuKaihi As Long = IIf(lngKyouryokuKaihi = Long.MinValue, 0, lngKyouryokuKaihi)
            Dim tmpKouzaFurikae As Long = IIf(lngKouzaFurikae = Long.MinValue, 0, lngKouzaFurikae)
            Dim tmpFurikomiTesuuryou As Long = IIf(lngFurikomiTesuuryou = Long.MinValue, 0, lngFurikomiTesuuryou)

            tmpGoukei = tmpGenkin + tmpKogitte + tmpFurikomi + tmpTegata + tmpSousai _
                            + tmpNebiki + tmpSonota + tmpKyouryokuKaihi + tmpKouzaFurikae + tmpFurikomiTesuuryou

            Return tmpGoukei
        End Get
    End Property
#End Region

#Region "手形期日"
    ''' <summary>
    ''' 手形期日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTegataKijitu As DateTime
    ''' <summary>
    ''' 手形期日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 手形期日</returns>
    ''' <remarks></remarks>
    <TableMap("tegata_kijitu", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property TegataKijitu() As DateTime
        Get
            Return dateTegataKijitu
        End Get
        Set(ByVal value As DateTime)
            dateTegataKijitu = value
        End Set
    End Property
#End Region

#Region "手形No."
    ''' <summary>
    ''' 手形No.
    ''' </summary>
    ''' <remarks></remarks>
    Private strTegataNo As String
    ''' <summary>
    ''' 手形No.
    ''' </summary>
    ''' <value></value>
    ''' <returns> 手形No.</returns>
    ''' <remarks></remarks>
    <TableMap("tegata_no", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property TegataNo() As String
        Get
            Return strTegataNo
        End Get
        Set(ByVal value As String)
            strTegataNo = value
        End Set
    End Property
#End Region

#Region "摘要名"
    ''' <summary>
    ''' 摘要名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTekiyouMei As String
    ''' <summary>
    ''' 摘要名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 摘要名</returns>
    ''' <remarks></remarks>
    <TableMap("tekiyou_mei", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=255)> _
    Public Property TekiyouMei() As String
        Get
            Return strTekiyouMei
        End Get
        Set(ByVal value As String)
            strTekiyouMei = value
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
    <TableMap("add_login_user_id", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("add_datetime", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("upd_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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