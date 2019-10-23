Partial Public Class TeibetuNyuukinRecordCtrl
    Inherits System.Web.UI.UserControl

#Region "外部からのアクセス用プロパティ"

    ''' <summary>
    ''' TRタグ(行の色分け用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTrRecord() As HtmlTableRow
        Get
            Return Me.trRecord
        End Get
        Set(ByVal value As HtmlTableRow)
            Me.trRecord = value
        End Set
    End Property

    ''' <summary>
    ''' 商品コードテキストボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSyouhinCd() As TextBox
        Get
            Return Me.TextSyouhinCd
        End Get
        Set(ByVal value As TextBox)
            Me.TextSyouhinCd = value
        End Set
    End Property

    ''' <summary>
    ''' 商品名テキストボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSyouhinMei() As TextBox
        Get
            Return Me.TextSyouhinMei
        End Get
        Set(ByVal value As TextBox)
            Me.TextSyouhinMei = value
        End Set
    End Property

    ''' <summary>
    ''' 請求金額テキストボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSeikyuuKingGaku() As TextBox
        Get
            Return Me.TextSeikyuuKingaku
        End Get
        Set(ByVal value As TextBox)
            Me.TextSeikyuuKingaku = value
        End Set
    End Property

    ''' <summary>
    ''' 入金金額テキストボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextNyuukinKinGaku() As TextBox
        Get
            Return Me.TextNyuukinKingaku
        End Get
        Set(ByVal value As TextBox)
            Me.TextNyuukinKingaku = value
        End Set
    End Property

    ''' <summary>
    ''' 返金額テキストボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextHenkinGaku() As TextBox
        Get
            Return Me.TextHenkingaku
        End Get
        Set(ByVal value As TextBox)
            Me.TextHenkingaku = value
        End Set
    End Property

    ''' <summary>
    ''' 残額テキストボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextZanGaku() As TextBox
        Get
            Return Me.TextZangaku
        End Get
        Set(ByVal value As TextBox)
            Me.TextZangaku = value
        End Set
    End Property

    ''' <summary>
    ''' 分類コードHidden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenBunruiCd() As HiddenField
        Get
            Return Me.HiddenBunruiCd
        End Get
        Set(ByVal value As HiddenField)
            Me.HiddenBunruiCd = value
        End Set
    End Property

    ''' <summary>
    ''' 画面表示NoHidden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenGamenHyoujiNo() As HiddenField
        Get
            Return Me.HiddenGamenHyoujiNo
        End Get
        Set(ByVal value As HiddenField)
            Me.HiddenGamenHyoujiNo = value
        End Set
    End Property

    ''' <summary>
    ''' 更新日時Hidden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenUpdDatetime() As HiddenField
        Get
            Return Me.HiddenUpdDatetime
        End Get
        Set(ByVal value As HiddenField)
            Me.HiddenUpdDatetime = value
        End Set
    End Property

#End Region

    Private logic As New TeibetuNyuukinLogic

    ''' <summary>
    ''' ページ初期化処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'JavaScriptの埋め込み
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this))__doPostBack(this.id,'');}else{checkNumberAddFig(this);}"

        Me.TextNyuukinKingaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextHenkingaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextNyuukinKingaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextHenkingaku.Attributes("onblur") = onBlurPostBackScript

        'テキストボックスの読取専用設定
        Me.TextSeikyuuKingaku.Attributes("readonly") = True
        Me.TextSyouhinCd.Attributes("readonly") = True
        Me.TextSyouhinMei.Attributes("readonly") = True

    End Sub
    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '残額の設定
        SetZangaku()
        End Sub

    ''' <summary>
    ''' 商品入金額変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextNyuukinKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextNyuukinKingaku.TextChanged
        '残額の設定
        SetZangaku()
    End Sub

    ''' <summary>
    ''' 商品返金額変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHenkingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextHenkingaku.TextChanged
        '残額の設定
        SetZangaku()
    End Sub

    ''' <summary>
    ''' 残額の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZangaku()
        '残額の設定
        Me.TextZangaku.Text = logic.CalcZanGaku(Me.TextSeikyuuKingaku.Text _
                                                , Me.TextNyuukinKingaku.Text _
                                                , Me.TextHenkingaku.Text)
    End Sub
End Class