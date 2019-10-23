''' <summary>
''' 入金エラー情報テーブル用レコードクラス
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_nyuukin_error")> _
Public Class NyuukinErrorRecord

#Region "EDI情報作成日"
    ''' <summary>
    ''' EDI情報作成日
    ''' </summary>
    ''' <remarks></remarks>
    Private strEdiJouhouSakuseiDate As String
    ''' <summary>
    ''' EDI情報作成日
    ''' </summary>
    ''' <value></value>
    ''' <returns> EDI情報作成日</returns>
    ''' <remarks></remarks>
    <TableMap("edi_jouhou_sakusei_date", IsKey:=True, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Property EdiJouhouSakuseiDate() As String
        Get
            Return strEdiJouhouSakuseiDate
        End Get
        Set(ByVal value As String)
            strEdiJouhouSakuseiDate = value
        End Set
    End Property
#End Region

#Region "行NO"
    ''' <summary>
    ''' 行NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intGyouNo As Integer
    ''' <summary>
    ''' 行NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 行NO</returns>
    ''' <remarks></remarks>
    <TableMap("gyou_no", IsKey:=True, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property GyouNo() As Integer
        Get
            Return intGyouNo
        End Get
        Set(ByVal value As Integer)
            intGyouNo = value
        End Set
    End Property
#End Region

#Region "処理日時"
    ''' <summary>
    ''' 処理日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoriDatetime As DateTime
    ''' <summary>
    ''' 処理日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 処理日時</returns>
    ''' <remarks></remarks>
    <TableMap("syori_datetime", IsKey:=True, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SyoriDatetime() As DateTime
        Get
            Return dateSyoriDatetime
        End Get
        Set(ByVal value As DateTime)
            dateSyoriDatetime = value
        End Set
    End Property
#End Region

#Region "グループコード"
    ''' <summary>
    ''' グループコード
    ''' </summary>
    ''' <remarks></remarks>
    Private strGroupCd As String
    ''' <summary>
    ''' グループコード
    ''' </summary>
    ''' <value></value>
    ''' <returns> グループコード</returns>
    ''' <remarks></remarks>
    <TableMap("group_cd", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property GroupCd() As String
        Get
            Return strGroupCd
        End Get
        Set(ByVal value As String)
            strGroupCd = value
        End Set
    End Property
#End Region

#Region "顧客コード"
    ''' <summary>
    ''' 顧客コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuCd As String
    ''' <summary>
    ''' 顧客コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 顧客コード</returns>
    ''' <remarks></remarks>
    <TableMap("kokyaku_cd", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property KokyakuCd() As String
        Get
            Return strKokyakuCd
        End Get
        Set(ByVal value As String)
            strKokyakuCd = value
        End Set
    End Property
#End Region

#Region "摘要"
    ''' <summary>
    ''' 摘要
    ''' </summary>
    ''' <remarks></remarks>
    Private strTekiyou As String
    ''' <summary>
    ''' 摘要
    ''' </summary>
    ''' <value></value>
    ''' <returns> 摘要</returns>
    ''' <remarks></remarks>
    <TableMap("tekiyou", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=510)> _
    Public Property Tekiyou() As String
        Get
            Return strTekiyou
        End Get
        Set(ByVal value As String)
            strTekiyou = value
        End Set
    End Property
#End Region

#Region "入金額(手数料額含む）"
    ''' <summary>
    ''' 入金額(手数料額含む）
    ''' </summary>
    ''' <remarks></remarks>
    Private lngNyuukinGaku As Long
    ''' <summary>
    ''' 入金額(手数料額含む）
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金額(手数料額含む）</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_gaku", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property NyuukinGaku() As Long
        Get
            Return lngNyuukinGaku
        End Get
        Set(ByVal value As Long)
            lngNyuukinGaku = value
        End Set
    End Property
#End Region

#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=8)> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "ファイル区分"
    ''' <summary>
    ''' ファイル区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intFileKbn As Integer
    ''' <summary>
    ''' ファイル区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> ファイル区分</returns>
    ''' <remarks></remarks>
    <TableMap("file_kbn", IsKey:=True, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property FileKbn() As Integer
        Get
            Return intFileKbn
        End Get
        Set(ByVal value As Integer)
            intFileKbn = value
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

End Class