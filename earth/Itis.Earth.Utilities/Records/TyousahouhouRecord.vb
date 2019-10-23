''' <summary>
''' 調査方法マスタのレコードクラスです
''' </summary>
''' <remarks></remarks>
Public Class TyousahouhouRecord

#Region "調査方法NO"
    ''' <summary>
    ''' 調査方法NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHouhouNo As Integer = Integer.MinValue
    ''' <summary>
    ''' 調査方法NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査方法NO</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_no", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property TysHouhouNo() As Integer
        Get
            Return intTysHouhouNo
        End Get
        Set(ByVal value As Integer)
            intTysHouhouNo = value
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
    <TableMap("torikesi", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "調査方法略称"
    ''' <summary>
    ''' 調査方法略称
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHouhouMeiRyaku As String
    ''' <summary>
    ''' 調査方法略称
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査方法略称</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_mei_ryaku", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Property TysHouhouMeiRyaku() As String
        Get
            Return strTysHouhouMeiRyaku
        End Get
        Set(ByVal value As String)
            strTysHouhouMeiRyaku = value
        End Set
    End Property
#End Region

#Region "調査方法名称"
    ''' <summary>
    ''' 調査方法名称
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHouhouMei As String
    ''' <summary>
    ''' 調査方法名称
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査方法名称</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_mei", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Property TysHouhouMei() As String
        Get
            Return strTysHouhouMei
        End Get
        Set(ByVal value As String)
            strTysHouhouMei = value
        End Set
    End Property
#End Region

#Region "原価設定不要フラグ"
    ''' <summary>
    ''' 原価設定不要フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intGenkaSetteiFuyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 原価設定不要フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns>原価設定不要フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("genka_settei_fuyou_flg", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property GenkaSetteiFuyouFlg() As Integer
        Get
            Return intGenkaSetteiFuyouFlg
        End Get
        Set(ByVal value As Integer)
            intGenkaSetteiFuyouFlg = value
        End Set
    End Property
#End Region

#Region "価格設定不要フラグ"
    ''' <summary>
    ''' 価格設定不要フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakakuSetteiFuyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 価格設定不要フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns>価格設定不要フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("kakaku_settei_fuyou_flg", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property KakakuSetteiFuyouFlg() As Integer
        Get
            Return intKakakuSetteiFuyouFlg
        End Get
        Set(ByVal value As Integer)
            intKakakuSetteiFuyouFlg = value
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

