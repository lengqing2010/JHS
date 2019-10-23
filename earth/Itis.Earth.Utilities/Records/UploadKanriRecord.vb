''' <summary>
''' アップロード管理テーブル用レコードクラス
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_upload_kanri")> _
Public Class UploadKanriRecord

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

#Region "入力ファイル名"
    ''' <summary>
    ''' 入力ファイル名
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuuryokuFileMei As String
    ''' <summary>
    ''' 入力ファイル名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入力ファイル名</returns>
    ''' <remarks></remarks>
    <TableMap("nyuuryoku_file_mei", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=128)> _
    Public Property NyuuryokuFileMei() As String
        Get
            Return strNyuuryokuFileMei
        End Get
        Set(ByVal value As String)
            strNyuuryokuFileMei = value
        End Set
    End Property
#End Region

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
    <TableMap("edi_jouhou_sakusei_date", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Property EdiJouhouSakuseiDate() As String
        Get
            Return strEdiJouhouSakuseiDate
        End Get
        Set(ByVal value As String)
            strEdiJouhouSakuseiDate = value
        End Set
    End Property
#End Region

#Region "エラー有無"
    ''' <summary>
    ''' エラー有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intErrorUmu As Integer
    ''' <summary>
    ''' エラー有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> エラー有無</returns>
    ''' <remarks></remarks>
    <TableMap("error_umu", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property ErrorUmu() As Integer
        Get
            Return intErrorUmu
        End Get
        Set(ByVal value As Integer)
            intErrorUmu = value
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