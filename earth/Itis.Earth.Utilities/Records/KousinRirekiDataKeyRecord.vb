Public Class KousinRirekiDataKeyRecord


#Region "�X�V�� FROM"
    ''' <summary>
    ''' �X�V�� FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKousinbiFrom As DateTime
    ''' <summary>
    ''' �X�V�� FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V�� FROM</returns>
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

#Region "�X�V�� TO"
    ''' <summary>
    ''' �X�V�� TO
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKousinbiTo As DateTime
    ''' <summary>
    ''' �X�V�� TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V�� TO</returns>
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

#Region "�敪"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strKubun As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �敪</returns>
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

#Region "�ۏ؏�No"
    ''' <summary>
    ''' �ۏ؏�No
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' �ۏ؏�No
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�No</returns>
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

#Region "�X�V����"
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinKoumoku As String
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
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

#Region "�X�V��"
    ''' <summary>
    ''' �X�V��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String
    ''' <summary>
    ''' �X�V��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V��</returns>
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

#Region "�ŐV�����X"
    ''' <summary>
    ''' �ŐV�����X
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' �ŐV�����X
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ŐV�����X</returns>
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

#Region "�ŐV�����X�J�i"
    ''' <summary>
    ''' �ŐV�����X�J�i
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenKana As String
    ''' <summary>
    ''' �ŐV�����X
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ŐV�����X</returns>
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

#Region "�X�V�O�l"
    ''' <summary>
    ''' �X�V�O�l
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinBeforeValue As String
    ''' <summary>
    ''' �ŐV�����X
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ŐV�����X</returns>
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

#Region "�X�V��l"
    ''' <summary>
    ''' �X�V��l
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinAfterValue As String
    ''' <summary>
    ''' �ŐV�����X
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ŐV�����X</returns>
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
