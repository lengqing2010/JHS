''' <summary>
''' �d���f�[�^�e�[�u���̃��R�[�h�N���X
''' ���������ɕK�v�ȏ��̂ݐݒ�
''' </summary>
''' <remarks></remarks>
Public Class SiireDataKeyRecord

#Region "�`�[���j�[�NNO"
    ''' <summary>
    ''' �`�[���j�[�NNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenUnqNo As String
    ''' <summary>
    ''' �`�[���j�[�NNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �`�[���j�[�NNO</returns>
    ''' <remarks></remarks>
    Public Property DenUnqNo() As String
        Get
            Return strDenUnqNo
        End Get
        Set(ByVal value As String)
            strDenUnqNo = value
        End Set
    End Property
#End Region

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

#Region "�d���N���� From"
    ''' <summary>
    ''' �d���N���� From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSiireDateFrom As DateTime
    ''' <summary>
    ''' �d���N���� From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d���N���� From</returns>
    ''' <remarks></remarks>
    Public Property SiireDateFrom() As DateTime
        Get
            Return dateSiireDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateSiireDateFrom = value
        End Set
    End Property
#End Region

#Region "�d���N���� To"
    ''' <summary>
    ''' �d���N���� To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSiireDateTo As DateTime
    ''' <summary>
    ''' �d���N���� To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d���N���� To</returns>
    ''' <remarks></remarks>
    Public Property SiireDateTo() As DateTime
        Get
            Return dateSiireDateTo
        End Get
        Set(ByVal value As DateTime)
            dateSiireDateTo = value
        End Set
    End Property
#End Region

#Region "�d����R�[�h"
    ''' <summary>
    ''' �d����R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSiireSakiCd As String
    ''' <summary>
    ''' �d����R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d����R�[�h</returns>
    ''' <remarks></remarks>
    Public Property SiireSakiCd() As String
        Get
            Return strSiireSakiCd
        End Get
        Set(ByVal value As String)
            strSiireSakiCd = value
        End Set
    End Property
#End Region

#Region "�d����}��"
    ''' <summary>
    ''' �d����}��
    ''' </summary>
    ''' <remarks></remarks>
    Private strSiireSakiBrc As String
    ''' <summary>
    ''' �d����}��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d����}��</returns>
    ''' <remarks></remarks>
    Public Property SiireSakiBrc() As String
        Get
            Return strSiireSakiBrc
        End Get
        Set(ByVal value As String)
            strSiireSakiBrc = value
        End Set
    End Property
#End Region

#Region "�d���於"
    ''' <summary>
    ''' �d���於
    ''' </summary>
    ''' <remarks></remarks>
    Private strSiireSakiMei As String
    ''' <summary>
    ''' �d���於
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d���於</returns>
    ''' <remarks></remarks>
    Public Property SiireSakiMei() As String
        Get
            Return strSiireSakiMei
        End Get
        Set(ByVal value As String)
            strSiireSakiMei = value
        End Set
    End Property
#End Region

#Region "�d���於�J�i"
    ''' <summary>
    ''' �d���於�J�i
    ''' </summary>
    ''' <remarks></remarks>
    Private strSiireSakiMeiKana As String
    ''' <summary>
    ''' �d���於�J�i
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d���於�J�i</returns>
    ''' <remarks></remarks>
    Public Property SiireSakiMeiKana() As String
        Get
            Return strSiireSakiMeiKana
        End Get
        Set(ByVal value As String)
            strSiireSakiMeiKana = value
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

#Region "�}�C�i�X�`�[�\��"
    ''' <summary>
    ''' �}�C�i�X�`�[�\��
    ''' </summary>
    ''' <remarks></remarks>
    Private intMinusDenpyouDisp As Integer
    ''' <summary>
    ''' �}�C�i�X�`�[�\��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �}�C�i�X�`�[�\��</returns>
    ''' <remarks></remarks>
    Public Property MinusDenpyouDisp() As Integer
        Get
            Return intMinusDenpyouDisp
        End Get
        Set(ByVal value As Integer)
            intMinusDenpyouDisp = value
        End Set
    End Property
#End Region

#Region "�v��ϓ`�[�\��"
    ''' <summary>
    ''' �v��ϓ`�[�\��
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeijyouZumiDisp As Integer
    ''' <summary>
    ''' �v��ϓ`�[�\��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �v��ϓ`�[�\��</returns>
    ''' <remarks></remarks>
    Public Property KeijyouZumiDisp() As Integer
        Get
            Return intKeijyouZumiDisp
        End Get
        Set(ByVal value As Integer)
            intKeijyouZumiDisp = value
        End Set
    End Property
#End Region

End Class
