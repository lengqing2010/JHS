''' <summary>
''' ¤iîñÌR[hNXÅ·
''' </summary>
''' <remarks></remarks>
Public Class SyouhinMeisaiRecord

#Region "¤iR[h"
    ''' <summary>
    ''' ¤iR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' ¤iR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤iR[h</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd")> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "¤i¼"
    ''' <summary>
    ''' ¤i¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinMei As String
    ''' <summary>
    ''' ¤i¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤i¼</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Property SyouhinMei() As String
        Get
            Return strSyouhinMei
        End Get
        Set(ByVal value As String)
            strSyouhinMei = value
        End Set
    End Property
#End Region

#Region "æÁ"
    ''' <summary>
    ''' æÁ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer
    ''' <summary>
    ''' æÁ
    ''' </summary>
    ''' <value></value>
    ''' <returns> æÁ</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi")> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "PÊ"
    ''' <summary>
    ''' PÊ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTani As String
    ''' <summary>
    ''' PÊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> PÊ</returns>
    ''' <remarks></remarks>
    <TableMap("tani")> _
    Public Property Tani() As String
        Get
            Return strTani
        End Get
        Set(ByVal value As String)
            strTani = value
        End Set
    End Property
#End Region

#Region "¿ææª"
    ''' <summary>
    ''' ¿ææª
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusakiKbn As String
    ''' <summary>
    ''' ¿ææª
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿ææª</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusaki_kbn")> _
    Public Property SeikyuusakiKbn() As String
        Get
            Return strSeikyuusakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuusakiKbn = value
        End Set
    End Property
#End Region

#Region "x¥p¤i¼"
    ''' <summary>
    ''' x¥p¤i¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSiharaiyouSyouhinMei As String
    ''' <summary>
    ''' x¥p¤i¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> x¥p¤i¼</returns>
    ''' <remarks></remarks>
    <TableMap("siharaiyou_syouhin_mei")> _
    Public Property SiharaiyouSyouhinMei() As String
        Get
            Return strSiharaiyouSyouhinMei
        End Get
        Set(ByVal value As String)
            strSiharaiyouSyouhinMei = value
        End Set
    End Property
#End Region

#Region "¤iæª1"
    ''' <summary>
    ''' ¤iæª1
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinKbn1 As String
    ''' <summary>
    ''' ¤iæª1
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤iæª1</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_kbn1")> _
    Public Property SyouhinKbn1() As String
        Get
            Return strSyouhinKbn1
        End Get
        Set(ByVal value As String)
            strSyouhinKbn1 = value
        End Set
    End Property
#End Region

#Region "¤iæª2"
    ''' <summary>
    ''' ¤iæª2
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinKbn2 As String
    ''' <summary>
    ''' ¤iæª2
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤iæª2</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_kbn2")> _
    Public Property SyouhinKbn2() As String
        Get
            Return strSyouhinKbn2
        End Get
        Set(ByVal value As String)
            strSyouhinKbn2 = value
        End Set
    End Property
#End Region

#Region "¤iæª3"
    ''' <summary>
    ''' ¤iæª3
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinKbn3 As String
    ''' <summary>
    ''' ¤iæª3
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤iæª3</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_kbn3")> _
    Public Property SyouhinKbn3() As String
        Get
            Return strSyouhinKbn3
        End Get
        Set(ByVal value As String)
            strSyouhinKbn3 = value
        End Set
    End Property
#End Region

#Region "ÛØL³"
    ''' <summary>
    ''' ÛØL³
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouUmu As Integer
    ''' <summary>
    ''' ÛØL³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÛØL³</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_umu")> _
    Public Property HosyouUmu() As Integer
        Get
            Return intHosyouUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouUmu = value
        End Set
    End Property
#End Region

#Region "Åæª"
    ''' <summary>
    ''' Åæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strZeiKbn As String
    ''' <summary>
    ''' Åæª
    ''' </summary>
    ''' <value></value>
    ''' <returns> Åæª</returns>
    ''' <remarks></remarks>
    <TableMap("zei_kbn")> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region

#Region "Åæª"
    ''' <summary>
    ''' Åæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strZeikomiKbn As String
    ''' <summary>
    ''' Åæª
    ''' </summary>
    ''' <value></value>
    ''' <returns> Åæª</returns>
    ''' <remarks></remarks>
    <TableMap("zeikomi_kbn")> _
    Public Property ZeikomiKbn() As String
        Get
            Return strZeikomiKbn
        End Get
        Set(ByVal value As String)
            strZeikomiKbn = value
        End Set
    End Property
#End Region

#Region "W¿i"
    ''' <summary>
    ''' W¿i
    ''' </summary>
    ''' <remarks></remarks>
    Private intHyoujunKkk As Integer
    ''' <summary>
    ''' W¿i
    ''' </summary>
    ''' <value></value>
    ''' <returns> W¿i</returns>
    ''' <remarks></remarks>
    <TableMap("hyoujun_kkk")> _
    Public Property HyoujunKkk() As Integer
        Get
            Return intHyoujunKkk
        End Get
        Set(ByVal value As Integer)
            intHyoujunKkk = value
        End Set
    End Property
#End Region

#Region "nñR[h"
    ''' <summary>
    ''' nñR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' nñR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> nñR[h</returns>
    ''' <remarks></remarks>
    <TableMap("keiretu_cd")> _
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region

#Region "r_[¿z"
    ''' <summary>
    ''' r_[¿z
    ''' </summary>
    ''' <remarks></remarks>
    Private intBuilderSeikyuugaku As Integer
    ''' <summary>
    ''' r_[¿z
    ''' </summary>
    ''' <value></value>
    ''' <returns> r_[¿z</returns>
    ''' <remarks></remarks>
    <TableMap("builder_seikyuugaku")> _
    Public Property BuilderSeikyuugaku() As Integer
        Get
            Return intBuilderSeikyuugaku
        End Get
        Set(ByVal value As Integer)
            intBuilderSeikyuugaku = value
        End Set
    End Property
#End Region

#Region "qÉR[h"
    ''' <summary>
    ''' qÉR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCd As String
    ''' <summary>
    ''' qÉR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> qÉR[h</returns>
    ''' <remarks></remarks>
    <TableMap("souko_cd")> _
    Public Property SoukoCd() As String
        Get
            Return strSoukoCd
        End Get
        Set(ByVal value As String)
            strSoukoCd = value
        End Set
    End Property
#End Region

#Region "dü¿i"
    ''' <summary>
    ''' dü¿i
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireKkk As Integer
    ''' <summary>
    ''' dü¿i
    ''' </summary>
    ''' <value></value>
    ''' <returns> dü¿i</returns>
    ''' <remarks></remarks>
    <TableMap("siire_kkk")> _
    Public Property SiireKkk() As Integer
        Get
            Return intSiireKkk
        End Get
        Set(ByVal value As Integer)
            intSiireKkk = value
        End Set
    End Property
#End Region

#Region "H^Cv"
    ''' <summary>
    ''' H^Cv
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojType As String
    ''' <summary>
    ''' H^Cv
    ''' </summary>
    ''' <value></value>
    ''' <returns> H^Cv</returns>
    ''' <remarks></remarks>
    <TableMap("koj_type")> _
    Public Property KojType() As String
        Get
            Return strKojType
        End Get
        Set(ByVal value As String)
            strKojType = value
        End Set
    End Property
#End Region

#Region "²¸L³æª"
    ''' <summary>
    ''' ²¸L³æª
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysUmuKbn As Integer
    ''' <summary>
    ''' ²¸L³æª
    ''' </summary>
    ''' <value></value>
    ''' <returns>²¸L³æª </returns>
    ''' <remarks></remarks>
    <TableMap("tys_umu_kbn")> _
    Public Property TysUmuKbn() As Integer
        Get
            Return intTysUmuKbn
        End Get
        Set(ByVal value As Integer)
            intTysUmuKbn = value
        End Set
    End Property
#End Region

#Region "o^OC[U[ID"
    ''' <summary>
    ''' o^OC[U[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' o^OC[U[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> o^OC[U[ID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id")> _
    Public Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "o^ú"
    ''' <summary>
    ''' o^ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' o^ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> o^ú</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime")> _
    Public Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
        End Set
    End Property
#End Region

#Region "XVOC[U[ID"
    ''' <summary>
    ''' XVOC[U[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' XVOC[U[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVOC[U[ID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id")> _
    Public Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "XVú"
    ''' <summary>
    ''' XVú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' XVú
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVú</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime")> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "Å¦"
    ''' <summary>
    ''' Å¦
    ''' </summary>
    ''' <remarks></remarks>
    Private decZeiritu As Decimal
    ''' <summary>
    ''' Å¦
    ''' </summary>
    ''' <value></value>
    ''' <returns> Å¦</returns>
    ''' <remarks></remarks>
    <TableMap("zeiritu")> _
    Public Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
        End Set
    End Property
#End Region

#Region "Á¿XR[h"
    ''' <summary>
    ''' Á¿XR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCdDisp As String
    ''' <summary>
    ''' Á¿XR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> Á¿XR[h</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCdDisp() As String
        Get
            Return strKameitenCdDisp
        End Get
        Set(ByVal value As String)
            strKameitenCdDisp = value
        End Set
    End Property
#End Region

#Region "¿æR[h(î{)"
    ''' <summary>
    ''' ¿æR[h(î{)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCdDisp As String
    ''' <summary>
    ''' ¿æR[h(î{)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æR[h(î{)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd")> _
    Public Property SeikyuuSakiCdDisp() As String
        Get
            Return strSeikyuuSakiCdDisp
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCdDisp = value
        End Set
    End Property
#End Region

#Region "¿æ}Ô(î{)"
    ''' <summary>
    ''' ¿æ}Ô(î{)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrcDisp As String
    ''' <summary>
    ''' ¿æ}Ô(î{)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æ}Ô(î{)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc")> _
    Public Property SeikyuuSakiBrcDisp() As String
        Get
            Return strSeikyuuSakiBrcDisp
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrcDisp = value
        End Set
    End Property
#End Region

#Region "¿ææª(î{)"
    ''' <summary>
    ''' ¿ææª(î{)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbnDisp As String
    ''' <summary>
    ''' ¿ææª(î{)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿ææª(î{)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiKbnDisp() As String
        Get
            Return strSeikyuuSakiKbnDisp
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbnDisp = value
        End Set
    End Property
#End Region

#Region "¿æ^Cv(¼Úor¼¿)"
    ''' <summary>
    ''' ¿æ^Cv(¼Úor¼¿)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiType As String
    ''' <summary>
    ''' ¿æ^Cv(¼Úor¼¿)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æ^Cv(¼Úor¼¿)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiType() As String
        Get
            If KameitenCdDisp <> String.Empty AndAlso _
               SeikyuuSakiCdDisp <> String.Empty AndAlso _
               KameitenCdDisp = SeikyuuSakiCdDisp Then
                Return EarthConst.SEIKYU_TYOKUSETU
            Else
                Return EarthConst.SEIKYU_TASETU
            End If
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiType = value
        End Set
    End Property
#End Region

#Region "SDS©®Ýè"
    ''' <summary>
    ''' SDS©®Ýè
    ''' </summary>
    ''' <remarks></remarks>
    Private intSdsJidouSet As Integer
    ''' <summary>
    ''' SDS©®Ýè
    ''' </summary>
    ''' <value></value>
    ''' <returns>²¸L³æª </returns>
    ''' <remarks></remarks>
    <TableMap("sds_jidou_set")> _
    Public Property SdsJidouSet() As Integer
        Get
            Return intSdsJidouSet
        End Get
        Set(ByVal value As Integer)
            intSdsJidouSet = value
        End Set
    End Property
#End Region

End Class