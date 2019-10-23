''' <summary>
''' 入金取込データの排他チェック用レコードクラスです
''' </summary>
''' <remarks>
''' Kye項目と更新ユーザー、更新日時を保持し、他端末の更新チェックを行います。<br />
''' </remarks>
<TableClassMap("t_nyuukin_file_torikomi")> _
Public Class NyuukinFileTorikomiHaitaRecord

#Region "入力取込ユニークNO"
    ''' <summary>
    ''' 入力取込ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinTorikomiNo As Integer
    ''' <summary>
    ''' 入力取込ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入力取込ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_torikomi_unique_no", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property NyuukinTorikomiUniqueNo() As Integer
        Get
            Return intNyuukinTorikomiNo
        End Get
        Set(ByVal value As Integer)
            intNyuukinTorikomiNo = value
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
    <TableMap("upd_datetime", IsKey:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
