''' <summary>
''' ���i�R�[�h1 �����ݒ�p�p�����[�^���R�[�h
''' </summary>
''' <remarks></remarks>
Public Class Syouhin1InfoRecord
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
    ''' <returns>�敪</returns>
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

#Region "�����X�R�[�h"
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����X�R�[�h</returns>
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

#Region "�c�Ə��R�[�h"
    ''' <summary>
    ''' �c�Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoCd As String
    ''' <summary>
    ''' �c�Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �c�Ə��R�[�h</returns>
    ''' <remarks></remarks>
    Public Property EigyousyoCd() As String
        Get
            Return strEigyousyoCd
        End Get
        Set(ByVal value As String)
            strEigyousyoCd = value
        End Set
    End Property
#End Region

#Region "���i�敪"
    ''' <summary>
    ''' ���i�敪
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhinKbn As Integer
    ''' <summary>
    ''' ���i�敪(���w�莞��9��ݒ肵�Ă�������)
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�敪</returns>
    ''' <remarks>100:���i�敪1 110,115:���i�敪2 120:���i�敪3</remarks>
    Public Property SyouhinKbn() As Integer
        Get
            Return intSyouhinKbn
        End Get
        Set(ByVal value As Integer)
            intSyouhinKbn = value
        End Set
    End Property
#End Region

#Region "�������@NO"
    ''' <summary>
    ''' �������@NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaHouhouNo As Integer
    ''' <summary>
    ''' �������@NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������@NO</returns>
    ''' <remarks></remarks>
    Public Property TyousaHouhouNo() As Integer
        Get
            Return intTyousaHouhouNo
        End Get
        Set(ByVal value As Integer)
            intTyousaHouhouNo = value
        End Set
    End Property
#End Region

#Region "�����T�v"
    ''' <summary>
    ''' �����T�v
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaGaiyou As Integer
    ''' <summary>
    ''' �����T�v(���w�莞��9��ݒ肵�Ă�������)
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����T�v</returns>
    ''' <remarks></remarks>
    Public Property TyousaGaiyou() As Integer
        Get
            Return intTyousaGaiyou
        End Get
        Set(ByVal value As Integer)
            intTyousaGaiyou = value
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
    ''' <returns>���i�R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd")> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "�����p�r"
    ''' <summary>
    ''' �����p�r
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatemonoYouto As Integer
    ''' <summary>
    ''' �����p�r
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����p�r</returns>
    ''' <remarks></remarks>
    Public Property TatemonoYouto() As Integer
        Get
            Return intTatemonoYouto
        End Get
        Set(ByVal value As Integer)
            intTatemonoYouto = value
        End Set
    End Property
#End Region

#Region "������(����or������)"
    ''' <summary>
    ''' ������(����or������)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusaki As String
    ''' <summary>
    ''' ������(����or������)
    ''' </summary>
    ''' <value></value>
    ''' <returns>������(����or������)</returns>
    ''' <remarks></remarks>
    Public Property Seikyuusaki() As String
        Get
            Return strSeikyuusaki
        End Get
        Set(ByVal value As String)
            strSeikyuusaki = value
        End Set
    End Property
#End Region

#Region "�n��FLG"
    ''' <summary>
    ''' �n��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiretuFlg As Integer
    ''' <summary>
    ''' �n��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�n��FLG</returns>
    ''' <remarks></remarks>
    Public Property KeiretuFlg() As Integer
        Get
            Return intKeiretuFlg
        End Get
        Set(ByVal value As Integer)
            intKeiretuFlg = value
        End Set
    End Property
#End Region

#Region "�n����"
    ''' <summary>
    ''' �n����
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' �n����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�n����</returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region

#Region "�H���X���z�P"
    ''' <summary>
    ''' �H���X���z�P
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenKingaku1 As Integer
    ''' <summary>
    ''' �H���X���z�P
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���X���z�P</returns>
    ''' <remarks></remarks>
    Public Property KoumutenKingaku1() As Integer
        Get
            If intKoumutenKingaku1 = Integer.MinValue Then
                intKoumutenKingaku1 = 0
            End If
            Return intKoumutenKingaku1
        End Get
        Set(ByVal value As Integer)
            intKoumutenKingaku1 = value
        End Set
    End Property
#End Region

#Region "�Ŕ����z�P"
    ''' <summary>
    ''' �Ŕ����z�P
    ''' </summary>
    ''' <remarks></remarks>
    Private intZeinukiKingaku1 As Integer
    ''' <summary>
    ''' �Ŕ����z�P
    ''' </summary>
    ''' <value></value>
    ''' <returns>�Ŕ����z�P</returns>
    ''' <remarks></remarks>
    Public Property ZeinukiKingaku1() As Integer
        Get
            If intZeinukiKingaku1 = Integer.MinValue Then
                intZeinukiKingaku1 = 0
            End If
            Return intZeinukiKingaku1
        End Get
        Set(ByVal value As Integer)
            intZeinukiKingaku1 = value
        End Set
    End Property
#End Region

#Region "������к��ށ{���Ə��R�[�h"
    ''' <summary>
    ''' ������к��ށ{���Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ������к��ށ{���Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������к��ށ{���Ə��R�[�h</returns>
    ''' <remarks></remarks>
    Public Overridable Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

    '#Region "������Ў��Ə�����"
    '    ''' <summary>
    '    ''' ������Ў��Ə�����
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private strTysKaisyaJigyousyoCd As String
    '    ''' <summary>
    '    ''' ������Ў��Ə�����
    '    ''' </summary>
    '    ''' <value></value>
    '    ''' <returns> ������Ў��Ə�����</returns>
    '    ''' <remarks></remarks>
    '    Public Overridable Property TysKaisyaJigyousyoCd() As String
    '        Get
    '            Return strTysKaisyaJigyousyoCd
    '        End Get
    '        Set(ByVal value As String)
    '            strTysKaisyaJigyousyoCd = value
    '        End Set
    '    End Property
    '#End Region

#Region "�����˗�����"
    ''' <summary>
    ''' �����˗�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intDoujiIraiTousuu As Integer
    ''' <summary>
    ''' �����˗�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����˗�����</returns>
    ''' <remarks></remarks>
    Public Overridable Property DoujiIraiTousuu() As Integer
        Get
            Return intDoujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuu = value
        End Set
    End Property
#End Region

End Class
