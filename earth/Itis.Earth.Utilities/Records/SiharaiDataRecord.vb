''' <summary>
''' 支払データテーブルのレコードクラスです
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_siharai_data")> _
Public Class SiharaiDataRecord

#Region "伝票ユニークNO"
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenUnqNo As Integer
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no")> _
    Public Property DenUnqNo() As Integer
        Get
            Return intDenUnqNo
        End Get
        Set(ByVal value As Integer)
            intDenUnqNo = value
        End Set
    End Property
#End Region

#Region "伝票NO"
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNo As String
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票NO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_no")> _
    Public Property DenNo() As String
        Get
            Return strDenNo
        End Get
        Set(ByVal value As String)
            strDenNo = value
        End Set
    End Property
#End Region

#Region "伝票種別"
    ''' <summary>
    ''' 伝票種別
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenSyubetu As String
    ''' <summary>
    ''' 伝票種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票種別</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_syubetu")> _
    Public Property DenSyubetu() As String
        Get
            Return strDenSyubetu
        End Get
        Set(ByVal value As String)
            strDenSyubetu = value
        End Set
    End Property
#End Region

#Region "取消元伝票ユニークNO"
    ''' <summary>
    ''' 取消元伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intToriMotoDenUnqNo As Integer
    ''' <summary>
    ''' 取消元伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消元伝票ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi_moto_denpyou_unique_no")> _
    Public Property ToriMotoDenUnqNo() As Integer
        Get
            Return intToriMotoDenUnqNo
        End Get
        Set(ByVal value As Integer)
            intToriMotoDenUnqNo = value
        End Set
    End Property
#End Region

#Region "新会計仕訳ユニークNO"
    ''' <summary>
    ''' 新会計仕訳ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intSkkSiwakeUnqNo As Integer
    ''' <summary>
    ''' 新会計仕訳ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新会計仕訳ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("skk_siwake_unique_no")> _
    Public Property SkkSiwakeUnqNo() As Integer
        Get
            Return intSkkSiwakeUnqNo
        End Get
        Set(ByVal value As Integer)
            intSkkSiwakeUnqNo = value
        End Set
    End Property
#End Region

#Region "新会計事業所コード"
    ''' <summary>
    ''' 新会計事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkJigyouCd As String
    ''' <summary>
    ''' 新会計事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新会計事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("skk_jigyou_cd")> _
    Public Property SkkJigyouCd() As String
        Get
            Return strSkkJigyouCd
        End Get
        Set(ByVal value As String)
            strSkkJigyouCd = value
        End Set
    End Property
#End Region

#Region "新会計支払先コード"
    ''' <summary>
    ''' 新会計支払先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkShriSakiCd As String
    ''' <summary>
    ''' 新会計支払先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新会計支払先コード</returns>
    ''' <remarks></remarks>
    <TableMap("skk_shri_saki_cd")> _
    Public Property SkkShriSakiCd() As String
        Get
            Return strSkkShriSakiCd
        End Get
        Set(ByVal value As String)
            strSkkShriSakiCd = value
        End Set
    End Property
#End Region

#Region "支払先名"
    ''' <summary>
    ''' 支払先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriSakiMei As String
    ''' <summary>
    ''' 支払先名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払先名</returns>
    ''' <remarks></remarks>
    <TableMap("shri_saki_mei")> _
    Public Property ShriSakiMei() As String
        Get
            Return strShriSakiMei
        End Get
        Set(ByVal value As String)
            strShriSakiMei = value
        End Set
    End Property
#End Region

#Region "支払年月日"
    ''' <summary>
    ''' 支払年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateShriDate As DateTime
    ''' <summary>
    ''' 支払年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払年月日</returns>
    ''' <remarks></remarks>
    <TableMap("siharai_date")> _
    Public Property ShriDate() As DateTime
        Get
            Return dateShriDate
        End Get
        Set(ByVal value As DateTime)
            dateShriDate = value
        End Set
    End Property
#End Region

#Region "入金額 [振込]"
    ''' <summary>
    ''' 入金額 [振込]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomi As Long
    ''' <summary>
    ''' 入金額 [振込]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [振込]</returns>
    ''' <remarks></remarks>
    <TableMap("furikomi")> _
    Public Property Furikomi() As Long
        Get
            Return lngFurikomi
        End Get
        Set(ByVal value As Long)
            lngFurikomi = value
        End Set
    End Property
#End Region

#Region "入金額 [相殺]"
    ''' <summary>
    ''' 入金額 [相殺]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSousai As Long
    ''' <summary>
    ''' 入金額 [相殺]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額 [相殺]</returns>
    ''' <remarks></remarks>
    <TableMap("sousai")> _
    Public Property Sousai() As Long
        Get
            Return lngSousai
        End Get
        Set(ByVal value As Long)
            lngSousai = value
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
    <TableMap("tekiyou_mei")> _
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

#Region "登録ログインユーザー名"
    ''' <summary>
    ''' 登録ログインユーザー名
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserName As String
    ''' <summary>
    ''' 登録ログインユーザー名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ログインユーザー名</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_name")> _
    Public Property AddLoginUserName() As String
        Get
            Return strAddLoginUserName
        End Get
        Set(ByVal value As String)
            strAddLoginUserName = value
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

End Class