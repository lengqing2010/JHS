''' <summary>
''' 基礎仕様マスタ検索レコードクラス
''' </summary>
''' <remarks></remarks>
Public Class KisoSiyouRecord

#Region "基礎仕様NO"
    ''' <summary>
    ''' 基礎仕様NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsSiyouNo As Integer
    ''' <summary>
    ''' 基礎仕様NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎仕様NO</returns>
    ''' <remarks></remarks>
    <TableMap("ks_siyou_no")> _
    Public Property KsSiyouNo() As Integer
        Get
            Return intKsSiyouNo
        End Get
        Set(ByVal value As Integer)
            intKsSiyouNo = value
        End Set
    End Property
#End Region

#Region "基礎仕様"
    ''' <summary>
    ''' 基礎仕様
    ''' </summary>
    ''' <remarks></remarks>
    Private strKsSiyou As String
    ''' <summary>
    ''' 基礎仕様
    ''' </summary>
    ''' <value></value>
    ''' <returns> 基礎仕様</returns>
    ''' <remarks></remarks>
    <TableMap("ks_siyou")> _
    Public Property KsSiyou() As String
        Get
            Return strKsSiyou
        End Get
        Set(ByVal value As String)
            strKsSiyou = value
        End Set
    End Property
#End Region

#Region "工事判定FLG"
    ''' <summary>
    ''' 工事判定FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojHanteiFlg As Integer
    ''' <summary>
    ''' 工事判定FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事判定FLG</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hantei_flg")> _
    Public Property KojHanteiFlg() As Integer
        Get
            Return intKojHanteiFlg
        End Get
        Set(ByVal value As Integer)
            intKojHanteiFlg = value
        End Set
    End Property
#End Region

#Region "加盟店ｺｰﾄﾞ"
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店ｺｰﾄﾞ</returns>
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

#Region "可否区分"
    ''' <summary>
    ''' 可否区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intKahiKbn As Integer
    ''' <summary>
    ''' 可否区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 可否区分</returns>
    ''' <remarks></remarks>
    <TableMap("kahi_kbn")> _
    Public Property KahiKbn() As Integer
        Get
            Return intKahiKbn
        End Get
        Set(ByVal value As Integer)
            intKahiKbn = value
        End Set
    End Property
#End Region

#Region "入力NO"
    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuuryokuNo As Integer
    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入力NO</returns>
    ''' <remarks></remarks>
    <TableMap("nyuuryoku_no")> _
    Public Property NyuuryokuNo() As Integer
        Get
            Return intNyuuryokuNo
        End Get
        Set(ByVal value As Integer)
            intNyuuryokuNo = value
        End Set
    End Property
#End Region



End Class
