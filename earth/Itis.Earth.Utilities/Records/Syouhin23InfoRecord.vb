''' <summary>
''' ���i�R�[�h2 �����ݒ�p�p�����[�^���R�[�h
''' </summary>
''' <remarks></remarks>
Public Class Syouhin23InfoRecord

#Region "���i�Q���R�[�h"
    ''' <summary>
    ''' ���i�Q���R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private objSyouhin2Rec As Syouhin23Record
    ''' <summary>
    ''' ���i�Q���R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�Q���R�[�h</returns>
    ''' <remarks></remarks>
    Public Property Syouhin2Rec() As Syouhin23Record
        Get
            Return objSyouhin2Rec
        End Get
        Set(ByVal value As Syouhin23Record)
            objSyouhin2Rec = value
        End Set
    End Property
#End Region
#Region "������"
    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusaki As String
    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <value></value>
    ''' <returns>������</returns>
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
#Region "�����L��"
    ''' <summary>
    ''' �����L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuuUmu As Integer
    ''' <summary>
    ''' �����L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����L��</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmu() As Integer
        Get
            Return intSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intSeikyuuUmu = value
        End Set
    End Property
#End Region
#Region "�������m��FLG"
    ''' <summary>
    ''' �������m��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoKakuteiFlg As Integer
    ''' <summary>
    ''' �������m��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������m��FLG</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuteiFlg() As Integer
        Get
            Return intHattyuusyoKakuteiFlg
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoKakuteiFlg = value
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
End Class
