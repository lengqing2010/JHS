''' <summary>
''' ¿R[hNX/¿êæÊA¿óàeÒWæÊ
''' ¦u¿óüvp
''' </summary>
''' <remarks>¿f[^Ìi[ÉgpµÜ·</remarks>
<TableClassMap("t_seikyuu_kagami")> _
Public Class SeikyuuDataHakkouRecord
    Inherits SeikyuuDataRecord

#Region "¿NO"
    ''' <summary>
    ''' ¿NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusyoNo As String
    ''' <summary>
    ''' ¿NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=15)> _
    Public Overrides Property SeikyuusyoNo() As String
        Get
            Return strSeikyuusyoNo
        End Get
        Set(ByVal value As String)
            strSeikyuusyoNo = value
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
    <TableMap("torikesi", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "¿óüú"
    ''' <summary>
    ''' ¿óüú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoInsatuDate As DateTime
    ''' <summary>
    ''' ¿óüú
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_insatu_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property SeikyuusyoInsatuDate() As DateTime
        Get
            Return dateSeikyuusyoInsatuDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoInsatuDate = value
        End Set
    End Property
#End Region

End Class
