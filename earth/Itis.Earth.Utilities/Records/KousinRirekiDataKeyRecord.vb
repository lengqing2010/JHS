Public Class KousinRirekiDataKeyRecord


#Region "更新日 FROM"
    ''' <summary>
    ''' 更新日 FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKousinbiFrom As DateTime
    ''' <summary>
    ''' 更新日 FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日 FROM</returns>
    ''' <remarks></remarks>
    Public Property KousinbiFrom() As DateTime
        Get
            Return dateKousinbiFrom
        End Get
        Set(ByVal value As DateTime)
            dateKousinbiFrom = value
        End Set
    End Property
#End Region

#Region "更新日 TO"
    ''' <summary>
    ''' 更新日 TO
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKousinbiTo As DateTime
    ''' <summary>
    ''' 更新日 TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日 TO</returns>
    ''' <remarks></remarks>_
    Public Property KousinbiTo() As DateTime
        Get
            Return dateKousinbiTo
        End Get
        Set(ByVal value As DateTime)
            dateKousinbiTo = value
        End Set
    End Property
#End Region

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
    ''' <returns> 区分</returns>
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

#Region "保証書No"
    ''' <summary>
    ''' 保証書No
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' 保証書No
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書No</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "更新項目"
    ''' <summary>
    ''' 更新項目
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinKoumoku As String
    ''' <summary>
    ''' 更新項目
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新項目</returns>
    ''' <remarks></remarks>
    Public Property KousinKoumoku() As String
        Get
            Return strKousinKoumoku
        End Get
        Set(ByVal value As String)
            strKousinKoumoku = value
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
    Public Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

#Region "最新加盟店"
    ''' <summary>
    ''' 最新加盟店
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 最新加盟店
    ''' </summary>
    ''' <value></value>
    ''' <returns> 最新加盟店</returns>
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

#Region "最新加盟店カナ"
    ''' <summary>
    ''' 最新加盟店カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenKana As String
    ''' <summary>
    ''' 最新加盟店
    ''' </summary>
    ''' <value></value>
    ''' <returns> 最新加盟店</returns>
    ''' <remarks></remarks>
    Public Property KameitenKana() As String
        Get
            Return strKameitenKana
        End Get
        Set(ByVal value As String)
            strKameitenKana = value
        End Set
    End Property
#End Region

#Region "更新前値"
    ''' <summary>
    ''' 更新前値
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinBeforeValue As String
    ''' <summary>
    ''' 最新加盟店
    ''' </summary>
    ''' <value></value>
    ''' <returns> 最新加盟店</returns>
    ''' <remarks></remarks>
    Public Property KousinBeforeValue() As String
        Get
            Return strKousinBeforeValue
        End Get
        Set(ByVal value As String)
            strKousinBeforeValue = value
        End Set
    End Property
#End Region

#Region "更新後値"
    ''' <summary>
    ''' 更新後値
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinAfterValue As String
    ''' <summary>
    ''' 最新加盟店
    ''' </summary>
    ''' <value></value>
    ''' <returns> 最新加盟店</returns>
    ''' <remarks></remarks>
    Public Property KousinAfterValue() As String
        Get
            Return strKousinAfterValue
        End Get
        Set(ByVal value As String)
            strKousinAfterValue = value
        End Set
    End Property
#End Region

End Class
