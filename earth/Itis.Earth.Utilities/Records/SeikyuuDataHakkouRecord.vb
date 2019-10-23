''' <summary>
''' 請求レコードクラス/請求書一覧画面、請求書印字内容編集画面
''' ※「請求書印刷」処理用
''' </summary>
''' <remarks>請求データの格納時に使用します</remarks>
<TableClassMap("t_seikyuu_kagami")> _
Public Class SeikyuuDataHakkouRecord
    Inherits SeikyuuDataRecord

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
    Public Overrides Property SeikyuusyoNo() As String
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

#Region "請求書印刷日"
    ''' <summary>
    ''' 請求書印刷日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoInsatuDate As DateTime
    ''' <summary>
    ''' 請求書印刷日
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
