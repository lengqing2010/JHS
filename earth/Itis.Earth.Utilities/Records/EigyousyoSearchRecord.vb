''' <summary>
''' �c�Ə��}�X�^�������R�[�h�N���X
''' </summary>
''' <remarks></remarks>
Public Class EigyousyoSearchRecord

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
    <TableMap("eigyousyo_cd")> _
    Public Property EigyousyoCd() As String
        Get
            Return strEigyousyoCd
        End Get
        Set(ByVal value As String)
            strEigyousyoCd = value
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
    ''' <returns> ���</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi")> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "�c�Ə���"
    ''' <summary>
    ''' �c�Ə���
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoMei As String
    ''' <summary>
    ''' �c�Ə���
    ''' </summary>
    ''' <value></value>
    ''' <returns> �c�Ə���</returns>
    ''' <remarks></remarks>
    <TableMap("eigyousyo_mei")> _
    Public Property EigyousyoMei() As String
        Get
            Return strEigyousyoMei
        End Get
        Set(ByVal value As String)
            strEigyousyoMei = value
        End Set
    End Property
#End Region

#Region "�c�Ə��J�i"
    ''' <summary>
    ''' �c�Ə��J�i
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoKana As String
    ''' <summary>
    ''' �c�Ə��J�i
    ''' </summary>
    ''' <value></value>
    ''' <returns> �c�Ə��J�i</returns>
    ''' <remarks></remarks>
    <TableMap("eigyousyo_kana")> _
    Public Property EigyousyoKana() As String
        Get
            Return strEigyousyoKana
        End Get
        Set(ByVal value As String)
            strEigyousyoKana = value
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
    ''' <returns>������R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd")> _
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
    ''' <returns>������}��</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc")> _
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
    ''' <returns>������敪</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
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
    ''' <returns>�����於</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei")> _
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "������J�i"
    ''' <summary>
    ''' ������J�i
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKana As String
    ''' <summary>
    ''' ������J�i
    ''' </summary>
    ''' <value></value>
    ''' <returns>������J�i</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kana")> _
    Public Property SeikyuuSakiKana() As String
        Get
            Return strSeikyuuSakiKana
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKana = value
        End Set
    End Property
#End Region

End Class