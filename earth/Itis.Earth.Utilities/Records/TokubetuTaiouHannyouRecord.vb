''' <summary>
''' 特別対応汎用レコードクラス
''' </summary>
''' <remarks>特別対応データの格納時に使用します</remarks>
Public Class TokubetuTaiouRecordBase

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String = String.Empty
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分</returns>
    ''' <remarks></remarks>
    <TableMap("kbn")> _
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
    Private strHosyousyoNo As String = String.Empty
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no")> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "特別対応コード"
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intTokubetuTaiouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特別対応コード</returns>
    ''' <remarks></remarks>
    <TableMap("tokubetu_taiou_cd")> _
    Public Property TokubetuTaiouCd() As Integer
        Get
            Return intTokubetuTaiouCd
        End Get
        Set(ByVal value As Integer)
            intTokubetuTaiouCd = value
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
    <TableMap("torikesi")> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "特別対応マスタの項目"
#Region "特別対応コード"
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <remarks></remarks>
    Private mIntTokubetuTaiouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特別対応コード</returns>
    ''' <remarks></remarks>
    <TableMap("m_tokubetu_taiou_cd")> _
    Public Property mTokubetuTaiouCd() As Integer
        Get
            Return mIntTokubetuTaiouCd
        End Get
        Set(ByVal value As Integer)
            mIntTokubetuTaiouCd = value
        End Set
    End Property
#End Region

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private mIntTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消</returns>
    ''' <remarks></remarks>
    <TableMap("m_torikesi")> _
    Public Property mTorikesi() As Integer
        Get
            Return mIntTorikesi
        End Get
        Set(ByVal value As Integer)
            mIntTorikesi = value
        End Set
    End Property
#End Region

#Region "特別対応名称"
    ''' <summary>
    ''' 特別対応名称
    ''' </summary>
    ''' <remarks></remarks>
    Private strTokubetuTaiouMeisyou As String = String.Empty
    ''' <summary>
    ''' 特別対応名称
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特別対応名称</returns>
    ''' <remarks></remarks>
    <TableMap("tokubetu_taiou_meisyou")> _
    Public Property TokubetuTaiouMeisyou() As String
        Get
            Return strTokubetuTaiouMeisyou
        End Get
        Set(ByVal value As String)
            strTokubetuTaiouMeisyou = value
        End Set
    End Property
#End Region
#End Region

#Region "加盟店商品調査方法特別対応マスタの項目"
#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String = String.Empty
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String = String.Empty
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd")> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

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
    ''' <returns> 調査方法NO</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_no")> _
    Public Property TysHouhouNo() As Integer
        Get
            Return intTysHouhouNo
        End Get
        Set(ByVal value As Integer)
            intTysHouhouNo = value
        End Set
    End Property
#End Region

#Region "特別対応コード"
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <remarks></remarks>
    Private kIntTokubetuTaiouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特別対応コード</returns>
    ''' <remarks></remarks>
    <TableMap("k_tokubetu_taiou_cd")> _
    Public Property kTokubetuTaiouCd() As Integer
        Get
            Return kIntTokubetuTaiouCd
        End Get
        Set(ByVal value As Integer)
            kIntTokubetuTaiouCd = value
        End Set
    End Property
#End Region

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private kIntTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消</returns>
    ''' <remarks></remarks>
    <TableMap("k_torikesi")> _
    Public Property kTorikesi() As Integer
        Get
            Return kIntTorikesi
        End Get
        Set(ByVal value As Integer)
            kIntTorikesi = value
        End Set
    End Property
#End Region

#End Region

#Region "登録ログインユーザーID"
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String = String.Empty
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

#Region "登録日時"
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime = DateTime.MinValue
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
    Private strUpdLoginUserId As String = String.Empty
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
    Private dateUpdDatetime As DateTime = DateTime.MinValue
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

#Region "各種メソッド"
#Region "チェック状況の判定"
    ''' <summary>
    ''' チェック状況の判定
    ''' </summary>
    ''' <remarks></remarks>
    Private blnChk As Boolean = False
    ''' <summary>
    ''' チェック状況の判定
    ''' </summary>
    ''' <value></value>
    ''' <returns>チェック状況の判定</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HanteiCheck() As Boolean
        Get
            If Kbn <> String.Empty _
                AndAlso HosyousyoNo <> String.Empty _
                    AndAlso TokubetuTaiouCd <> Integer.MinValue _
                        AndAlso Torikesi = 0 Then
                blnChk = True
            End If
            Return blnChk
        End Get
    End Property

    ''' <summary>
    ''' チェック状況の判定(マスタ再取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnChkM As Boolean = False
    ''' <summary>
    ''' チェック状況の判定(マスタ再取得)
    ''' </summary>
    ''' <value></value>
    ''' <returns>チェック状況の判定(マスタ再取得)</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property kHanteiCheck() As Boolean
        Get
            If KameitenCd <> String.Empty _
                AndAlso TysHouhouNo <> Integer.MinValue _
                    AndAlso SyouhinCd <> String.Empty _
                        AndAlso kTokubetuTaiouCd <> Integer.MinValue _
                            AndAlso kTorikesi = 0 Then
                blnChkM = True
            End If
            Return blnChkM
        End Get
    End Property
#End Region

#Region "取消状況の判定"
    ''' <summary>
    ''' 取消状況の判定
    ''' </summary>
    ''' <remarks></remarks>
    Private blnTorikesi As Boolean = True
    ''' <summary>
    ''' 取消状況の判定
    ''' </summary>
    ''' <value></value>
    ''' <returns>取消状況の判定</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HanteiTorikesi() As Boolean
        Get
            '特別対応Mが取消でない場合
            If mTorikesi = 0 Then
                blnTorikesi = False

            ElseIf Torikesi = 0 Then
                '特別対応Mが取消でも、特別対応データが取消でない場合
                blnTorikesi = False

            End If
            Return blnTorikesi
        End Get
    End Property
#End Region

#End Region

End Class