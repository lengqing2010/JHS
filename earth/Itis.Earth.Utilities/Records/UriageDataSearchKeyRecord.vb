''' <summary>
''' ����f�[�^�e�[�u���̃��R�[�h�N���X
''' ���������ɕK�v�ȏ��̂ݐݒ�
''' </summary>
''' <remarks></remarks>
Public Class UriageDataSearchKeyRecord

#Region "�`�[NO From"
    ''' <summary>
    ''' �`�[NO From
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNoFrom As String
    ''' <summary>
    ''' �`�[NO From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �`�[NO From</returns>
    ''' <remarks></remarks>
    Public Property DenNoFrom() As String
        Get
            Return strDenNoFrom
        End Get
        Set(ByVal value As String)
            strDenNoFrom = value
        End Set
    End Property
#End Region

#Region "�`�[NO To"
    ''' <summary>
    ''' �`�[NO To
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNoTo As String
    ''' <summary>
    ''' �`�[NO To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �`�[NO To</returns>
    ''' <remarks></remarks>
    Public Property DenNoTo() As String
        Get
            Return strDenNoTo
        End Get
        Set(ByVal value As String)
            strDenNoTo = value
        End Set
    End Property
#End Region

#Region "�敪"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �敪</returns>
    ''' <remarks></remarks>
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "�ԍ� From"
    ''' <summary>
    ''' �ԍ� From
    ''' </summary>
    ''' <remarks></remarks>
    Private strBangouFrom As String
    ''' <summary>
    ''' �ԍ� From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ԍ� From</returns>
    ''' <remarks></remarks>
    Public Property BangouFrom() As String
        Get
            Return strBangouFrom
        End Get
        Set(ByVal value As String)
            strBangouFrom = value
        End Set
    End Property
#End Region

#Region "�ԍ� To"
    ''' <summary>
    ''' �ԍ� From
    ''' </summary>
    ''' <remarks></remarks>
    Private strBangouTo As String
    ''' <summary>
    ''' �ԍ� To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ԍ� To</returns>
    ''' <remarks></remarks>
    Public Property BangouTo() As String
        Get
            Return strBangouTo
        End Get
        Set(ByVal value As String)
            strBangouTo = value
        End Set
    End Property
#End Region

#Region "����N���� From"
    ''' <summary>
    ''' ����N���� From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriDateFrom As DateTime
    ''' <summary>
    ''' ����N���� From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����N���� From</returns>
    ''' <remarks></remarks>
    Public Property UriDateFrom() As DateTime
        Get
            Return dateUriDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateUriDateFrom = value
        End Set
    End Property
#End Region

#Region "����N���� To"
    ''' <summary>
    ''' ����N���� To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriDateTo As DateTime
    ''' <summary>
    ''' ����N���� To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����N���� To</returns>
    ''' <remarks></remarks>
    Public Property UriDateTo() As DateTime
        Get
            Return dateUriDateTo
        End Get
        Set(ByVal value As DateTime)
            dateUriDateTo = value
        End Set
    End Property
#End Region

#Region "�����N���� From"
    ''' <summary>
    ''' �����N���� From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuuDateFrom As DateTime
    ''' <summary>
    ''' �����N���� From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����N���� From</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuDateFrom() As DateTime
        Get
            Return dateSeikyuuDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuuDateFrom = value
        End Set
    End Property
#End Region

#Region "�����N���� To"
    ''' <summary>
    ''' �����N���� To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuuDateTo As DateTime
    ''' <summary>
    ''' �����N���� To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����N���� To</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuDateTo() As DateTime
        Get
            Return dateSeikyuuDateTo
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuuDateTo = value
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

#Region "�����於"
    ''' <summary>
    ''' �����於
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei As String
    ''' <summary>
    ''' �����於
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����於</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
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

#Region "���i�R�[�h"
    ''' <summary>
    ''' ���i�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' ���i�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���i�R�[�h</returns>
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

#Region "�o�^���� From"
    ''' <summary>
    ''' �o�^���� From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetimeFrom As DateTime
    ''' <summary>
    ''' �o�^���� From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�^���� From</returns>
    ''' <remarks></remarks>
    Public Property AddDatetimeFrom() As DateTime
        Get
            Return dateAddDatetimeFrom
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetimeFrom = value
        End Set
    End Property
#End Region

#Region "�o�^���� To"
    ''' <summary>
    ''' �o�^���� To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetimeTo As DateTime
    ''' <summary>
    ''' �o�^���� To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�^���� To</returns>
    ''' <remarks></remarks>
    Public Property AddDatetimeTo() As DateTime
        Get
            Return dateAddDatetimeTo
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetimeTo = value
        End Set
    End Property
#End Region

#Region "�ŐV�`�[�\��"
    ''' <summary>
    ''' �ŐV�`�[�\��
    ''' </summary>
    ''' <remarks></remarks>
    Private intNewDenpyouDisp As Integer
    ''' <summary>
    ''' �ŐV�`�[�\��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ŐV�`�[�\��</returns>
    ''' <remarks></remarks>
    Public Property NewDenpyouDisp() As Integer
        Get
            Return intNewDenpyouDisp
        End Get
        Set(ByVal value As Integer)
            intNewDenpyouDisp = value
        End Set
    End Property
#End Region

End Class
