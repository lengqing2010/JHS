''' <summary>
''' Äpãf[^e[uÌR[hNXÅ·
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_hannyou_uriage")> _
Public Class HannyouUriageRecord

#Region "Äpãj[NNO"
    ''' <summary>
    ''' Äpãj[NNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanUriUnqNo As Integer = 0
    ''' <summary>
    ''' Äpãj[NNO
    ''' </summary>
    ''' <value></value>
    ''' <returns>Äpãj[NNO</returns>
    ''' <remarks></remarks>
    <TableMap("han_uri_unique_no", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HanUriUnqNo() As Integer
        Get
            Return intHanUriUnqNo
        End Get
        Set(ByVal value As Integer)
            intHanUriUnqNo = value
        End Set
    End Property
#End Region

#Region "æÁ"
    ''' <summary>
    ''' æÁ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' æÁ
    ''' </summary>
    ''' <value></value>
    ''' <returns>æÁ</returns>
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

#Region "Ev"
    ''' <summary>
    ''' Ev
    ''' </summary>
    ''' <remarks></remarks>
    Private strTekiyou As String
    ''' <summary>
    ''' Ev
    ''' </summary>
    ''' <value></value>
    ''' <returns>Ev</returns>
    ''' <remarks></remarks>
    <TableMap("tekiyou", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=512)> _
    Public Property Tekiyou() As String
        Get
            Return strTekiyou
        End Get
        Set(ByVal value As String)
            strTekiyou = value
        End Set
    End Property
#End Region

#Region "ãNú"
    ''' <summary>
    ''' ãNú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriDate As DateTime
    ''' <summary>
    ''' ãNú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ãNú</returns>
    ''' <remarks></remarks>
    <TableMap("uri_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UriDate() As DateTime
        Get
            Return dateUriDate
        End Get
        Set(ByVal value As DateTime)
            dateUriDate = value
        End Set
    End Property
#End Region

#Region "`[ãNú"
    ''' <summary>
    ''' `[ãNú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouUriDate As DateTime
    ''' <summary>
    ''' `[ãNú
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[ãNú</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_uri_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property DenpyouUriDate() As DateTime
        Get
            Return dateDenpyouUriDate
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouUriDate = value
        End Set
    End Property
#End Region

#Region "¿Nú"
    ''' <summary>
    ''' ¿Nú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuuDate As DateTime
    ''' <summary>
    ''' ¿Nú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿Nú</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SeikyuuDate() As DateTime
        Get
            Return dateSeikyuuDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuuDate = value
        End Set
    End Property
#End Region

#Region "¿æR[h"
    ''' <summary>
    ''' ¿æR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' ¿æR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æR[h</returns>
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

#Region "¿æ}Ô"
    ''' <summary>
    ''' ¿æ}Ô
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' ¿æ}Ô
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æ}Ô</returns>
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

#Region "¿ææª"
    ''' <summary>
    ''' ¿ææª
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' ¿ææª
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿ææª</returns>
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

#Region "¿æ¼"
    ''' <summary>
    ''' ¿æ¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei As String
    ''' <summary>
    ''' ¿æ¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æ¼</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei")> _
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region

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
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "i¼"
    ''' <summary>
    ''' i¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strHinmei As String
    ''' <summary>
    ''' i¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> i¼</returns>
    ''' <remarks></remarks>
    <TableMap("hin_mei", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Property Hinmei() As String
        Get
            Return strHinmei
        End Get
        Set(ByVal value As String)
            strHinmei = value
        End Set
    End Property
#End Region

#Region "Ê"
    ''' <summary>
    ''' Ê
    ''' </summary>
    ''' <remarks></remarks>
    Private intSuu As Integer = Integer.MinValue
    ''' <summary>
    ''' Ê
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ê</returns>
    ''' <remarks></remarks>
    <TableMap("suu", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Suu() As Integer
        Get
            Return intSuu
        End Get
        Set(ByVal value As Integer)
            intSuu = value
        End Set
    End Property
#End Region

#Region "P¿"
    ''' <summary>
    ''' P¿
    ''' </summary>
    ''' <remarks></remarks>
    Private intTanka As Integer = Integer.MinValue
    ''' <summary>
    ''' P¿
    ''' </summary>
    ''' <value></value>
    ''' <returns> P¿</returns>
    ''' <remarks></remarks>
    <TableMap("tanka", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Tanka() As Integer
        Get
            Return intTanka
        End Get
        Set(ByVal value As Integer)
            intTanka = value
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
    <TableMap("zei_kbn", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=1)> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region

#Region "Å¦"
    ''' <summary>
    ''' Å¦
    ''' </summary>
    ''' <remarks></remarks>
    Private decZeiritu As Decimal = 0
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

#Region "ÁïÅz"
    ''' <summary>
    ''' ÁïÅz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhiZeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' ÁïÅz
    ''' </summary>
    ''' <value></value>
    ''' <returns>ÁïÅz</returns>
    ''' <remarks></remarks>
    <TableMap("syouhizei_gaku", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property SyouhiZeiGaku() As Integer
        Get
            Return intSyouhiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intSyouhiZeiGaku = value
        End Set
    End Property
#End Region

#Region "ãàz[P¿*Ê*Å¦]"
    ''' <summary>
    ''' ãàz[P¿*Ê*Å¦]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngUriGaku As Long = 0
    ''' <summary>
    ''' ãàz[P¿*Ê*Å¦]
    ''' </summary>
    ''' <value></value>
    ''' <returns>ãàz[P¿*Ê*Å¦]</returns>
    ''' <remarks></remarks>
    Public Property UriGaku() As Long
        Get
            Dim tmpUriGaku As Long
            Dim tmpTanka As Long = Long.Parse(Tanka)
            Dim tmpSuu As Long = Long.Parse(Suu)

            If Suu = Integer.MinValue _
                OrElse Tanka = Integer.MinValue Then
                tmpUriGaku = 0
            Else
                tmpUriGaku = tmpSuu * tmpTanka 'Ê*P¿
            End If
            If SyouhiZeiGaku = Integer.MinValue Then
                tmpUriGaku = Fix(tmpUriGaku * (1 + Zeiritu)) '1+Å¦
            Else
                tmpUriGaku = Fix(tmpUriGaku + SyouhiZeiGaku) 'ÁïÅz
            End If

            Return tmpUriGaku
        End Get
        Set(ByVal value As Long)
            lngUriGaku = value
        End Set
    End Property
#End Region

#Region "ãvãFLG"
    ''' <summary>
    ''' ãvãFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKeijyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' ãvãFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> ãvãFLG</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_flg", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property UriKeijyouFlg() As Integer
        Get
            Return intUriKeijyouFlg
        End Get
        Set(ByVal value As Integer)
            intUriKeijyouFlg = value
        End Set
    End Property
#End Region

#Region "ãvãú"
    ''' <summary>
    ''' ãvãú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriKeijyouDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' ãvãú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ãvãú</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_date", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
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

#Region "o^OC[U[¼"
    ''' <summary>
    ''' o^OC[U[¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserName As String
    ''' <summary>
    ''' o^OC[U[¼
    ''' </summary>
    ''' <value></value>
    ''' <returns>o^OC[U[¼</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_name", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=128)> _
    Public Property AddLoginUserName() As String
        Get
            Return strAddLoginUserName
        End Get
        Set(ByVal value As String)
            strAddLoginUserName = value
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

#Region "XVOC[U[¼"
    ''' <summary>
    ''' XVOC[U[¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserName As String
    ''' <summary>
    ''' XVOC[U[¼
    ''' </summary>
    ''' <value></value>
    ''' <returns>XVOC[U[¼</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_name", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=128)> _
    Public Property UpdLoginUserName() As String
        Get
            Return strUpdLoginUserName
        End Get
        Set(ByVal value As String)
            strUpdLoginUserName = value
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
    <TableMap("kbn", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "Ô"
    ''' <summary>
    ''' Ô
    ''' </summary>
    ''' <remarks></remarks>
    Private strBangou As String
    ''' <summary>
    ''' Ô
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ô</returns>
    ''' <remarks></remarks>
    <TableMap("bangou", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property Bangou() As String
        Get
            Return strBangou
        End Get
        Set(ByVal value As String)
            strBangou = value
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
    <TableMap("sesyu_mei", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "ãXæª"
    ''' <summary>
    ''' ãXæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strUriageTenKbn As String
    ''' <summary>
    ''' ãXæª
    ''' </summary>
    ''' <value></value>
    ''' <returns> ãXæª</returns>
    ''' <remarks></remarks>
    <TableMap("uriage_ten_kbn", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property UriageTenKbn() As String
        Get
            Return strUriageTenKbn
        End Get
        Set(ByVal value As String)
            strUriageTenKbn = value
        End Set
    End Property
#End Region

#Region "ãXR[h"
    ''' <summary>
    ''' ãXR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strUriageTenCd As String
    ''' <summary>
    ''' ãXR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ãXR[h</returns>
    ''' <remarks></remarks>
    <TableMap("uriage_ten_cd", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=7)> _
    Public Property UriageTenCd() As String
        Get
            Return strUriageTenCd
        End Get
        Set(ByVal value As String)
            strUriageTenCd = value
        End Set
    End Property
#End Region


End Class