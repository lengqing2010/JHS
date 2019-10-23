''' <summary>
''' 商品コード2 自動設定用パラメータレコード
''' </summary>
''' <remarks></remarks>
Public Class Syouhin23InfoRecord

#Region "商品２レコード"
    ''' <summary>
    ''' 商品２レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private objSyouhin2Rec As Syouhin23Record
    ''' <summary>
    ''' 商品２レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品２レコード</returns>
    ''' <remarks></remarks>
    Public Property Syouhin2Rec() As Syouhin23Record
        Get
            Return objSyouhin2Rec
        End Get
        Set(ByVal value As Syouhin23Record)
            objSyouhin2Rec = value
        End Set
    End Property
#End Region
#Region "請求先"
    ''' <summary>
    ''' 請求先
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusaki As String
    ''' <summary>
    ''' 請求先
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先</returns>
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
#Region "請求有無"
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuuUmu As Integer
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求有無</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmu() As Integer
        Get
            Return intSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intSeikyuuUmu = value
        End Set
    End Property
#End Region
#Region "発注書確定FLG"
    ''' <summary>
    ''' 発注書確定FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoKakuteiFlg As Integer
    ''' <summary>
    ''' 発注書確定FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発注書確定FLG</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuteiFlg() As Integer
        Get
            Return intHattyuusyoKakuteiFlg
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoKakuteiFlg = value
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
End Class
