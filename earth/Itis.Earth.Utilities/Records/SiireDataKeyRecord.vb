''' <summary>
''' 仕入データテーブルのレコードクラス
''' 検索条件に必要な情報のみ設定
''' </summary>
''' <remarks></remarks>
Public Class SiireDataKeyRecord

#Region "伝票ユニークNO"
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenUnqNo As String
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票ユニークNO</returns>
    ''' <remarks></remarks>
    Public Property DenUnqNo() As String
        Get
            Return strDenUnqNo
        End Get
        Set(ByVal value As String)
            strDenUnqNo = value
        End Set
    End Property
#End Region

#Region "伝票NO From"
    ''' <summary>
    ''' 伝票NO From
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNoFrom As String
    ''' <summary>
    ''' 伝票NO From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票NO From</returns>
    ''' <remarks></remarks>
    Public Property DenNoFrom() As String
        Get
            Return strDenNoFrom
        End Get
        Set(ByVal value As String)
            strDenNoFrom = value
        End Set
    End Property
#End Region

#Region "伝票NO To"
    ''' <summary>
    ''' 伝票NO To
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNoTo As String
    ''' <summary>
    ''' 伝票NO To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票NO To</returns>
    ''' <remarks></remarks>
    Public Property DenNoTo() As String
        Get
            Return strDenNoTo
        End Get
        Set(ByVal value As String)
            strDenNoTo = value
        End Set
    End Property
#End Region

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分</returns>
    ''' <remarks></remarks>
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "番号 From"
    ''' <summary>
    ''' 番号 From
    ''' </summary>
    ''' <remarks></remarks>
    Private strBangouFrom As String
    ''' <summary>
    ''' 番号 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 番号 From</returns>
    ''' <remarks></remarks>
    Public Property BangouFrom() As String
        Get
            Return strBangouFrom
        End Get
        Set(ByVal value As String)
            strBangouFrom = value
        End Set
    End Property
#End Region

#Region "番号 To"
    ''' <summary>
    ''' 番号 From
    ''' </summary>
    ''' <remarks></remarks>
    Private strBangouTo As String
    ''' <summary>
    ''' 番号 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 番号 To</returns>
    ''' <remarks></remarks>
    Public Property BangouTo() As String
        Get
            Return strBangouTo
        End Get
        Set(ByVal value As String)
            strBangouTo = value
        End Set
    End Property
#End Region

#Region "仕入年月日 From"
    ''' <summary>
    ''' 仕入年月日 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSiireDateFrom As DateTime
    ''' <summary>
    ''' 仕入年月日 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入年月日 From</returns>
    ''' <remarks></remarks>
    Public Property SiireDateFrom() As DateTime
        Get
            Return dateSiireDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateSiireDateFrom = value
        End Set
    End Property
#End Region

#Region "仕入年月日 To"
    ''' <summary>
    ''' 仕入年月日 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSiireDateTo As DateTime
    ''' <summary>
    ''' 仕入年月日 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入年月日 To</returns>
    ''' <remarks></remarks>
    Public Property SiireDateTo() As DateTime
        Get
            Return dateSiireDateTo
        End Get
        Set(ByVal value As DateTime)
            dateSiireDateTo = value
        End Set
    End Property
#End Region

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
    Private strSiireSakiMeiKana As String
    ''' <summary>
    ''' 仕入先名カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入先名カナ</returns>
    ''' <remarks></remarks>
    Public Property SiireSakiMeiKana() As String
        Get
            Return strSiireSakiMeiKana
        End Get
        Set(ByVal value As String)
            strSiireSakiMeiKana = value
        End Set
    End Property
#End Region

#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "登録日時 From"
    ''' <summary>
    ''' 登録日時 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetimeFrom As DateTime
    ''' <summary>
    ''' 登録日時 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時 From</returns>
    ''' <remarks></remarks>
    Public Property AddDatetimeFrom() As DateTime
        Get
            Return dateAddDatetimeFrom
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetimeFrom = value
        End Set
    End Property
#End Region

#Region "登録日時 To"
    ''' <summary>
    ''' 登録日時 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetimeTo As DateTime
    ''' <summary>
    ''' 登録日時 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時 To</returns>
    ''' <remarks></remarks>
    Public Property AddDatetimeTo() As DateTime
        Get
            Return dateAddDatetimeTo
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetimeTo = value
        End Set
    End Property
#End Region

#Region "最新伝票表示"
    ''' <summary>
    ''' 最新伝票表示
    ''' </summary>
    ''' <remarks></remarks>
    Private intNewDenpyouDisp As Integer
    ''' <summary>
    ''' 最新伝票表示
    ''' </summary>
    ''' <value></value>
    ''' <returns> 最新伝票表示</returns>
    ''' <remarks></remarks>
    Public Property NewDenpyouDisp() As Integer
        Get
            Return intNewDenpyouDisp
        End Get
        Set(ByVal value As Integer)
            intNewDenpyouDisp = value
        End Set
    End Property
#End Region

#Region "マイナス伝票表示"
    ''' <summary>
    ''' マイナス伝票表示
    ''' </summary>
    ''' <remarks></remarks>
    Private intMinusDenpyouDisp As Integer
    ''' <summary>
    ''' マイナス伝票表示
    ''' </summary>
    ''' <value></value>
    ''' <returns> マイナス伝票表示</returns>
    ''' <remarks></remarks>
    Public Property MinusDenpyouDisp() As Integer
        Get
            Return intMinusDenpyouDisp
        End Get
        Set(ByVal value As Integer)
            intMinusDenpyouDisp = value
        End Set
    End Property
#End Region

#Region "計上済伝票表示"
    ''' <summary>
    ''' 計上済伝票表示
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeijyouZumiDisp As Integer
    ''' <summary>
    ''' 計上済伝票表示
    ''' </summary>
    ''' <value></value>
    ''' <returns> 計上済伝票表示</returns>
    ''' <remarks></remarks>
    Public Property KeijyouZumiDisp() As Integer
        Get
            Return intKeijyouZumiDisp
        End Get
        Set(ByVal value As Integer)
            intKeijyouZumiDisp = value
        End Set
    End Property
#End Region

End Class
