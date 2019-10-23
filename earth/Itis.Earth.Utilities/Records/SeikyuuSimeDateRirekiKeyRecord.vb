''' <summary>
''' 請求書締め日履歴データの検索KEYレコードクラス
''' 検索条件に必要な情報のみ設定
''' </summary>
''' <remarks></remarks>
Public Class SeikyuuSimeDateRirekiKeyRecord

#Region "請求先コード"
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String = String.Empty
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先コード</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiCd() As String
        Get
            Return strSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "請求先枝番"
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String = String.Empty
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先枝番</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "請求先区分"
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String = String.Empty
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先区分</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "請求先名カナ"
    ''' <summary>
    ''' 請求先名カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMeiKana As String
    ''' <summary>
    ''' 請求先名カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先名カナ</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiMeiKana() As String
        Get
            Return strSeikyuuSakiMeiKana
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMeiKana = value
        End Set
    End Property
#End Region

#Region "請求書発行日_FROM"
    ''' <summary>
    ''' 請求書発行日_FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoHakDateFrom As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 請求書発行日_FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書発行日_FROM</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoHakDateFrom() As DateTime
        Get
            Return dtSeikyuusyoHakDateFrom
        End Get
        Set(ByVal value As DateTime)
            dtSeikyuusyoHakDateFrom = value
        End Set
    End Property
#End Region

#Region "請求書発行日_TO"
    ''' <summary>
    ''' 請求書発行日_TO
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoHakDateTo As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 請求書発行日_TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書発行日_TO</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoHakDateTo() As DateTime
        Get
            Return dtSeikyuusyoHakDateTo
        End Get
        Set(ByVal value As DateTime)
            dtSeikyuusyoHakDateTo = value
        End Set
    End Property
#End Region

#Region "請求書NO_FROM"
    ''' <summary>
    ''' 請求書NO_FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusyoNoFrom As String = String.Empty
    ''' <summary>
    ''' 請求書NO_FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書NO_FROM</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoNoFrom() As String
        Get
            Return strSeikyuusyoNoFrom
        End Get
        Set(ByVal value As String)
            strSeikyuusyoNoFrom = value
        End Set
    End Property
#End Region

#Region "請求書NO_TO"
    ''' <summary>
    ''' 請求書NO_TO
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusyoNoTo As String = String.Empty
    ''' <summary>
    ''' 請求書NO_TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書_TO</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoNoTo() As String
        Get
            Return strSeikyuusyoNoTo
        End Get
        Set(ByVal value As String)
            strSeikyuusyoNoTo = value
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
    Public Property Torikesi() As String
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As String)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "最新履歴表示"
    ''' <summary>
    ''' 最新履歴表示
    ''' </summary>
    ''' <remarks></remarks>
    Private intNewRirekiDisp As Integer = Integer.MinValue
    ''' <summary>
    ''' 最新履歴表示
    ''' </summary>
    ''' <value></value>
    ''' <returns> 最新履歴表示</returns>
    ''' <remarks></remarks>
    Public Property NewRirekiDisp() As Integer
        Get
            Return intNewRirekiDisp
        End Get
        Set(ByVal value As Integer)
            intNewRirekiDisp = value
        End Set
    End Property
#End Region

End Class
