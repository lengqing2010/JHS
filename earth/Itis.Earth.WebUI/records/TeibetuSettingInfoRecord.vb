''' <summary>
''' �@�ʃf�[�^�C���̊e���׃R���g���[���ɐݒ肷��<br/>
''' ���ʃf�[�^��ێ����郌�R�[�h�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class TeibetuSettingInfoRecord

#Region "�敪"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks></remarks>
    Private _kubun As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kubun() As String
        Get
            Return _kubun
        End Get
        Set(ByVal value As String)
            _kubun = value
        End Set
    End Property
#End Region

#Region "�ԍ��i�ۏ؏�NO�j"
    ''' <summary>
    ''' �ԍ��i�ۏ؏�NO�j
    ''' </summary>
    ''' <remarks></remarks>
    Private _bangou As String
    ''' <summary>
    ''' �ԍ��i�ۏ؏�NO�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bangou() As String
        Get
            Return _bangou
        End Get
        Set(ByVal value As String)
            _bangou = value
        End Set
    End Property
#End Region

#Region "���O�C�����[�U�[ID"
    ''' <summary>
    ''' ���O�C�����[�U�[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private _updLoginUserId As String
    ''' <summary>
    ''' ���O�C�����[�U�[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdLoginUserId() As String
        Get
            Return _updLoginUserId
        End Get
        Set(ByVal value As String)
            _updLoginUserId = value
        End Set
    End Property
#End Region

#Region "�������Ǘ�����"
    ''' <summary>
    ''' �������Ǘ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private _hattyuusyoKanriKengen As Integer
    ''' <summary>
    ''' �������Ǘ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKanriKengen() As Integer
        Get
            Return _hattyuusyoKanriKengen
        End Get
        Set(ByVal value As Integer)
            _hattyuusyoKanriKengen = value
        End Set
    End Property
#End Region

#Region "�o���Ɩ�����"
    ''' <summary>
    ''' �o���Ɩ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private _keiriGyoumuKengen As Integer
    ''' <summary>
    ''' �o���Ɩ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiriGyoumuKengen() As Integer
        Get
            Return _keiriGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            _keiriGyoumuKengen = value
        End Set
    End Property
#End Region

End Class
