''' <summary>
''' ���i��񃌃R�[�h�ł�
''' </summary>
''' <remarks></remarks>
Public Class SyouhinRecord

#Region "���i����"
    ''' <summary>
    ''' ���i����
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' ���i����
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i����</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region
#Region "���i��"
    ''' <summary>
    ''' ���i��
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinNm As String
    ''' <summary>
    ''' ���i��
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i��</returns>
    ''' <remarks></remarks>
    Public Property SyouhinNm() As String
        Get
            Return strSyouhinNm
        End Get
        Set(ByVal value As String)
            strSyouhinNm = value
        End Set
    End Property
#End Region
#Region "�ŋ敪"
    ''' <summary>
    ''' �ŋ敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strZeiKbn As String
    ''' <summary>
    ''' �ŋ敪
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ŋ敪</returns>
    ''' <remarks></remarks>
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region
#Region "�W�����i"
    ''' <summary>
    ''' �W�����i
    ''' </summary>
    ''' <remarks></remarks>
    Private decHyoujunKakaku As Decimal
    ''' <summary>
    ''' �W�����i
    ''' </summary>
    ''' <value></value>
    ''' <returns>�W�����i</returns>
    ''' <remarks></remarks>
    Public Property HyoujunKakaku() As Decimal
        Get
            Return decHyoujunKakaku
        End Get
        Set(ByVal value As Decimal)
            decHyoujunKakaku = value
        End Set
    End Property
#End Region
#Region "�ŗ�"
    ''' <summary>
    ''' �ŗ�
    ''' </summary>
    ''' <remarks></remarks>
    Private decZeiritu As Decimal
    ''' <summary>
    ''' �ŗ�
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ŗ�</returns>
    ''' <remarks></remarks>
    Public Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
        End Set
    End Property
#End Region
#Region "�q�ɺ���"
    ''' <summary>
    ''' �q�ɺ���
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCd As String
    ''' <summary>
    ''' �q�ɺ���
    ''' </summary>
    ''' <value></value>
    ''' <returns>�q�ɺ���</returns>
    ''' <remarks></remarks>
    Public Property SoukoCd() As String
        Get
            Return strSoukoCd
        End Get
        Set(ByVal value As String)
            strSoukoCd = value
        End Set
    End Property
#End Region
End Class
