Partial Public Class TokubetuTaiouToolTipCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private cl As New CommonLogic
    '特別対応マスタロジック
    Dim mttLogic As New TokubetuTaiouMstLogic

#Region "プロパティ"
    ''' <summary>
    ''' 特別対応コード(表示用)Hiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccDisplayCd() As HtmlInputHidden
        Get
            Return hiddenDisplay
        End Get
        Set(ByVal value As HtmlInputHidden)
            hiddenDisplay = value
        End Set
    End Property

    ''' <summary>
    ''' 特別対応コード(更新対象)Hiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTaisyouCd() As HtmlInputHidden
        Get
            Return hiddenTaisyou
        End Get
        Set(ByVal value As HtmlInputHidden)
            hiddenTaisyou = value
        End Set
    End Property

    ''' <summary>
    ''' 更新日時Hiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccUpdDatetime() As HtmlInputHidden
        Get
            Return hiddenUpdDatetime
        End Get
        Set(ByVal value As HtmlInputHidden)
            hiddenUpdDatetime = value
        End Set
    End Property

    ''' <summary>
    ''' 表示切替フラグHiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccVisibleFlg() As HtmlInputHidden
        Get
            Return hiddenVisibleFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            hiddenVisibleFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 表示Labelへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AcclblTokubetuTaiou() As Label
        Get
            Return lblTokubetuTaiou
        End Get
        Set(ByVal value As Label)
            lblTokubetuTaiou = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' ページ描画前処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        '特別対応ラベルのスタイル設定
        SetLabelStyle()

        'ツールチップの設定
        Dim strToolTip As String = GetToolTip()
        'Htmlコントロールにツールチップを設定する
        cl.SetToolTipForCtrl(Me.spanTokubetuTaiou, strToolTip)

    End Sub

#Region "プライベートメソッド"
    ''' <summary>
    ''' 特別対応ラベルスタイルの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetLabelStyle()
        '特別対応コード(表示用)
        Dim strTokubetuTaiou As String = Me.hiddenDisplay.Value

        '表示切替フラグ
        If Me.AccVisibleFlg.Value = String.Empty Then

            '表示用コード
            If strTokubetuTaiou <> String.Empty Then
                '赤字太字
                Me.lblTokubetuTaiou.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
                Me.lblTokubetuTaiou.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD

                '表示
                Me.lblTokubetuTaiou.Visible = True
            Else
                '非表示
                Me.lblTokubetuTaiou.Visible = False
            End If
        Else
            '非表示
            Me.lblTokubetuTaiou.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' 特別対応のツールチップ設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetToolTip() As String

        Dim arrTokubetuTaiouCd() As String = Nothing
        Dim strTokubetuTaiou As String = String.Empty
        Dim intCnt As Integer = 0
        Dim dtRec As New TokubetuTaiouMstRecord
        Dim listMtt As New List(Of TokubetuTaiouMstRecord)
        Dim listResult As New List(Of TokubetuTaiouMstRecord)

        If Me.hiddenDisplay.Value <> String.Empty Then
            arrTokubetuTaiouCd = Split(Me.hiddenDisplay.Value, EarthConst.SEP_STRING)
        End If

        If Not arrTokubetuTaiouCd Is Nothing Then
            For intCnt = 0 To arrTokubetuTaiouCd.Length - 1
                '***************************************
                ' 特別対応マスタ
                '***************************************
                dtRec = New TokubetuTaiouMstRecord

                '特別対応コード
                dtRec.TokubetuTaiouCd = arrTokubetuTaiouCd(intCnt)

                listMtt.Add(dtRec)
            Next
            listResult = mttLogic.GetTokubetuTaiouToolTip(Me, listMtt)

        End If

        '取得した特別対応名称をツールチップ用に編集
        If Not listResult Is Nothing AndAlso listResult.Count <> 0 Then
            For i As Integer = 0 To listResult.Count - 1
                If i > 0 Then
                    strTokubetuTaiou += "" & vbCrLf
                End If
                strTokubetuTaiou += listResult(i).TokubetuTaiouMeisyou
            Next
        End If

        Return strTokubetuTaiou
    End Function

    ''' <summary>
    ''' 表示用のHiddenに特別対応コードを設定
    ''' </summary>
    ''' <param name="strTokubetuTaiouCd">特別対応コード</param>
    ''' <remarks></remarks>
    Public Sub SetDisplayCd(ByVal strTokubetuTaiouCd As String)
        '特別対応コード
        If Me.hiddenDisplay.Value = String.Empty Then
            Me.hiddenDisplay.Value = strTokubetuTaiouCd
        Else
            Me.hiddenDisplay.Value &= EarthConst.SEP_STRING & strTokubetuTaiouCd
        End If
    End Sub

#End Region

End Class