''' <summary>
''' 地盤データのレコードクラスです
''' 邸別入金データ修正画面用の更新データ構成です
''' </summary>
''' <remarks>地盤テーブルの全レコードカラムに加え、邸別データを保持してます<br/>
'''          商品コード１の邸別請求レコード：TeibetuSeikyuuRecord<br/>
'''          商品コード２の邸別請求レコード：Dictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          商品コード３の邸別請求レコード：Dictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          追加工事の邸別請求レコード　　：TeibetuSeikyuuRecord<br/>
'''          改良工事の邸別請求レコード　　：TeibetuSeikyuuRecord<br/>
'''          調査報告書の邸別請求レコード　：TeibetuSeikyuuRecord<br/>
'''          工事報告書の邸別請求レコード　：TeibetuSeikyuuRecord<br/>
'''          保証書の邸別請求レコード　　　：TeibetuSeikyuuRecord<br/>
'''          解約払戻の邸別請求レコード　　：TeibetuSeikyuuRecord<br/>
'''          上記以外の邸別請求レコード    ：List(TeibetuSeikyuuRecord)<br/>
'''          邸別入金レコード              ：Dictionary(Of String, TeibetuNyuukinRecord)<br/>
''' </remarks>
<TableClassMap("t_jiban")> _
Public Class JibanRecordTeibetuNyuukin
    Inherits JibanRecordBase

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
    <TableMap("kbn", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overrides Property Kbn() As String
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
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "返金処理FLG"
    ''' <summary>
    ''' 返金処理FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHenkinSyoriFlg As Integer
    ''' <summary>
    ''' 返金処理FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 返金処理FLG</returns>
    ''' <remarks></remarks>
    <TableMap("henkin_syori_flg", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HenkinSyoriFlg() As Integer
        Get
            Return intHenkinSyoriFlg
        End Get
        Set(ByVal value As Integer)
            intHenkinSyoriFlg = value
        End Set
    End Property
#End Region

#Region "返金処理日"
    ''' <summary>
    ''' 返金処理日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHenkinSyoriDate As DateTime
    ''' <summary>
    ''' 返金処理日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 返金処理日</returns>
    ''' <remarks></remarks>
    <TableMap("henkin_syori_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HenkinSyoriDate() As DateTime
        Get
            Return dateHenkinSyoriDate
        End Get
        Set(ByVal value As DateTime)
            dateHenkinSyoriDate = value
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
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property UpdLoginUserId() As String
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
    <TableMap("upd_datetime", IsKey:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "更新者"
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新者</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

End Class