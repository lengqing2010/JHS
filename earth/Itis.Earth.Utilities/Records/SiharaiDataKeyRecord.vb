''' <summary>
''' �x���f�[�^�e�[�u���̃��R�[�h�N���X
''' ���������ɕK�v�ȏ��̂ݐݒ�
''' </summary>
''' <remarks></remarks>
Public Class SiharaiDataKeyRecord

#Region "�`�[���j�[�NNO"
    ''' <summary>
    ''' �`�[���j�[�NNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenUnqNo As Integer
    ''' <summary>
    ''' �`�[���j�[�NNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �`�[���j�[�NNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no")> _
    Public Property DenUnqNo() As Integer
        Get
            Return intDenUnqNo
        End Get
        Set(ByVal value As Integer)
            intDenUnqNo = value
        End Set
    End Property
#End Region

#Region "�x���N���� From"
    ''' <summary>
    ''' �x���N���� From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateShriDateFrom As DateTime
    ''' <summary>
    ''' �x���N���� From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �x���N���� From</returns>
    ''' <remarks></remarks>
    Public Property ShriDateFrom() As DateTime
        Get
            Return dateShriDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateShriDateFrom = value
        End Set
    End Property
#End Region

#Region "�x���N���� To"
    ''' <summary>
    ''' �x���N���� To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateShriDateTo As DateTime
    ''' <summary>
    ''' �x���N���� To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �x���N���� To</returns>
    ''' <remarks></remarks>
    Public Property ShriDateTo() As DateTime
        Get
            Return dateShriDateTo
        End Get
        Set(ByVal value As DateTime)
            dateShriDateTo = value
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

#Region "������ЃR�[�h�{������Ў��Ə��R�[�h"
    ''' <summary>
    ''' ������ЃR�[�h�{������Ў��Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ������ЃR�[�h�{������Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������ЃR�[�h�{������Ў��Ə��R�[�h</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "�V��v���Ə��R�[�h"
    ''' <summary>
    ''' �V��v���Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkJigyouCd As String
    ''' <summary>
    ''' �V��v���Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �V��v���Ə��R�[�h</returns>
    ''' <remarks></remarks>
    Public Property SkkJigyouCd() As String
        Get
            Return strSkkJigyouCd
        End Get
        Set(ByVal value As String)
            strSkkJigyouCd = value
        End Set
    End Property
#End Region

#Region "�V��v�x����R�[�h"
    ''' <summary>
    ''' �V��v�x����R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkShriSakiCd As String
    ''' <summary>
    ''' �V��v�x����R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �V��v�x����R�[�h</returns>
    ''' <remarks></remarks>
    Public Property SkkShriSakiCd() As String
        Get
            Return strSkkShriSakiCd
        End Get
        Set(ByVal value As String)
            strSkkShriSakiCd = value
        End Set
    End Property
#End Region

#Region "�ŐV�`�[�\��"
    ''' <summary>
    ''' �ŐV�`�[�\��
    ''' </summary>
    ''' <remarks></remarks>
    Private intNewDenDisp As Integer
    ''' <summary>
    ''' �ŐV�`�[�\��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ŐV�`�[�\��</returns>
    ''' <remarks></remarks>
    Public Property NewDenDisp() As Integer
        Get
            Return intNewDenDisp
        End Get
        Set(ByVal value As Integer)
            intNewDenDisp = value
        End Set
    End Property
#End Region

End Class
