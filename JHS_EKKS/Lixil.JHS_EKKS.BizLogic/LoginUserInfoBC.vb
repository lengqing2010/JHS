''' <summary>
'''   ���[�U�[INFOR���X�g
''' </summary>
''' <remarks></remarks>
Public Class LoginUserInfoList
    Private _LoginUserInfoList As New List(Of LoginUserInfoBC)
    Public Property Items() As List(Of LoginUserInfoBC)
        Get
            Return _LoginUserInfoList
        End Get
        Set(ByVal value As List(Of LoginUserInfoBC))
            _LoginUserInfoList = value
        End Set
    End Property
End Class
''' <summary>
''' ���[�U�[INFOR
''' </summary>
''' <remarks></remarks>
Public Class LoginUserInfoBC
#Region "�A�J�E���gNO"
    ''' <summary>
    ''' �A�J�E���gNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intAccountNo As Integer
    ''' <summary>
    ''' �A�J�E���gNO
    ''' </summary>
    ''' <value></value>
    ''' <returns>�A�J�E���gNO</returns>
    ''' <remarks></remarks>
    Public Property AccountNo() As Integer
        Get
            Return intAccountNo
        End Get
        Set(ByVal value As Integer)
            intAccountNo = value
        End Set
    End Property
#End Region

#Region "���"
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <value></value>
    ''' <returns>���</returns>
    ''' <remarks></remarks>
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region
#Region "�A�J�E���g"
    ''' <summary>
    ''' �A�J�E���g
    ''' </summary>
    ''' <remarks></remarks>
    Private strAccount As String
    ''' <summary>
    ''' �A�J�E���g
    ''' </summary>
    ''' <value></value>
    ''' <returns>�A�J�E���g</returns>
    ''' <remarks></remarks>
    Public Property Account() As String
        Get
            Return strAccount
        End Get
        Set(ByVal value As String)
            strAccount = value
        End Set
    End Property
#End Region
#Region "����"
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <remarks></remarks>
    Private strSimei As String
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <value></value>
    ''' <returns>����</returns>
    ''' <remarks></remarks>
    Public Property Simei() As String
        Get
            Return strSimei
        End Get
        Set(ByVal value As String)
            strSimei = value
        End Set
    End Property
#End Region

#Region "��������"
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyozokuBusyo As String
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <value></value>
    ''' <returns>��������</returns>
    ''' <remarks></remarks>

    Public Property SyozokuBusyo() As String
        Get
            Return strSyozokuBusyo
        End Get
        Set(ByVal value As String)
            strSyozokuBusyo = value
        End Set
    End Property
#End Region
#Region "���l"
    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <value></value>
    ''' <returns>���l</returns>
    ''' <remarks></remarks>
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "�c�ƌv��Ǘ�_�Q�ƌ���"
    ''' <summary>
    ''' �c�ƌv��Ǘ�_�Q�ƌ���
    ''' </summary>
    ''' <remarks></remarks>
    Private intEigyouKeikakuKenriSansyou As Integer
    ''' <summary>
    ''' �c�ƌv��Ǘ�_�Q�ƌ���
    ''' </summary>
    ''' <value></value>
    ''' <returns>�c�ƌv��Ǘ�_�Q�ƌ���</returns>
    ''' <remarks></remarks>
    Public Property EigyouKeikakuKenriSansyou() As Integer
        Get
            Return intEigyouKeikakuKenriSansyou
        End Get
        Set(ByVal value As Integer)
            intEigyouKeikakuKenriSansyou = value
        End Set
    End Property
#End Region
#Region "����\��_�Q�ƌ���"
    ''' <summary>
    ''' ����\��_�Q�ƌ���
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriYojituKanriSansyou As Integer
    ''' <summary>
    ''' ����\��_�Q�ƌ���
    ''' </summary>
    ''' <value></value>
    ''' <returns>����\��_�Q�ƌ���</returns>
    ''' <remarks></remarks>
    Public Property UriYojituKanriSansyou() As Integer
        Get
            Return intUriYojituKanriSansyou
        End Get
        Set(ByVal value As Integer)
            intUriYojituKanriSansyou = value
        End Set
    End Property
#End Region
#Region "�S�Ќv��_�m�茠��"
    ''' <summary>
    ''' �S�Ќv��_�m�茠��
    ''' </summary>
    ''' <remarks></remarks>
    Private intZensyaKeikakuKengen As Integer
    ''' <summary>
    ''' �S�Ќv��_�m�茠��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�S�Ќv��_�m�茠��</returns>
    ''' <remarks></remarks>
    Public Property ZensyaKeikakuKengen() As Integer
        Get
            Return intZensyaKeikakuKengen
        End Get
        Set(ByVal value As Integer)
            intZensyaKeikakuKengen = value
        End Set
    End Property
#End Region

#Region "�x�X�ʔN�x�v��_�m�茠��"
    ''' <summary>
    '''�x�X�ʔN�x�v��_�m�茠��
    ''' </summary>
    ''' <remarks></remarks>
    Private intSitenbetuNenKeikakuKengen As Integer
    ''' <summary>
    ''' �x�X�ʔN�x�v��_�m�茠��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�x�X�ʔN�x�v��_�m�茠��</returns>
    ''' <remarks></remarks>
    Public Property SitenbetuNenKeikakuKengen() As Integer
        Get
            Return intSitenbetuNenKeikakuKengen
        End Get
        Set(ByVal value As Integer)
            intSitenbetuNenKeikakuKengen = value
        End Set
    End Property
#End Region

#Region "�x�X�ʌ����v��_�捞����"
    ''' <summary>
    '''�x�X�ʌ����v��_�捞����
    ''' </summary>
    ''' <remarks></remarks>
    Private intSitenbetuGetujiKeikakuTorikomi As Integer
    ''' <summary>
    ''' �x�X�ʌ����v��_�捞����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�x�X�ʌ����v��_�捞����</returns>
    ''' <remarks></remarks>
    Public Property SitenbetuGetujiKeikakuTorikomi() As Integer
        Get
            Return intSitenbetuGetujiKeikakuTorikomi
        End Get
        Set(ByVal value As Integer)
            intSitenbetuGetujiKeikakuTorikomi = value
        End Set
    End Property
#End Region

#Region "�x�X�ʌ����v��_�m�茠��"
    ''' <summary>
    '''�x�X�ʌ����v��_�m�茠��
    ''' </summary>
    ''' <remarks></remarks>
    Private intSitenbetuGetujiKeikakuKakutei As Integer
    ''' <summary>
    ''' �x�X�ʌ����v��_�m�茠��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�x�X�ʌ����v��_�m�茠��</returns>
    ''' <remarks></remarks>
    Public Property SitenbetuGetujiKeikakuKakutei() As Integer
        Get
            Return intSitenbetuGetujiKeikakuKakutei
        End Get
        Set(ByVal value As Integer)
            intSitenbetuGetujiKeikakuKakutei = value
        End Set
    End Property
#End Region

#Region "�x�X�ʌ����v��_����������"
    ''' <summary>
    '''�x�X�ʌ����v��_����������
    ''' </summary>
    ''' <remarks></remarks>
    Private intSitenbetuGetujiKeikakuMinaosi As Integer
    ''' <summary>
    ''' �x�X�ʌ����v��_����������
    ''' </summary>
    ''' <value></value>
    ''' <returns>�x�X�ʌ����v��_����������</returns>
    ''' <remarks></remarks>
    Public Property SitenbetuGetujiKeikakuMinaosi() As Integer
        Get
            Return intSitenbetuGetujiKeikakuMinaosi
        End Get
        Set(ByVal value As Integer)
            intSitenbetuGetujiKeikakuMinaosi = value
        End Set
    End Property
#End Region

#Region "�v��l������_����"
    ''' <summary>
    ''' �v��l������_����
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeikakuMinaosiKengen As Integer
    ''' <summary>
    ''' �v��l������_����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�v��l������_����</returns>
    ''' <remarks></remarks>
    Public Property KeikakuMinaosiKengen() As Integer
        Get
            Return intKeikakuMinaosiKengen
        End Get
        Set(ByVal value As Integer)
            intKeikakuMinaosiKengen = value
        End Set
    End Property
#End Region

#Region "�v��l�m��_����"
    ''' <summary>
    ''' �v��l�m��_����
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeikakuKakuteiKengen As Integer
    ''' <summary>
    '''�v��l�m��_����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�v��l�m��_����</returns>
    ''' <remarks></remarks>
    Public Property KeikakuKakuteiKengen() As Integer
        Get
            Return intKeikakuKakuteiKengen
        End Get
        Set(ByVal value As Integer)
            intKeikakuKakuteiKengen = value
        End Set
    End Property
#End Region

#Region "�x�X�ʌ��ʌv��_�m�茠��"
    ''' <summary>
    ''' �x�X�ʌ��ʌv��_�m�茠��
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeikakuTorikomiKengen As Integer
    ''' <summary>
    '''�x�X�ʌ��ʌv��_�m�茠��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�x�X�ʌ��ʌv��_�m�茠��</returns>
    ''' <remarks></remarks>
    Public Property KeikakuTorikomiKengen() As Integer
        Get
            Return intKeikakuTorikomiKengen
        End Get
        Set(ByVal value As Integer)
            intKeikakuTorikomiKengen = value
        End Set
    End Property
#End Region

End Class
