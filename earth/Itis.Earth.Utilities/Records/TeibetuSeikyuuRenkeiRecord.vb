''' <summary>
''' �@�ʐ����A�g�Ǘ����R�[�h
''' </summary>
''' <remarks></remarks>
Public Class TeibetuSeikyuuRenkeiRecord

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

#Region "�ۏ؏�NO"
    ''' <summary>
    ''' �ۏ؏�NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' �ۏ؏�NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�NO</returns>
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

#Region "���޺���"
    ''' <summary>
    ''' ���޺���
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String
    ''' <summary>
    ''' ���޺���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���޺���</returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "��ʕ\��NO"
    ''' <summary>
    ''' ��ʕ\��NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intGamenHyoujiNo As Integer
    ''' <summary>
    ''' ��ʕ\��NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��ʕ\��NO</returns>
    ''' <remarks></remarks>
    Public Property GamenHyoujiNo() As Integer
        Get
            Return intGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            intGamenHyoujiNo = value
        End Set
    End Property
#End Region

#Region "�A�g�w���R�[�h"
    ''' <summary>
    ''' �A�g�w���R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private intRenkeiSijiCd As Integer
    ''' <summary>
    ''' �A�g�w���R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �A�g�w���R�[�h</returns>
    ''' <remarks></remarks>
    Public Property RenkeiSijiCd() As Integer
        Get
            Return intRenkeiSijiCd
        End Get
        Set(ByVal value As Integer)
            intRenkeiSijiCd = value
        End Set
    End Property
#End Region

#Region "���M�󋵃R�[�h"
    ''' <summary>
    ''' ���M�󋵃R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private intSousinJykyCd As Integer
    ''' <summary>
    ''' ���M�󋵃R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���M�󋵃R�[�h</returns>
    ''' <remarks></remarks>
    Public Property SousinJykyCd() As Integer
        Get
            Return intSousinJykyCd
        End Get
        Set(ByVal value As Integer)
            intSousinJykyCd = value
        End Set
    End Property
#End Region

#Region "���M��������"
    ''' <summary>
    ''' ���M��������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSousinKanryDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' ���M��������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���M��������</returns>
    ''' <remarks></remarks>
    Public Property SousinKanryDatetime() As DateTime
        Get
            Return dateSousinKanryDatetime
        End Get
        Set(ByVal value As DateTime)
            dateSousinKanryDatetime = value
        End Set
    End Property
#End Region

#Region "�X�V۸޲�հ�ްID"
    ''' <summary>
    ''' �X�V۸޲�հ�ްID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' �X�V۸޲�հ�ްID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V۸޲�հ�ްID</returns>
    ''' <remarks></remarks>
    Public Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "�X�V����"
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime1 As DateTime = DateTime.MinValue
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
    ''' <remarks></remarks>
    Public Property UpdDatetime1() As DateTime
        Get
            Return dateUpdDatetime1
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime1 = value
        End Set
    End Property
#End Region

#Region "�X�V�󋵃t���O"
    ''' <summary>
    ''' �X�V�󋵃t���O
    ''' </summary>
    ''' <remarks></remarks>
    Private blnIsUpdate As Boolean
    ''' <summary>
    ''' �X�V�󋵃t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns>�X�V�󋵃t���O</returns>
    ''' <remarks>�n�Ղɂ͑��݂���ꍇTrue</remarks>
    Public Property IsUpdate() As Boolean
        Get
            Return blnIsUpdate
        End Get
        Set(ByVal value As Boolean)
            blnIsUpdate = value
        End Set
    End Property
#End Region

End Class