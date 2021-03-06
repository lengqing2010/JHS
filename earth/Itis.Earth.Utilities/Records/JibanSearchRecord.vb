Public Class JibanSearchRecord

#Region "æª"
    ''' <summary>
    ''' æª
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' æª
    ''' </summary>
    ''' <value></value>
    ''' <returns> æª</returns>
    ''' <remarks></remarks>
    <TableMap("kbn")> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "ÛØNO"
    ''' <summary>
    ''' ÛØNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' ÛØNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÛØNO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no")> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "f[^jüíÊ"
    ''' <summary>
    ''' f[^jüíÊ
    ''' </summary>
    ''' <remarks></remarks>
    Private strDataHakiSyubetu As String
    ''' <summary>
    ''' f[^jüíÊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> f[^jüíÊ</returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_syubetu")> _
    Public Property DataHakiSyubetu() As String
        Get
            Return strDataHakiSyubetu
        End Get
        Set(ByVal value As String)
            strDataHakiSyubetu = value
        End Set
    End Property
#End Region

#Region "{å¼"
    ''' <summary>
    ''' {å¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' {å¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> {å¼</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei")> _
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "¨Z1"
    ''' <summary>
    ''' ¨Z1
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo1 As String
    ''' <summary>
    ''' ¨Z1
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¨Z1</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo1")> _
    Public Property BukkenJyuusyo1() As String
        Get
            Return strBukkenJyuusyo1
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "¨Z2"
    ''' <summary>
    ''' ¨Z2
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo2 As String
    ''' <summary>
    ''' ¨Z2
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¨Z2</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo2")> _
    Public Property BukkenJyuusyo2() As String
        Get
            Return strBukkenJyuusyo2
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "¨Z3"
    ''' <summary>
    ''' ¨Z3
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo3 As String
    ''' <summary>
    ''' ¨Z3
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¨Z3</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo3")> _
    Public Property BukkenJyuusyo3() As String
        Get
            Return strBukkenJyuusyo3
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo3 = value
        End Set
    End Property
#End Region

#Region "Á¿XR[h"
    ''' <summary>
    ''' Á¿XR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' Á¿XR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> Á¿XR[h</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "Á¿XæÁ"
    ''' <summary>
    ''' Á¿XæÁ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKtTorikesi As Integer
    ''' <summary>
    ''' Á¿XæÁ
    ''' </summary>
    ''' <value></value>
    ''' <returns>Á¿XæÁ</returns>
    ''' <remarks></remarks>
    <TableMap("kt_torikesi")> _
    Public Property KtTorikesi() As Integer
        Get
            Return intKtTorikesi
        End Get
        Set(ByVal value As Integer)
            intKtTorikesi = value
        End Set
    End Property
#End Region

#Region "Á¿XæÁR"
    ''' <summary>
    ''' Á¿XæÁR
    ''' </summary>
    ''' <remarks></remarks>
    Private strKtTorikesiRiyuu As String
    ''' <summary>
    ''' Á¿XæÁR
    ''' </summary>
    ''' <value></value>
    ''' <returns>Á¿XæÁR</returns>
    ''' <remarks></remarks>
    <TableMap("kt_torikesi_riyuu")> _
    Public Property KtTorikesiRiyuu() As String
        Get
            Return strKtTorikesiRiyuu
        End Get
        Set(ByVal value As String)
            strKtTorikesiRiyuu = value
        End Set
    End Property
#End Region

#Region "Á¿X¼1"
    ''' <summary>
    ''' Á¿X¼1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei1 As String
    ''' <summary>
    ''' Á¿X¼1
    ''' </summary>
    ''' <value></value>
    ''' <returns> Á¿X¼1</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei1")> _
    Public Property KameitenMei1() As String
        Get
            Return strKameitenMei1
        End Get
        Set(ByVal value As String)
            strKameitenMei1 = value
        End Set
    End Property
#End Region

#Region "²¸À{ú"
    ''' <summary>
    ''' ²¸À{ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysJissiDate As DateTime
    ''' <summary>
    ''' ²¸À{ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸À{ú</returns>
    ''' <remarks></remarks>
    <TableMap("tys_jissi_date")> _
    Public Property TysJissiDate() As DateTime
        Get
            Return dateTysJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateTysJissiDate = value
        End Set
    End Property
#End Region

#Region "ËSÒ"
    ''' <summary>
    ''' ËSÒ
    ''' </summary>
    ''' <remarks></remarks>
    Private strIraiTantousyaMei As String
    ''' <summary>
    ''' ËSÒ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ËSÒ</returns>
    ''' <remarks></remarks>
    <TableMap("irai_tantousya_mei")> _
    Public Property IraiTantousyaMei() As String
        Get
            Return strIraiTantousyaMei
        End Get
        Set(ByVal value As String)
            strIraiTantousyaMei = value
        End Set
    End Property
#End Region

#Region "Ëú"
    ''' <summary>
    ''' Ëú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDate As DateTime
    ''' <summary>
    ''' Ëú
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ëú</returns>
    ''' <remarks></remarks>
    <TableMap("irai_date")> _
    Public Property IraiDate() As DateTime
        Get
            Return dateIraiDate
        End Get
        Set(ByVal value As DateTime)
            dateIraiDate = value
        End Set
    End Property
#End Region

#Region "²¸ó]ú"
    ''' <summary>
    ''' ²¸ó]ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKibouDate As DateTime
    ''' <summary>
    ''' ²¸ó]ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ó]ú</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_date")> _
    Public Property TysKibouDate() As DateTime
        Get
            Return dateTysKibouDate
        End Get
        Set(ByVal value As DateTime)
            dateTysKibouDate = value
        End Set
    End Property
#End Region

#Region "\ñÏFLG"
    ''' <summary>
    ''' \ñÏFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoyakuZumiFlg As Integer
    ''' <summary>
    ''' \ñÏFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> \ñÏFLG</returns>
    ''' <remarks></remarks>
    <TableMap("yoyaku_zumi_flg")> _
    Public Property YoyakuZumiFlg() As Integer
        Get
            Return intYoyakuZumiFlg
        End Get
        Set(ByVal value As Integer)
            intYoyakuZumiFlg = value
        End Set
    End Property
#End Region

#Region "²¸ïÐR[h"
    ''' <summary>
    ''' ²¸ïÐR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ²¸ïÐR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ïÐR[h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd")> _
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "²¸ïÐÆR[h"
    ''' <summary>
    ''' ²¸ïÐÆR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' ²¸ïÐÆR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ïÐÆR[h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd")> _
    Public Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "²¸ïÐ¼"
    ''' <summary>
    ''' ²¸ïÐ¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaMei As String
    ''' <summary>
    ''' ²¸ïÐ¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ïÐ¼</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_mei")> _
    Public Property TysKaisyaMei() As String
        Get
            Return strTysKaisyaMei
        End Get
        Set(ByVal value As String)
            strTysKaisyaMei = value
        End Set
    End Property
#End Region

#Region "²¸û@¼Ì"
    ''' <summary>
    ''' ²¸û@¼Ì
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHouhouMei As String
    ''' <summary>
    ''' ²¸û@¼Ì
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸û@¼Ì</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_mei")> _
    Public Property TysHouhouMei() As String
        Get
            Return strTysHouhouMei
        End Get
        Set(ByVal value As String)
            strTysHouhouMei = value
        End Set
    End Property
#End Region

#Region "³ø²¸ú"
    ''' <summary>
    ''' ³ø²¸ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoudakusyoTysDate As DateTime
    ''' <summary>
    ''' ³ø²¸ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ³ø²¸ú</returns>
    ''' <remarks></remarks>
    <TableMap("syoudakusyo_tys_date")> _
    Public Property SyoudakusyoTysDate() As DateTime
        Get
            Return dateSyoudakusyoTysDate
        End Get
        Set(ByVal value As DateTime)
            dateSyoudakusyoTysDate = value
        End Set
    End Property
#End Region

#Region "²¸H±X¿z"
    ''' <summary>
    ''' ²¸H±X¿z
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenSeikyuuGaku As Integer
    ''' <summary>
    ''' ²¸H±X¿z
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸H±X¿z</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_seikyuu_gaku")> _
    Public Property KoumutenSeikyuuGaku() As Integer
        Get
            Return intKoumutenSeikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intKoumutenSeikyuuGaku = value
        End Set
    End Property
#End Region

#Region "²¸À¿z"
    ''' <summary>
    ''' ²¸À¿z
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriGaku As Integer
    ''' <summary>
    ''' ²¸À¿z
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸À¿z</returns>
    ''' <remarks></remarks>
    <TableMap("uri_gaku")> _
    Public Property UriGaku() As Integer
        Get
            Return intUriGaku
        End Get
        Set(ByVal value As Integer)
            intUriGaku = value
        End Set
    End Property
#End Region

#Region "²¸³øàz"
    ''' <summary>
    ''' ²¸³øàz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireGaku As Integer
    ''' <summary>
    ''' ²¸³øàz
    ''' </summary>
    ''' <value></value>
    ''' <returns> düàz</returns>
    ''' <remarks></remarks>
    <TableMap("siire_gaku")> _
    Public Property SiireGaku() As Integer
        Get
            Return intSiireGaku
        End Get
        Set(ByVal value As Integer)
            intSiireGaku = value
        End Set
    End Property
#End Region

#Region "SÒ¼"
    ''' <summary>
    ''' SÒ¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantousyaMei As String
    ''' <summary>
    ''' SÒ¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> SÒ¼</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_mei")> _
    Public Property TantousyaMei() As String
        Get
            Return strTantousyaMei
        End Get
        Set(ByVal value As String)
            strTantousyaMei = value
        End Set
    End Property
#End Region

#Region "³FÒ¼iHSÒj"
    ''' <summary>
    ''' ³FÒ¼iHSÒj
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouninsyaMei As String
    ''' <summary>
    ''' ³FÒ¼iHSÒj
    ''' </summary>
    ''' <value></value>
    ''' <returns> ³FÒ¼iHSÒj</returns>
    ''' <remarks></remarks>
    <TableMap("syouninsya_mei")> _
    Public Property SyouninsyaMei() As String
        Get
            Return strSyouninsyaMei
        End Get
        Set(ByVal value As String)
            strSyouninsyaMei = value
        End Set
    End Property
#End Region

#Region "»è1"
    ''' <summary>
    ''' »è1
    ''' </summary>
    ''' <remarks></remarks>
    Private strHantei1 As String
    ''' <summary>
    ''' »è1
    ''' </summary>
    ''' <value></value>
    ''' <returns> »è1</returns>
    ''' <remarks></remarks>
    <TableMap("hantei1")> _
    Public Property Hantei1() As String
        Get
            Return strHantei1
        End Get
        Set(ByVal value As String)
            strHantei1 = value
        End Set
    End Property
#End Region

#Region "»èÚ±¶"
    ''' <summary>
    ''' »èÚ±¶
    ''' </summary>
    ''' <remarks></remarks>
    Private strHanteiSetuzokuMoji As String
    ''' <summary>
    ''' »èÚ±¶
    ''' </summary>
    ''' <value></value>
    ''' <returns> »èÚ±¶</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_setuzoku_moji")> _
    Public Property HanteiSetuzokuMoji() As String
        Get
            Return strHanteiSetuzokuMoji
        End Get
        Set(ByVal value As String)
            strHanteiSetuzokuMoji = value
        End Set
    End Property
#End Region

#Region "»è2"
    ''' <summary>
    ''' »è2
    ''' </summary>
    ''' <remarks></remarks>
    Private strHantei2 As String
    ''' <summary>
    ''' »è2
    ''' </summary>
    ''' <value></value>
    ''' <returns> »è2</returns>
    ''' <remarks></remarks>
    <TableMap("hantei2")> _
    Public Property Hantei2() As String
        Get
            Return strHantei2
        End Get
        Set(ByVal value As String)
            strHantei2 = value
        End Set
    End Property
#End Region

#Region "væì¬ú"
    ''' <summary>
    ''' væì¬ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKeikakusyoSakuseiDate As DateTime
    ''' <summary>
    ''' væì¬ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> væì¬ú</returns>
    ''' <remarks></remarks>
    <TableMap("keikakusyo_sakusei_date")> _
    Public Property KeikakusyoSakuseiDate() As DateTime
        Get
            Return dateKeikakusyoSakuseiDate
        End Get
        Set(ByVal value As DateTime)
            dateKeikakusyoSakuseiDate = value
        End Set
    End Property
#End Region

#Region "ÛØ­sú"
    ''' <summary>
    ''' ÛØ­sú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakDate As DateTime
    ''' <summary>
    ''' ÛØ­sú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÛØ­sú</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_date")> _
    Public Property HosyousyoHakDate() As DateTime
        Get
            Return dateHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakDate = value
        End Set
    End Property
#End Region

#Region "õl"
    ''' <summary>
    ''' õl
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' õl
    ''' </summary>
    ''' <value></value>
    ''' <returns> õl</returns>
    ''' <remarks></remarks>
    <TableMap("bikou")> _
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "cÆSÒ"
    ''' <summary>
    ''' cÆSÒ
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyouTantousyaMei As String
    ''' <summary>
    ''' cÆSÒ
    ''' </summary>
    ''' <value></value>
    ''' <returns> cÆSÒ</returns>
    ''' <remarks></remarks>
    <TableMap("eigyou_tantousya_mei")> _
    Public Property EigyouTantousyaMei() As String
        Get
            Return strEigyouTantousyaMei
        End Get
        Set(ByVal value As String)
            strEigyouTantousyaMei = value
        End Set
    End Property
#End Region

#Region "HãNú"
    ''' <summary>
    ''' HãNú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojUriDate As DateTime
    ''' <summary>
    ''' HãNú
    ''' </summary>
    ''' <value></value>
    ''' <returns> HãNú</returns>
    ''' <remarks></remarks>
    <TableMap("koj_uri_date")> _
    Public Property KojUriDate() As DateTime
        Get
            Return dateKojUriDate
        End Get
        Set(ByVal value As DateTime)
            dateKojUriDate = value
        End Set
    End Property
#End Region

#Region "ª÷R[h"
    ''' <summary>
    ''' ª÷R[h
    ''' </summary>
    ''' <remarks></remarks>
    Private intBunjouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' ª÷R[h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("bunjou_cd")> _
    Public Property BunjouCd() As Integer
        Get
            Return intBunjouCd
        End Get
        Set(ByVal value As Integer)
            intBunjouCd = value
        End Set
    End Property
#End Region

#Region "¨¼ñR[h"
    ''' <summary>
    ''' ¨¼ñR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenNayoseCd As String
    ''' <summary>
    ''' ¨¼ñR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("bukken_nayose_cd")> _
    Public Property BukkenNayoseCd() As String
        Get
            Return strBukkenNayoseCd
        End Get
        Set(ByVal value As String)
            strBukkenNayoseCd = value
        End Set
    End Property
#End Region

#Region "_ñNO"
    ''' <summary>
    ''' _ñNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiyakuNo As String
    ''' <summary>
    ''' _ñNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> _ñNO</returns>
    ''' <remarks></remarks>
    <TableMap("keiyaku_no")> _
    Public Overridable Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
        End Set
    End Property
#End Region
End Class