Public Class SiireSakiInfoRecord

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
    <TableMap("siire_saki_cd")> _
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
    <TableMap("siire_saki_brc")> _
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
    <TableMap("siire_saki_mei")> _
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
    Private strSiireSakiKana As String
    ''' <summary>
    ''' �d���於�J�i
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d���於�J�i</returns>
    ''' <remarks></remarks>
    <TableMap("siire_saki_kana")> _
    Public Property SiireSakiKana() As String
        Get
            Return strSiireSakiKana
        End Get
        Set(ByVal value As String)
            strSiireSakiKana = value
        End Set
    End Property
#End Region

End Class