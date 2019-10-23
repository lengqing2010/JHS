Public Class SiireSakiInfoRecord

#Region "仕入先コード"
    ''' <summary>
    ''' 仕入先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSiireSakiCd As String
    ''' <summary>
    ''' 仕入先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入先コード</returns>
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

#Region "仕入先枝番"
    ''' <summary>
    ''' 仕入先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strSiireSakiBrc As String
    ''' <summary>
    ''' 仕入先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入先枝番</returns>
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

#Region "仕入先名"
    ''' <summary>
    ''' 仕入先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSiireSakiMei As String
    ''' <summary>
    ''' 仕入先名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入先名</returns>
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

#Region "仕入先名カナ"
    ''' <summary>
    ''' 仕入先名カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSiireSakiKana As String
    ''' <summary>
    ''' 仕入先名カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入先名カナ</returns>
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