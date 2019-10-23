Public Partial Class NyuukinZangakuCtrl
    Inherits System.Web.UI.UserControl

#Region "プロパティ"
    ''' <summary>
    ''' 入金額タイトル判定
    ''' </summary>
    ''' <remarks>true:入金額 false:返金額</remarks>
    Private _isNyuukingaku As Boolean = True
    ''' <summary>
    ''' 入金額タイトル判定
    ''' </summary>
    ''' <value></value>
    ''' <returns>true:入金額 false:返金額</returns>
    ''' <remarks></remarks>
    Public Property isNyuukingaku() As Boolean
        Get
            Return _isNyuukingaku
        End Get
        Set(ByVal value As Boolean)
            _isNyuukingaku = value
        End Set
    End Property

    ''' <summary>
    ''' 入金額(返金額)コントロールへのアクセサ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NyuukinGaku() As String
        Get
            Return TextNyuukinGaku.Value
        End Get
        Set(ByVal value As String)
            TextNyuukinGaku.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 残額コントロールへのアクセサ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ZanGaku() As String
        Get
            Return TextZanGaku.Value
        End Get
        Set(ByVal value As String)
            TextZanGaku.Value = value
        End Set
    End Property

    ''' <summary>
    ''' UpdatePanel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdateZangakuPanel() As UpdatePanel
        Get
            Return UpdatePanelZangaku
        End Get
        Set(ByVal value As UpdatePanel)
            UpdatePanelZangaku = value
        End Set
    End Property


#End Region

    ''' <summary>
    ''' ページロード時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            ' 入金額タイトルの設定
            If _isNyuukingaku = True Then
                ' 入金額
                SpanNyuukinTitle.InnerText = Earth.Utilities.EarthConst.NYUUKINGAKU_ZEIKOMI
            Else
                ' 返金額
                SpanNyuukinTitle.InnerText = Earth.Utilities.EarthConst.HENKINGAKU_ZEIKOMI
            End If
        End If
    End Sub

    ''' <summary>
    ''' 入金額・残額を表示します
    ''' </summary>
    ''' <param name="uriageGoukeiGaku">税込売上金額合計</param>
    ''' <param name="nyuukinGaku">入金額</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku(ByVal uriageGoukeiGaku As Integer, _
                           ByVal nyuukinGaku As Integer)

        ' NULLは０にする
        uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)
        nyuukinGaku = IIf(nyuukinGaku = Integer.MinValue, 0, nyuukinGaku)

        ' 入金額
        TextNyuukinGaku.Value = nyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' 残額
        TextZanGaku.Value = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

    ''' <summary>
    ''' 入金額・残額を表示します<br/>
    ''' 税込売上金額合計のみ変更し、再計算します
    ''' </summary>
    ''' <param name="uriageGoukeiGaku">税込売上金額合計</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku(ByVal uriageGoukeiGaku As Integer)

        Dim strNyuukinGaku As String = IIf(TextNyuukinGaku.Value.Replace(",", "").Trim() = "", _
                                        "0", _
                                        TextNyuukinGaku.Value.Replace(",", "").Trim())

        Dim nyuukinGaku As Integer = Integer.Parse(strNyuukinGaku)

        ' NULLは０にする
        uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)

        ' 残額
        TextZanGaku.Value = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub


End Class