''' <summary>
''' �����t�@�C���捞�f�[�^�e�[�u���̃��R�[�h�N���X
''' ���������ɕK�v�ȏ��̂ݐݒ�
''' </summary>
''' <remarks></remarks>
Public Class NyuukinFileTorikomiKeyRecord

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

#Region "�����捞���j�[�N From"
    ''' <summary>
    ''' �����捞���j�[�N From
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinTorikomiNoFrom As Integer = Integer.MinValue
    ''' <summary>
    ''' �����捞���j�[�N From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����捞���j�[�N From</returns>
    ''' <remarks></remarks>
    Public Property NyuukinTorikomiNoFrom() As Integer
        Get
            Return intNyuukinTorikomiNoFrom
        End Get
        Set(ByVal value As Integer)
            intNyuukinTorikomiNoFrom = value
        End Set
    End Property
#End Region

#Region "�����捞���j�[�N To"
    ''' <summary>
    ''' �����捞���j�[�N To
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinTorikomiNoTo As Integer = Integer.MinValue
    ''' <summary>
    ''' �����捞���j�[�N To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����捞���j�[�N To</returns>
    ''' <remarks></remarks>
    Public Property NyuukinTorikomiNoTo() As Integer
        Get
            Return intNyuukinTorikomiNoTo
        End Get
        Set(ByVal value As Integer)
            intNyuukinTorikomiNoTo = value
        End Set
    End Property
#End Region

#Region "�捞�`�[�ԍ� From"
    ''' <summary>
    ''' �捞�`�[�ԍ� From
    ''' </summary>
    ''' <remarks></remarks>
    Private strTorikomiDenpyouNoFrom As String
    ''' <summary>
    ''' �捞�`�[�ԍ� From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �捞�`�[�ԍ� From</returns>
    ''' <remarks></remarks>
    Public Property TorikomiDenpyouNoFrom() As String
        Get
            Return strTorikomiDenpyouNoFrom
        End Get
        Set(ByVal value As String)
            strTorikomiDenpyouNoFrom = value
        End Set
    End Property
#End Region

#Region "�捞�`�[�ԍ� To"
    ''' <summary>
    ''' �捞�`�[�ԍ� To
    ''' </summary>
    ''' <remarks></remarks>
    Private strTorikomiDenpyouNoTo As String
    ''' <summary>
    ''' �捞�`�[�ԍ� To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �捞�`�[�ԍ� To</returns>
    ''' <remarks></remarks>
    Public Property TorikomiDenpyouNoTo() As String
        Get
            Return strTorikomiDenpyouNoTo
        End Get
        Set(ByVal value As String)
            strTorikomiDenpyouNoTo = value
        End Set
    End Property
#End Region

#Region "������ From"
    ''' <summary>
    ''' ������ From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuukinDateFrom As DateTime
    ''' <summary>
    ''' ������ From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������ From</returns>
    ''' <remarks></remarks>
    Public Property NyuukinDateFrom() As DateTime
        Get
            Return dateNyuukinDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateNyuukinDateFrom = value
        End Set
    End Property
#End Region

#Region "������ To"
    ''' <summary>
    ''' ������ To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuukinDateTo As DateTime
    ''' <summary>
    ''' ������ To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������ To</returns>
    ''' <remarks></remarks>
    Public Property NyuukinDateTo() As DateTime
        Get
            Return dateNyuukinDateTo
        End Get
        Set(ByVal value As DateTime)
            dateNyuukinDateTo = value
        End Set
    End Property
#End Region

#Region "EDI���쐬��"
    ''' <summary>
    ''' EDI���쐬��
    ''' </summary>
    ''' <remarks></remarks>
    Private strEdiJouhouSakuseiDate As String = String.Empty
    ''' <summary>
    ''' EDI���쐬��
    ''' </summary>
    ''' <value></value>
    ''' <returns>EDI���쐬��</returns>
    ''' <remarks></remarks>
    Public Property EdiJouhouSakuseiDate() As String
        Get
            Return strEdiJouhouSakuseiDate
        End Get
        Set(ByVal value As String)
            strEdiJouhouSakuseiDate = value
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


End Class
