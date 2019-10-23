''' <summary>
''' 入金データCSVレコード
''' </summary>
''' <remarks></remarks>
Public Class NyuukinDataCsvRecord

#Region "CSV 取込日時"
    ''' <summary>
    ''' CSV 取込日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dtTorikomiDate As DateTime
    ''' <summary>
    ''' CSV 取込日時
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 取込日時</returns>
    ''' <remarks></remarks>
    Public Property TorikomiDate() As DateTime
        Get
            Return dtTorikomiDate
        End Get
        Set(ByVal value As DateTime)
            dtTorikomiDate = value
        End Set
    End Property
#End Region

#Region "CSV ファイル名"
    ''' <summary>
    ''' CSV ファイル名
    ''' </summary>
    ''' <remarks></remarks>
    Private strCsvFileName As String
    ''' <summary>
    ''' CSV ファイル名
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV ファイル名</returns>
    ''' <remarks></remarks>
    Public Property FileName() As String
        Get
            Return strCsvFileName
        End Get
        Set(ByVal value As String)
            strCsvFileName = value
        End Set
    End Property
#End Region

#Region "CSV 行NO"
    ''' <summary>
    ''' CSV 行NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intRowCnt As Integer
    ''' <summary>
    ''' CSV 行NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 行NO</returns>
    ''' <remarks></remarks>
    Public Property RowCnt() As Integer
        Get
            Return intRowCnt
        End Get
        Set(ByVal value As Integer)
            intRowCnt = value
        End Set
    End Property
#End Region

#Region "CSV 入金日"
    ''' <summary>
    ''' CSV 入金日
    ''' </summary>
    ''' <remarks></remarks>
    Private dtCsvNyuukinDate As DateTime
    ''' <summary>
    ''' CSV 入金日
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 入金日</returns>
    ''' <remarks></remarks>
    Public Property NyuukinDate() As DateTime
        Get
            Return dtCsvNyuukinDate
        End Get
        Set(ByVal value As DateTime)
            dtCsvNyuukinDate = value
        End Set
    End Property
#End Region

#Region "CSV 入金額"
    ''' <summary>
    ''' CSV 入金額
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private intCsvNyuukinGaku As Integer
    ''' <summary>
    ''' CSV 入金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 入金額</returns>
    ''' <remarks></remarks>
    Public Property NyuukinGaku() As Integer
        Get
            Return intCsvNyuukinGaku
        End Get
        Set(ByVal value As Integer)
            intCsvNyuukinGaku = value
        End Set
    End Property
#End Region

#Region "CSV 顧客コード"
    ''' <summary>
    ''' CSV 顧客コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strCsvKokyakuCd As String
    ''' <summary>
    ''' CSV 顧客コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 顧客コード</returns>
    ''' <remarks></remarks>
    Public Property KokyakuCd() As String
        Get
            Return strCsvKokyakuCd
        End Get
        Set(ByVal value As String)
            strCsvKokyakuCd = value
        End Set
    End Property
#End Region

#Region "CSV グループコード"
    ''' <summary>
    ''' CSV グループコード
    ''' </summary>
    ''' <remarks></remarks>
    Private strCsvGroupCd As String
    ''' <summary>
    ''' CSV グループコード
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV グループコード</returns>
    ''' <remarks></remarks>
    Public Property GroupCd() As String
        Get
            Return strCsvGroupCd
        End Get
        Set(ByVal value As String)
            strCsvGroupCd = value
        End Set
    End Property
#End Region

#Region "CSV 手数料額"
    ''' <summary>
    ''' CSV 手数料額
    ''' </summary>
    ''' <remarks></remarks>
    Private intCsvTesuuRyou As Integer
    ''' <summary>
    ''' CSV 手数料額
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 手数料額</returns>
    ''' <remarks></remarks>
    Public Property TesuuRyou() As Integer
        Get
            Return intCsvTesuuRyou
        End Get
        Set(ByVal value As Integer)
            intCsvTesuuRyou = value
        End Set
    End Property
#End Region

#Region "CSV EDI情報"
    ''' <summary>
    ''' CSV  EDI情報
    ''' </summary>
    ''' <remarks></remarks>
    Private strCsvEdiJouhou As String
    ''' <summary>
    ''' CSV  EDI情報
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV EDI情報</returns>
    ''' <remarks></remarks>
    Public Property EdiJouhou() As String
        Get
            Return strCsvEdiJouhou
        End Get
        Set(ByVal value As String)
            strCsvEdiJouhou = value
        End Set
    End Property
#End Region

#Region "CSV 請求名目"
    ''' <summary>
    ''' CSV 請求名目
    ''' </summary>
    ''' <remarks></remarks>
    Private strCsvSeikyuuMeimoku As String
    ''' <summary>
    ''' CSV 請求名目
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 請求名目</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuMeimoku() As String
        Get
            Return strCsvSeikyuuMeimoku
        End Get
        Set(ByVal value As String)
            strCsvSeikyuuMeimoku = value
        End Set
    End Property
#End Region

#Region "CSV 適用"
    ''' <summary>
    ''' CSV 適用
    ''' </summary>
    ''' <remarks></remarks>
    Private strCsvTekiyou As String
    ''' <summary>
    ''' CSV 適用
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 適用</returns>
    ''' <remarks></remarks>
    Public Property Tekiyou() As String
        Get
            Return strCsvTekiyou
        End Get
        Set(ByVal value As String)
            strCsvTekiyou = value
        End Set
    End Property
#End Region

#Region "CSV 区分"
    ''' <summary>
    ''' CSV 区分
    ''' </summary>
    ''' <remarks>CSV 適用の1桁目</remarks>
    Private strCsvKbn As String
    ''' <summary>
    ''' CSV 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 区分</returns>
    ''' <remarks></remarks>
    Public Property Kbn() As String
        Get
            Return strCsvKbn
        End Get
        Set(ByVal value As String)
            strCsvKbn = value
        End Set
    End Property
#End Region

#Region "CSV 保証書NO"
    ''' <summary>
    ''' CSV 保証書NO
    ''' </summary>
    ''' <remarks>CSV 適用の2桁目～11桁目</remarks>
    Private strCsvHosyousyoNo As String
    ''' <summary>
    ''' CSV 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 保証書NO</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNo() As String
        Get
            Return strCsvHosyousyoNo
        End Get
        Set(ByVal value As String)
            strCsvHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "CSV 分類(倉庫)コード"
    ''' <summary>
    ''' CSV 分類(倉庫)コード
    ''' </summary>
    ''' <remarks>CSV 請求名目(商品コード)より紐付けた分類コード</remarks>
    Private strCsvBunruiCd As String
    ''' <summary>
    ''' CSV 分類(倉庫)コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 分類(倉庫)コード</returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As String
        Get
            Return strCsvBunruiCd
        End Get
        Set(ByVal value As String)
            strCsvBunruiCd = value
        End Set
    End Property
#End Region

#Region "CSV 画面表示NO"
    ''' <summary>
    ''' CSV 画面表示NO
    ''' </summary>
    ''' <remarks>CSV 画面表示NO</remarks>
    Private strCsvGamenHyoujiNo As Integer
    ''' <summary>
    ''' CSV 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 画面表示NO</returns>
    ''' <remarks></remarks>
    Public Property GamenHyoujiNo() As Integer
        Get
            Return strCsvGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            strCsvGamenHyoujiNo = value
        End Set
    End Property
#End Region

#Region "CSV 返金額"
    ''' <summary>
    ''' CSV 返金額
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private intCsvHenkinGaku As Integer
    ''' <summary>
    ''' CSV 返金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSV 返金額</returns>
    ''' <remarks></remarks>
    Public Property HenkinGaku() As Integer
        Get
            Return intCsvHenkinGaku
        End Get
        Set(ByVal value As Integer)
            intCsvHenkinGaku = value
        End Set
    End Property
#End Region

End Class
