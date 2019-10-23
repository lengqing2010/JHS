''' <summary>
''' TBL_Mﾃﾞｰﾀ区分テーブルのレコードクラスです
''' </summary>
''' <remarks>TBL_Mﾃﾞｰﾀ区分テーブルの項目プロパティ</remarks>
Public Class KubunRecord

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKubun As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分</returns>
    ''' <remarks></remarks>
    <TableMap("kbn", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property Kubun() As String
        Get
            Return strKubun
        End Get
        Set(ByVal value As String)
            strKubun = value
        End Set
    End Property
#End Region

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks>0以外:取り消し</remarks>
    Private intTorikeshi As Integer = Integer.MinValue
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns>取消</returns>
    ''' <remarks>0以外:取り消し</remarks>
    <TableMap("torikesi", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Torikeshi() As Integer
        Get
            Return intTorikeshi
        End Get
        Set(ByVal value As Integer)
            intTorikeshi = value
        End Set
    End Property
#End Region

#Region "区分名"
    ''' <summary>
    ''' 区分名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKubunMei As String
    ''' <summary>
    ''' 区分名
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分名</returns>
    ''' <remarks></remarks>
    <TableMap("kbn_mei", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Property KubunMei() As String
        Get
            Return strKubunMei
        End Get
        Set(ByVal value As String)
            strKubunMei = value
        End Set
    End Property
#End Region

#Region "原価マスタ非参照フラグ"
    ''' <summary>
    ''' 原価マスタ非参照フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intGenkaMasterHisansyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 原価マスタ非参照フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 原価マスタ非参照フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("genka_master_hisansyou_flg", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property GenkaMasterHisansyouFlg() As Integer
        Get
            Return intGenkaMasterHisansyouFlg
        End Get
        Set(ByVal value As Integer)
            intGenkaMasterHisansyouFlg = value
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
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
