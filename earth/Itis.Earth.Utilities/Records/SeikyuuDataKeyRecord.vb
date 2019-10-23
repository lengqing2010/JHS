''' <summary>
''' 請求データの検索KEYレコードクラス
''' 検索条件に必要な情報のみ設定
''' </summary>
''' <remarks></remarks>
Public Class SeikyuuDataKeyRecord

#Region "請求書NO群"
    ''' <summary>
    ''' 請求書NO群
    ''' </summary>
    ''' <remarks></remarks>
    Private strArrSeikyuuSakiCd As String
    ''' <summary>
    ''' 請求書NO群
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書NO群</returns>
    ''' <remarks></remarks>
    Public Property ArrSeikyuuSakiNo() As String
        Get
            Return strArrSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strArrSeikyuuSakiCd = value
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

#Region "印字対象外用紙"
    ''' <summary>
    ''' 印字対象外用紙
    ''' </summary>
    ''' <remarks></remarks>
    Private intInjiYousi As Integer = Integer.MinValue
    ''' <summary>
    ''' 印字対象外用紙
    ''' </summary>
    ''' <value></value>
    ''' <returns> 印字対象外用紙</returns>
    ''' <remarks></remarks>
    Public Property InjiYousi() As Integer
        Get
            Return intInjiYousi
        End Get
        Set(ByVal value As Integer)
            intInjiYousi = value
        End Set
    End Property
#End Region

#Region "請求書発行日 From"
    ''' <summary>
    ''' 請求書発行日 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoHakDateFrom As DateTime
    ''' <summary>
    ''' 請求書発行日 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書発行日 From</returns>
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

#Region "請求書発行日 To"
    ''' <summary>
    ''' 請求書発行日 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoHakDateTo As DateTime
    ''' <summary>
    ''' 請求書発行日 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書発行日 To</returns>
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

#Region "請求先区分"
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
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

#Region "請求先コード"
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
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
    Private strSeikyuuSakiBrc As String
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

#Region "請求締日"
    ''' <summary>
    ''' 請求締日
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSimeDate As String
    ''' <summary>
    ''' 請求締日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求締日</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSimeDate() As String
        Get
            Return strSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "請求書式"
    ''' <summary>
    ''' 請求書式
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSyosiki As String
    ''' <summary>
    ''' 請求書式
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書式</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSyosiki() As String
        Get
            Return strSeikyuuSyosiki
        End Get
        Set(ByVal value As String)
            strSeikyuuSyosiki = value
        End Set
    End Property
#End Region

#Region "明細件数 From"
    ''' <summary>
    ''' 明細件数 From
    ''' </summary>
    ''' <remarks></remarks>
    Private intMeisaiKensuuFrom As Integer = Integer.MinValue
    ''' <summary>
    ''' 明細件数 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 明細件数 From</returns>
    ''' <remarks></remarks>
    Public Property MeisaiKensuuFrom() As Integer
        Get
            Return intMeisaiKensuuFrom
        End Get
        Set(ByVal value As Integer)
            intMeisaiKensuuFrom = value
        End Set
    End Property
#End Region

#Region "明細件数 To"
    ''' <summary>
    ''' 明細件数 To
    ''' </summary>
    ''' <remarks></remarks>
    Private intMeisaiKensuuTo As Integer = Integer.MinValue
    ''' <summary>
    ''' 明細件数 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 明細件数 From</returns>
    ''' <remarks></remarks>
    Public Property MeisaiKensuuTo() As Integer
        Get
            Return intMeisaiKensuuTo
        End Get
        Set(ByVal value As Integer)
            intMeisaiKensuuTo = value
        End Set
    End Property
#End Region

#Region "請求書印刷日"
    ''' <summary>
    ''' 請求書印刷日
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoInsatuDate As DateTime
    ''' <summary>
    ''' 請求書印刷日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書印刷日</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoInsatuDate() As DateTime
        Get
            Return dtSeikyuusyoInsatuDate
        End Get
        Set(ByVal value As DateTime)
            dtSeikyuusyoInsatuDate = value
        End Set
    End Property
#End Region

End Class