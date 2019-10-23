''' <summary>
''' 仕入データ照会の検索結果格納用レコード
''' </summary>
''' <remarks></remarks>
Public Class SiireSearchResultRecord
    Inherits SiireDataRecord

#Region "地盤T.施主名"
    ''' <summary>
    ''' 地盤T.施主名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 施主名</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei")> _
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "仕入処理FLG"
    ''' <summary>
    ''' 仕入処理FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireKeijyouFlg As String
    ''' <summary>
    ''' 仕入処理FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入処理FLG</returns>
    ''' <remarks></remarks>
    <TableMap("siire_keijyou_flg")> _
    Public Property SiireKeijyouFlg() As Integer
        Get
            Return intSiireKeijyouFlg
        End Get
        Set(ByVal value As Integer)
            intSiireKeijyouFlg = value
        End Set
    End Property
#End Region

End Class
