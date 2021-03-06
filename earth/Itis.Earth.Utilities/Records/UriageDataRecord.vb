''' <summary>
''' ãf[^e[uÌR[hNXÅ·
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_uriage_data")> _
Public Class UriageDataRecord

#Region "`[j[NNO"
    ''' <summary>
    ''' `[j[NNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenUnqNo As Integer
    ''' <summary>
    ''' `[j[NNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[j[NNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property DenUnqNo() As Integer
        Get
            Return intDenUnqNo
        End Get
        Set(ByVal value As Integer)
            intDenUnqNo = value
        End Set
    End Property
#End Region

#Region "`[NO"
    ''' <summary>
    ''' `[NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNo As String
    ''' <summary>
    ''' `[NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[NO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_no", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=5)> _
    Public Property DenNo() As String
        Get
            Return strDenNo
        End Get
        Set(ByVal value As String)
            strDenNo = value
        End Set
    End Property
#End Region

#Region "`[íÊ"
    ''' <summary>
    ''' `[íÊ
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenSyubetu As String
    ''' <summary>
    ''' `[íÊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[íÊ</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_syubetu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Property DenSyubetu() As String
        Get
            Return strDenSyubetu
        End Get
        Set(ByVal value As String)
            strDenSyubetu = value
        End Set
    End Property
#End Region

#Region "æÁ³`[j[NNO"
    ''' <summary>
    ''' æÁ³`[j[NNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strToriMotoDenUnqNo As Integer
    ''' <summary>
    ''' æÁ³`[j[NNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> æÁ³`[j[NNO</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi_moto_denpyou_unique_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property ToriMotoDenUnqNo() As Integer
        Get
            Return strToriMotoDenUnqNo
        End Get
        Set(ByVal value As Integer)
            strToriMotoDenUnqNo = value
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
    <TableMap("kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    <TableMap("bangou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property Bangou() As String
        Get
            Return strBangou
        End Get
        Set(ByVal value As String)
            strBangou = value
        End Set
    End Property
#End Region

#Region "Rt¯R[h"
    ''' <summary>
    ''' Rt¯R[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strHimodukeCd As String
    ''' <summary>
    ''' Rt¯R[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> Rt¯R[h</returns>
    ''' <remarks></remarks>
    <TableMap("himoduke_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property HimodukeCd() As String
        Get
            Return strHimodukeCd
        End Get
        Set(ByVal value As String)
            strHimodukeCd = value
        End Set
    End Property
#End Region

#Region "Rt¯³e[uíÊ"
    ''' <summary>
    ''' Rt¯³e[uíÊ
    ''' </summary>
    ''' <remarks></remarks>
    Private strHimodukeMotoTblSyubetu As String
    ''' <summary>
    ''' Rt¯³e[uíÊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> Rt¯³e[uíÊ</returns>
    ''' <remarks></remarks>
    <TableMap("himoduke_table_type", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HimodukeMotoTblSyubetu() As Integer
        Get
            Return strHimodukeMotoTblSyubetu
        End Get
        Set(ByVal value As Integer)
            strHimodukeMotoTblSyubetu = value
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
    <TableMap("uri_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    Private dateDenUriDate As DateTime
    ''' <summary>
    ''' `[ãNú
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[ãNú</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_uri_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property DenUriDate() As DateTime
        Get
            Return dateDenUriDate
        End Get
        Set(ByVal value As DateTime)
            dateDenUriDate = value
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
    <TableMap("seikyuu_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
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
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
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
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    <TableMap("seikyuu_saki_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
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
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
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
    <TableMap("hinmei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    Private intSuu As Integer
    ''' <summary>
    ''' Ê
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ê</returns>
    ''' <remarks></remarks>
    <TableMap("suu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Suu() As Integer
        Get
            Return intSuu
        End Get
        Set(ByVal value As Integer)
            intSuu = value
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
    <TableMap("tani", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Property Tani() As String
        Get
            Return strTani
        End Get
        Set(ByVal value As String)
            strTani = value
        End Set
    End Property
#End Region

#Region "P¿"
    ''' <summary>
    ''' P¿
    ''' </summary>
    ''' <remarks></remarks>
    Private intTanka As Integer
    ''' <summary>
    ''' P¿
    ''' </summary>
    ''' <value></value>
    ''' <returns> P¿</returns>
    ''' <remarks></remarks>
    <TableMap("tanka", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Tanka() As Integer
        Get
            Return intTanka
        End Get
        Set(ByVal value As Integer)
            intTanka = value
        End Set
    End Property
#End Region

#Region "ãàz"
    ''' <summary>
    ''' ãàz
    ''' </summary>
    ''' <remarks></remarks>
    Private lngUriGaku As Long
    ''' <summary>
    ''' ãàz
    ''' </summary>
    ''' <value></value>
    ''' <returns> ãàz</returns>
    ''' <remarks></remarks>
    <TableMap("uri_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property UriGaku() As Long
        Get
            Return lngUriGaku
        End Get
        Set(ByVal value As Long)
            lngUriGaku = value
        End Set
    End Property
#End Region

#Region "OÅz"
    ''' <summary>
    ''' OÅz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSotozeiGaku As Integer
    ''' <summary>
    ''' OÅz
    ''' </summary>
    ''' <value></value>
    ''' <returns> OÅz</returns>
    ''' <remarks></remarks>
    <TableMap("sotozei_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property SotozeiGaku() As Integer
        Get
            Return intSotozeiGaku
        End Get
        Set(ByVal value As Integer)
            intSotozeiGaku = value
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
    <TableMap("zei_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=1)> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
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
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
