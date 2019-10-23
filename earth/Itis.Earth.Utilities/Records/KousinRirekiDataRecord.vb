''' <summary>
''' 更新履歴データレコードクラス/更新履歴テーブル画面
''' </summary>
''' <remarks>更新履歴データの格納時に使用します</remarks>


Public Class KousinRirekiDataRecord


#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKousinNitiji As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime")> _
    Public Property KousinNitiji() As DateTime
        Get
            Return dateKousinNitiji
        End Get
        Set(ByVal value As DateTime)
            dateKousinNitiji = value
        End Set
    End Property
#End Region

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKubun As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分</returns>
    ''' <remarks></remarks>
    <TableMap("kbn")> _
    Public Property Kubun() As String
        Get
            Return strKubun
        End Get
        Set(ByVal value As String)
            strKubun = value
        End Set
    End Property
#End Region

#Region "保証書No"
    ''' <summary>
    ''' 保証書No
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' 保証書No
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書No</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no")> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "更新項目"
    ''' <summary>
    ''' 更新項目
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinKoumoku As String
    ''' <summary>
    ''' 更新項目
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新項目</returns>
    ''' <remarks></remarks>
    <TableMap("upd_koumoku")> _
    Public Property KousinKoumoku() As String
        Get
            Return strKousinKoumoku
        End Get
        Set(ByVal value As String)
            strKousinKoumoku = value
        End Set
    End Property
#End Region

#Region "更新前値"
    ''' <summary>
    ''' 更新前値
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinPreValue As String
    ''' <summary>
    ''' 更新前値
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新前値</returns>
    ''' <remarks></remarks>
    <TableMap("upd_mae_atai")> _
    Public Property KousinPreValue() As String
        Get
            Return strKousinPreValue
        End Get
        Set(ByVal value As String)
            strKousinPreValue = value
        End Set
    End Property
#End Region

#Region "更新後値"
    ''' <summary>
    ''' 更新後値
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinPostValue As String
    ''' <summary>
    ''' 更新後値
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新後値</returns>
    ''' <remarks></remarks>
    <TableMap("upd_go_atai")> _
    Public Property KousinPostValue() As String
        Get
            Return strKousinPostValue
        End Get
        Set(ByVal value As String)
            strKousinPostValue = value
        End Set
    End Property
#End Region

#Region "更新者"
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String
    ''' <summary>
    ''' 更新者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新者</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya")> _
    Public Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

End Class
