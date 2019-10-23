''' <summary>
''' 日付Saveマスタ用レコードクラスです
''' </summary>
''' <remarks></remarks>
<TableClassMap("m_hiduke_save")> _
Public Class HidukeSaveRecord

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
    <TableMap("kbn", IsKey:=True, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "保証書発行日"
    ''' <summary>
    ''' 保証書発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private _hosyousyoHakDate As DateTime
    ''' <summary>
    ''' 保証書発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns>保証書発行日</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HosyousyoHakDate() As DateTime
        Get
            Return _hosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            _hosyousyoHakDate = value
        End Set
    End Property
#End Region

#Region "報告書発送日"
    ''' <summary>
    ''' 報告書発送日
    ''' </summary>
    ''' <remarks></remarks>
    Private _hkksHassouDate As DateTime
    ''' <summary>
    ''' 報告書発送日
    ''' </summary>
    ''' <value></value>
    ''' <returns>報告書発送日</returns>
    ''' <remarks></remarks>
    <TableMap("hkks_hassou_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HkksHassouDate() As DateTime
        Get
            Return _hkksHassouDate
        End Get
        Set(ByVal value As DateTime)
            _hkksHassouDate = value
        End Set
    End Property
#End Region

#Region "保証書NO年月"
    ''' <summary>
    ''' 保証書NO年月
    ''' </summary>
    ''' <remarks></remarks>
    Private _hosyousyoNoNengetu As DateTime
    ''' <summary>
    ''' 保証書NO年月
    ''' </summary>
    ''' <value></value>
    ''' <returns>保証書NO年月</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no_nengetu", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HosyousyoNoNengetu() As DateTime
        Get
            Return _hosyousyoNoNengetu
        End Get
        Set(ByVal value As DateTime)
            _hosyousyoNoNengetu = value
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

#Region "更新ﾛｸﾞｲﾝﾕｰｻﾞｰID"
    ''' <summary>
    ''' 更新ﾛｸﾞｲﾝﾕｰｻﾞｰID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' 更新ﾛｸﾞｲﾝﾕｰｻﾞｰID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ﾛｸﾞｲﾝﾕｰｻﾞｰID</returns>
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
    ''' <remarks>排他制御を行う為、検索時の更新日付を設定してください<br/>
    '''          更新時の日付はシステム日付が設定されます</remarks>
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
