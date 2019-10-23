''' <summary>
''' 商品コード1 自動設定用パラメータレコード
''' </summary>
''' <remarks></remarks>
Public Class Syouhin1InfoRecord
#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKubun As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分</returns>
    ''' <remarks></remarks>
    Public Property Kubun() As String
        Get
            Return strKubun
        End Get
        Set(ByVal value As String)
            strKubun = value
        End Set
    End Property
#End Region

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店コード</returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "営業所コード"
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoCd As String
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業所コード</returns>
    ''' <remarks></remarks>
    Public Property EigyousyoCd() As String
        Get
            Return strEigyousyoCd
        End Get
        Set(ByVal value As String)
            strEigyousyoCd = value
        End Set
    End Property
#End Region

#Region "商品区分"
    ''' <summary>
    ''' 商品区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhinKbn As Integer
    ''' <summary>
    ''' 商品区分(未指定時は9を設定してください)
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品区分</returns>
    ''' <remarks>100:商品区分1 110,115:商品区分2 120:商品区分3</remarks>
    Public Property SyouhinKbn() As Integer
        Get
            Return intSyouhinKbn
        End Get
        Set(ByVal value As Integer)
            intSyouhinKbn = value
        End Set
    End Property
#End Region

#Region "調査方法NO"
    ''' <summary>
    ''' 調査方法NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaHouhouNo As Integer
    ''' <summary>
    ''' 調査方法NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査方法NO</returns>
    ''' <remarks></remarks>
    Public Property TyousaHouhouNo() As Integer
        Get
            Return intTyousaHouhouNo
        End Get
        Set(ByVal value As Integer)
            intTyousaHouhouNo = value
        End Set
    End Property
#End Region

#Region "調査概要"
    ''' <summary>
    ''' 調査概要
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaGaiyou As Integer
    ''' <summary>
    ''' 調査概要(未指定時は9を設定してください)
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査概要</returns>
    ''' <remarks></remarks>
    Public Property TyousaGaiyou() As Integer
        Get
            Return intTyousaGaiyou
        End Get
        Set(ByVal value As Integer)
            intTyousaGaiyou = value
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
    ''' <returns>商品コード</returns>
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

#Region "建物用途"
    ''' <summary>
    ''' 建物用途
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatemonoYouto As Integer
    ''' <summary>
    ''' 建物用途
    ''' </summary>
    ''' <value></value>
    ''' <returns>建物用途</returns>
    ''' <remarks></remarks>
    Public Property TatemonoYouto() As Integer
        Get
            Return intTatemonoYouto
        End Get
        Set(ByVal value As Integer)
            intTatemonoYouto = value
        End Set
    End Property
#End Region

#Region "請求先(直接or他請求)"
    ''' <summary>
    ''' 請求先(直接or他請求)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusaki As String
    ''' <summary>
    ''' 請求先(直接or他請求)
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先(直接or他請求)</returns>
    ''' <remarks></remarks>
    Public Property Seikyuusaki() As String
        Get
            Return strSeikyuusaki
        End Get
        Set(ByVal value As String)
            strSeikyuusaki = value
        End Set
    End Property
#End Region

#Region "系列FLG"
    ''' <summary>
    ''' 系列FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiretuFlg As Integer
    ''' <summary>
    ''' 系列FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>系列FLG</returns>
    ''' <remarks></remarks>
    Public Property KeiretuFlg() As Integer
        Get
            Return intKeiretuFlg
        End Get
        Set(ByVal value As Integer)
            intKeiretuFlg = value
        End Set
    End Property
#End Region

#Region "系列ｺｰﾄﾞ"
    ''' <summary>
    ''' 系列ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' 系列ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns>系列ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region

#Region "工務店金額１"
    ''' <summary>
    ''' 工務店金額１
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenKingaku1 As Integer
    ''' <summary>
    ''' 工務店金額１
    ''' </summary>
    ''' <value></value>
    ''' <returns>工務店金額１</returns>
    ''' <remarks></remarks>
    Public Property KoumutenKingaku1() As Integer
        Get
            If intKoumutenKingaku1 = Integer.MinValue Then
                intKoumutenKingaku1 = 0
            End If
            Return intKoumutenKingaku1
        End Get
        Set(ByVal value As Integer)
            intKoumutenKingaku1 = value
        End Set
    End Property
#End Region

#Region "税抜金額１"
    ''' <summary>
    ''' 税抜金額１
    ''' </summary>
    ''' <remarks></remarks>
    Private intZeinukiKingaku1 As Integer
    ''' <summary>
    ''' 税抜金額１
    ''' </summary>
    ''' <value></value>
    ''' <returns>税抜金額１</returns>
    ''' <remarks></remarks>
    Public Property ZeinukiKingaku1() As Integer
        Get
            If intZeinukiKingaku1 = Integer.MinValue Then
                intZeinukiKingaku1 = 0
            End If
            Return intZeinukiKingaku1
        End Get
        Set(ByVal value As Integer)
            intZeinukiKingaku1 = value
        End Set
    End Property
#End Region

#Region "調査会社ｺｰﾄﾞ＋事業所コード"
    ''' <summary>
    ''' 調査会社ｺｰﾄﾞ＋事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' 調査会社ｺｰﾄﾞ＋事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社ｺｰﾄﾞ＋事業所コード</returns>
    ''' <remarks></remarks>
    Public Overridable Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

    '#Region "調査会社事業所ｺｰﾄﾞ"
    '    ''' <summary>
    '    ''' 調査会社事業所ｺｰﾄﾞ
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private strTysKaisyaJigyousyoCd As String
    '    ''' <summary>
    '    ''' 調査会社事業所ｺｰﾄﾞ
    '    ''' </summary>
    '    ''' <value></value>
    '    ''' <returns> 調査会社事業所ｺｰﾄﾞ</returns>
    '    ''' <remarks></remarks>
    '    Public Overridable Property TysKaisyaJigyousyoCd() As String
    '        Get
    '            Return strTysKaisyaJigyousyoCd
    '        End Get
    '        Set(ByVal value As String)
    '            strTysKaisyaJigyousyoCd = value
    '        End Set
    '    End Property
    '#End Region

#Region "同時依頼棟数"
    ''' <summary>
    ''' 同時依頼棟数
    ''' </summary>
    ''' <remarks></remarks>
    Private intDoujiIraiTousuu As Integer
    ''' <summary>
    ''' 同時依頼棟数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 同時依頼棟数</returns>
    ''' <remarks></remarks>
    Public Overridable Property DoujiIraiTousuu() As Integer
        Get
            Return intDoujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuu = value
        End Set
    End Property
#End Region

End Class
