<TableClassMap("t_teibetu_nyuukin")> _
Public Class TeibetuNyuukinRecord

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
    <TableMap("kbn", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "保証書NO"
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "分類ｺｰﾄﾞ"
    ''' <summary>
    ''' 分類ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String
    ''' <summary>
    ''' 分類ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 分類ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("bunrui_cd", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "画面表示NO"
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intGamenHyoujiNo As Integer
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 分類ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("gamen_hyouji_no", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property GamenHyoujiNo() As Integer
        Get
            Return intGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            intGamenHyoujiNo = value
        End Set
    End Property
#End Region

#Region "入金金額"
    ''' <summary>
    ''' 入金金額
    ''' </summary>
    ''' <remarks>税込入金金額 - 税込返金金額</remarks>
    Private intNyuukinGaku As Integer
    ''' <summary>
    ''' 入金金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金金額</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_gaku")> _
    Public Property NyuukinGaku() As Integer
        Get
            Return intNyuukinGaku
        End Get
        Set(ByVal value As Integer)
            intNyuukinGaku = value
        End Set
    End Property
#End Region

#Region "税込入金金額"
    ''' <summary>
    ''' 税込入金金額
    ''' </summary>
    ''' <remarks>税込入金金額</remarks>
    Private intZeikomiNyuukinGaku As Integer
    ''' <summary>
    ''' 税込入金金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税込入金金額</returns>
    ''' <remarks></remarks>
    <TableMap("zeikomi_nyuukin_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property ZeikomiNyuukinGaku() As Integer
        Get
            Return intZeikomiNyuukinGaku
        End Get
        Set(ByVal value As Integer)
            intZeikomiNyuukinGaku = value
        End Set
    End Property
#End Region

#Region "税込返金金額"
    ''' <summary>
    ''' 税込返金金額
    ''' </summary>
    ''' <remarks>税込返金金額</remarks>
    Private intZeikomiHenkinGaku As Integer
    ''' <summary>
    ''' 税込返金金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税込返金金額</returns>
    ''' <remarks></remarks>
    <TableMap("zeikomi_henkin_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property ZeikomiHenkinGaku() As Integer
        Get
            Return intZeikomiHenkinGaku
        End Get
        Set(ByVal value As Integer)
            intZeikomiHenkinGaku = value
        End Set
    End Property
#End Region

#Region "最終入金日"
    ''' <summary>
    ''' 最終入金日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSaisyuuNyuukinDate As DateTime
    ''' <summary>
    ''' 最終入金日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 最終入金日</returns>
    ''' <remarks></remarks>
    <TableMap("saisyuu_nyuukin_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SaisyuuNyuukinDate() As DateTime
        Get
            Return dateSaisyuuNyuukinDate
        End Get
        Set(ByVal value As DateTime)
            dateSaisyuuNyuukinDate = value
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
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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