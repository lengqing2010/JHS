''' <summary>
''' FC申込修正受注済レコードクラス/FC申込修正画面
''' </summary>
''' <remarks>FC申込データの修正時に使用します(受注済時用)</remarks>
<TableClassMap("FcMousikomiIF")> _
Public Class FcMousikomiSyuuseiJytyzumiRecord
    Inherits FcMousikomiRecord

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "申込NO"
    ''' <summary>
    ''' 申込NO
    ''' </summary>
    ''' <remarks></remarks>
    Private lngMousikomiNo As Long = Long.MinValue
    ''' <summary>
    ''' 申込NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 申込NO</returns>
    ''' <remarks></remarks>
    <TableMap("mousikomi_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Overrides Property MousikomiNo() As Long
        Get
            Return lngMousikomiNo
        End Get
        Set(ByVal value As Long)
            lngMousikomiNo = value
        End Set
    End Property
#End Region

#Region "調査連絡先_担当者"
    ''' <summary>
    ''' 調査連絡先_担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String = String.Empty
    ''' <summary>
    ''' 調査連絡先_担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_担当者</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tantou_mei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

#Region "立会者(その他補足)"
    ''' <summary>
    ''' 立会者(その他補足)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTtsySonotaHosoku As String = String.Empty
    ''' <summary>
    ''' 立会者(その他補足)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 立会者(その他補足)</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_sonota_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TtsySonotaHosoku() As String
        Get
            Return strTtsySonotaHosoku
        End Get
        Set(ByVal value As String)
            strTtsySonotaHosoku = value
        End Set
    End Property
#End Region

#Region "要注意情報"
    ''' <summary>
    ''' 要注意情報
    ''' </summary>
    ''' <remarks></remarks>
    Private strYouTyuuiJouhou As String = String.Empty
    ''' <summary>
    ''' 要注意情報
    ''' </summary>
    ''' <value></value>
    ''' <returns> 要注意情報</returns>
    ''' <remarks></remarks>
    <TableMap("you_tyuui_jouhou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property YouTyuuiJouhou() As String
        Get
            Return strYouTyuuiJouhou
        End Get
        Set(ByVal value As String)
            strYouTyuuiJouhou = value
        End Set
    End Property
#End Region

#Region "調査会社担当者"
    ''' <summary>
    ''' 調査会社担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaTantousya As String = String.Empty
    ''' <summary>
    ''' 調査会社担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社担当者</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd_tantousya", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TysKaisyaTantousya() As String
        Get
            Return strTysKaisyaTantousya
        End Get
        Set(ByVal value As String)
            strTysKaisyaTantousya = value
        End Set
    End Property
#End Region

#Region "更新者"
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String = String.Empty
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新者</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "更新ログインユーザーID"
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String = String.Empty
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

End Class
