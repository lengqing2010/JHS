''' <summary>
''' �����f�[�^�̌���KEY���R�[�h�N���X
''' ���������ɕK�v�ȏ��̂ݐݒ�
''' </summary>
''' <remarks></remarks>
Public Class SeikyuuDataKeyRecord

#Region "������NO�Q"
    ''' <summary>
    ''' ������NO�Q
    ''' </summary>
    ''' <remarks></remarks>
    Private strArrSeikyuuSakiCd As String
    ''' <summary>
    ''' ������NO�Q
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������NO�Q</returns>
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

#Region "���"
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���</returns>
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

#Region "�󎚑ΏۊO�p��"
    ''' <summary>
    ''' �󎚑ΏۊO�p��
    ''' </summary>
    ''' <remarks></remarks>
    Private intInjiYousi As Integer = Integer.MinValue
    ''' <summary>
    ''' �󎚑ΏۊO�p��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �󎚑ΏۊO�p��</returns>
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

#Region "���������s�� From"
    ''' <summary>
    ''' ���������s�� From
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoHakDateFrom As DateTime
    ''' <summary>
    ''' ���������s�� From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������s�� From</returns>
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

#Region "���������s�� To"
    ''' <summary>
    ''' ���������s�� To
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoHakDateTo As DateTime
    ''' <summary>
    ''' ���������s�� To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������s�� To</returns>
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

#Region "������敪"
    ''' <summary>
    ''' ������敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' ������敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������敪</returns>
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

#Region "������R�[�h"
    ''' <summary>
    ''' ������R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' ������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������R�[�h</returns>
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

#Region "������}��"
    ''' <summary>
    ''' ������}��
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' ������}��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������}��</returns>
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

#Region "�����於�J�i"
    ''' <summary>
    ''' �����於�J�i
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMeiKana As String
    ''' <summary>
    ''' �����於�J�i
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����於�J�i</returns>
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

#Region "��������"
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSimeDate As String
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��������</returns>
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

#Region "��������"
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSyosiki As String
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��������</returns>
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

#Region "���׌��� From"
    ''' <summary>
    ''' ���׌��� From
    ''' </summary>
    ''' <remarks></remarks>
    Private intMeisaiKensuuFrom As Integer = Integer.MinValue
    ''' <summary>
    ''' ���׌��� From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���׌��� From</returns>
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

#Region "���׌��� To"
    ''' <summary>
    ''' ���׌��� To
    ''' </summary>
    ''' <remarks></remarks>
    Private intMeisaiKensuuTo As Integer = Integer.MinValue
    ''' <summary>
    ''' ���׌��� To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���׌��� From</returns>
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

#Region "�����������"
    ''' <summary>
    ''' �����������
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoInsatuDate As DateTime
    ''' <summary>
    ''' �����������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����������</returns>
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