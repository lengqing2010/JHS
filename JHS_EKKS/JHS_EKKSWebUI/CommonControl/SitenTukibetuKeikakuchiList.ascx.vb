Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase

Partial Class CommonControl_SitenTukibetuKeikakuchiList
    Inherits System.Web.UI.UserControl


#Region "プロパティ"

    Private _marginLeft As String
    ''' <summary>
    ''' MarginLeft
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MarginLeft() As String

        Set(ByVal value As String)
            _marginLeft = value
            Me.tblId.Style.Item("margin-left") = (40 + value.Replace("px", "")).ToString + "px"
            'Me.tblId.Style.Item("margin-left") = value
        End Set

    End Property

    Private _marginTop As String
    ''' <summary>
    ''' MarginTop
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MarginTop() As String

        Set(ByVal value As String)
            _marginTop = value
            Me.tblId.Style.Item("margin-top") = (-110 + value.Replace("px", "")).ToString + "px"
            'Me.tblId.Style.Item("margin-top") = value
        End Set

    End Property

    ''' <summary>
    ''' Title visible 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property TitleVisable() As Boolean

        Set(ByVal value As Boolean)
            Me.tblTitle.Visible = value
            If Not value Then
                Me.tblId.Style.Item("margin-left") = _marginLeft
                Me.tblId.Style.Item("margin-top") = _marginTop
            End If
        End Set

    End Property

    ''' <summary>
    ''' 白色のエクセル
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property backcolorWhite() As Boolean

        Set(ByVal value As Boolean)
            If value Then
                Me.tdEigyouKeikakuKensuu.Style.Item("background-color") = "#FFFFFF"
                Me.tdEigyouKeikakuUriKingaku.Style.Item("background-color") = "#FFFFFF"
                Me.tdEigyouKeikakuArari.Style.Item("background-color") = "#FFFFFF"
                Me.tdFCKeikakuKensuu.Style.Item("background-color") = "#FFFFFF"
                Me.tdFCKeikakuUriKingaku.Style.Item("background-color") = "#FFFFFF"
                Me.tdFCKeikakuArari.Style.Item("background-color") = "#FFFFFF"
                Me.tdTokuhanKeikakuKensuu.Style.Item("background-color") = "#FFFFFF"
                Me.tdTokuhanKeikakuUriKingaku.Style.Item("background-color") = "#FFFFFF"
                Me.tdTokuhanKeikakuArari.Style.Item("background-color") = "#FFFFFF"
            End If
        End Set

    End Property

    Private _dtInfo As Data.DataTable
    ''' <summary>
    ''' リスト情報
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property dtInfo() As Data.DataTable
        Set(ByVal value As Data.DataTable)
            _dtInfo = value
        End Set
    End Property

#End Region

#Region "イベント"
    '''' <summary>
    '''' ページロード
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'End Sub

#End Region

#Region "Publicメソッド"
    ''' <summary>
    ''' 明細データ表示処理
    ''' </summary>
    ''' <param name="dtKeikaku">明細データ</param>
    ''' <remarks></remarks>
    Public Sub ShowMeisaiData(ByVal dtKeikaku As Data.DataTable, ByVal dtLastYear As Data.DataTable, ByVal getu As String)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                     MyMethod.GetCurrentMethod.Name)

        '前年
        If dtLastYear.Rows.Count = 0 Then
            Select Case getu
                Case "4", "5", "6", "7", "8", "9", "10", "11", "12", "1", "2", "3"
                    '営業_調査件数
                    Me.lblEigyouKensuuBefore.Text = String.Empty
                    '特販_調査件数
                    Me.lblTokuhanKensuuBefore.Text = String.Empty
                    'FC_調査件数
                    Me.lblFCKensuuBefore.Text = String.Empty
                    '営業_売上金額
                    Me.lblEigyouUriKingakuBefore.Text = String.Empty
                    '特販_売上金額
                    Me.lblTokuhanUriKingakuBefore.Text = String.Empty
                    'FC_売上金額
                    Me.lblFCUriKingakuBefore.Text = String.Empty
                    '営業_粗利
                    Me.lblEigyouArariBefore.Text = String.Empty
                    '特販_粗利
                    Me.lblTokuhanArariBefore.Text = String.Empty
                    'FC_粗利
                    Me.lblFCArariBefore.Text = String.Empty
                Case Else
                    '営業_調査件数
                    Me.lblEigyouKensuuBefore.Text = "0"
                    '特販_調査件数
                    Me.lblTokuhanKensuuBefore.Text = "0"
                    'FC_調査件数
                    Me.lblFCKensuuBefore.Text = "0"
                    '営業_売上金額
                    Me.lblEigyouUriKingakuBefore.Text = "0"
                    '特販_売上金額
                    Me.lblTokuhanUriKingakuBefore.Text = "0"
                    'FC_売上金額
                    Me.lblFCUriKingakuBefore.Text = "0"
                    '営業_粗利
                    Me.lblEigyouArariBefore.Text = "0"
                    '特販_粗利
                    Me.lblTokuhanArariBefore.Text = "0"
                    'FC_粗利
                    Me.lblFCArariBefore.Text = "0"
            End Select
            '合計
            Me.lblGoukeiKensuuBefore.Text = "0"
            Me.lblGoukeiUriKingakuBefore.Text = "0"
            Me.lblGoukeiArariBefore.Text = "0"
        Else
            With dtLastYear
                '営業_調査件数
                Me.lblEigyouKensuuBefore.Text = FormatComma(.Rows(0).Item(getu & "gatu_jisseki_kensuu").ToString)
                '特販_調査件数
                Me.lblTokuhanKensuuBefore.Text = FormatComma(.Rows(1).Item(getu & "gatu_jisseki_kensuu").ToString)
                'FC_調査件数
                Me.lblFCKensuuBefore.Text = FormatComma(.Rows(2).Item(getu & "gatu_jisseki_kensuu").ToString)
                '営業_売上金額
                Me.lblEigyouUriKingakuBefore.Text = FormatComma(.Rows(0).Item(getu & "gatu_jisseki_kingaku").ToString)
                '特販_売上金額
                Me.lblTokuhanUriKingakuBefore.Text = FormatComma(.Rows(1).Item(getu & "gatu_jisseki_kingaku").ToString)
                'FC_売上金額
                Me.lblFCUriKingakuBefore.Text = FormatComma(.Rows(2).Item(getu & "gatu_jisseki_kingaku").ToString)
                '営業_粗利
                Me.lblEigyouArariBefore.Text = FormatComma(.Rows(0).Item(getu & "gatu_jisseki_arari").ToString)
                '特販_粗利
                Me.lblTokuhanArariBefore.Text = FormatComma(.Rows(1).Item(getu & "gatu_jisseki_arari").ToString)
                'FC_粗利
                Me.lblFCArariBefore.Text = FormatComma(.Rows(2).Item(getu & "gatu_jisseki_arari").ToString)
                '合計
                Me.lblGoukeiKensuuBefore.Text = SetGoukei(.Rows(0).Item(getu & "gatu_jisseki_kensuu").ToString, _
                                                          .Rows(1).Item(getu & "gatu_jisseki_kensuu").ToString, _
                                                          .Rows(2).Item(getu & "gatu_jisseki_kensuu").ToString)

                Me.lblGoukeiUriKingakuBefore.Text = SetGoukei(.Rows(0).Item(getu & "gatu_jisseki_kingaku").ToString, _
                                                              .Rows(1).Item(getu & "gatu_jisseki_kingaku").ToString, _
                                                              .Rows(2).Item(getu & "gatu_jisseki_kingaku").ToString)

                Me.lblGoukeiArariBefore.Text = SetGoukei(.Rows(0).Item(getu & "gatu_jisseki_arari").ToString, _
                                                         .Rows(1).Item(getu & "gatu_jisseki_arari").ToString, _
                                                         .Rows(2).Item(getu & "gatu_jisseki_arari").ToString)
            End With
        End If

        If dtKeikaku.Rows.Count = 0 Then
            Select Case getu
                Case "4", "5", "6", "7", "8", "9", "10", "11", "12", "1", "2", "3"
                    '営業_計画調査件数
                    Me.lblEigyouKeikakuKensuu.Text = String.Empty
                    '特販_計画調査件数
                    Me.lblTokuhanKeikakuKensuu.Text = String.Empty
                    'FC_計画調査件数
                    Me.lblFCKeikakuKensuu.Text = String.Empty
                    '営業_計画売上金額
                    Me.lblEigyouKeikakuUriKingaku.Text = String.Empty
                    '特販_計画売上金額
                    Me.lblTokuhanKeikakuUriKingaku.Text = String.Empty
                    'FC_計画売上金額
                    Me.lblFCKeikakuUriKingaku.Text = String.Empty
                    '営業_計画粗利
                    Me.lblEigyouKeikakuArari.Text = String.Empty
                    '特販_計画粗利
                    Me.lblTokuhanKeikakuArari.Text = String.Empty
                    'FC_計画粗利
                    Me.lblFCKeikakuArari.Text = String.Empty
                Case Else
                    '営業_計画調査件数
                    Me.lblEigyouKeikakuKensuu.Text = "0"
                    '特販_計画調査件数
                    Me.lblTokuhanKeikakuKensuu.Text = "0"
                    'FC_計画調査件数
                    Me.lblFCKeikakuKensuu.Text = "0"
                    '営業_計画売上金額
                    Me.lblEigyouKeikakuUriKingaku.Text = "0"
                    '特販_計画売上金額
                    Me.lblTokuhanKeikakuUriKingaku.Text = "0"
                    'FC_計画売上金額
                    Me.lblFCKeikakuUriKingaku.Text = "0"
                    '営業_計画粗利
                    Me.lblEigyouKeikakuArari.Text = "0"
                    '特販_計画粗利
                    Me.lblTokuhanKeikakuArari.Text = "0"
                    'FC_計画粗利
                    Me.lblFCKeikakuArari.Text = "0"
            End Select
            '合計
            Me.lblGoukeiKeikakuKensuu.Text = "0"
            Me.lblGoukeiKeikakuUriKingaku.Text = "0"
            Me.lblGoukeiKeikakuArari.Text = "0"
        Else
            '計画
            With dtKeikaku
                '営業_計画調査件数
                Me.lblEigyouKeikakuKensuu.Text = FormatComma(.Rows(0).Item(getu & "gatu_keikaku_kensuu").ToString)
                '特販_計画調査件数
                Me.lblTokuhanKeikakuKensuu.Text = FormatComma(.Rows(1).Item(getu & "gatu_keikaku_kensuu").ToString)
                'FC_計画調査件数
                Me.lblFCKeikakuKensuu.Text = FormatComma(.Rows(2).Item(getu & "gatu_keikaku_kensuu").ToString)
                '営業_計画売上金額
                Me.lblEigyouKeikakuUriKingaku.Text = FormatComma(.Rows(0).Item(getu & "gatu_keikaku_kingaku").ToString)
                '特販_計画売上金額
                Me.lblTokuhanKeikakuUriKingaku.Text = FormatComma(.Rows(1).Item(getu & "gatu_keikaku_kingaku").ToString)
                'FC_計画売上金額
                Me.lblFCKeikakuUriKingaku.Text = FormatComma(.Rows(2).Item(getu & "gatu_keikaku_kingaku").ToString)
                '営業_計画粗利
                Me.lblEigyouKeikakuArari.Text = FormatComma(.Rows(0).Item(getu & "gatu_keikaku_arari").ToString)
                '特販_計画粗利
                Me.lblTokuhanKeikakuArari.Text = FormatComma(.Rows(1).Item(getu & "gatu_keikaku_arari").ToString)
                'FC_計画粗利
                Me.lblFCKeikakuArari.Text = FormatComma(.Rows(2).Item(getu & "gatu_keikaku_arari").ToString)
                '合計
                Me.lblGoukeiKeikakuKensuu.Text = SetGoukei(.Rows(0).Item(getu & "gatu_keikaku_kensuu").ToString, _
                                                           .Rows(1).Item(getu & "gatu_keikaku_kensuu").ToString, _
                                                           .Rows(2).Item(getu & "gatu_keikaku_kensuu").ToString)

                Me.lblGoukeiKeikakuUriKingaku.Text = SetGoukei(.Rows(0).Item(getu & "gatu_keikaku_kingaku").ToString, _
                                                               .Rows(1).Item(getu & "gatu_keikaku_kingaku").ToString, _
                                                               .Rows(2).Item(getu & "gatu_keikaku_kingaku").ToString)

                Me.lblGoukeiKeikakuArari.Text = SetGoukei(.Rows(0).Item(getu & "gatu_keikaku_arari").ToString, _
                                                          .Rows(1).Item(getu & "gatu_keikaku_arari").ToString, _
                                                          .Rows(2).Item(getu & "gatu_keikaku_arari").ToString)
            End With

        End If

        'タイトル
        Select Case getu
            Case "4", "5", "6", "7", "8", "9", "10", "11", "12", "1", "2", "3"
                Me.lblTitle.Text = getu & "月"
            Case "456"
                Me.lblTitle.Text = "第一_四半期"
            Case "789"
                Me.lblTitle.Text = "第二_四半期"
            Case "101112"
                Me.lblTitle.Text = "第三_四半期"
            Case "123"
                Me.lblTitle.Text = "第四_四半期"
            Case "kamiki"
                Me.lblTitle.Text = "上期"
            Case "simoki"
                Me.lblTitle.Text = "下期"
            Case "nendo"
                Me.lblTitle.Text = "年度集計"
        End Select

    End Sub

    ''' <summary>
    ''' 画面合計のセット
    ''' </summary>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Private Function SetGoukei(ByVal strEigyou As String, ByVal strTokuhan As String, ByVal strFC As String) As String

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                     MyMethod.GetCurrentMethod.Name, strEigyou, strTokuhan, strFC)

        Dim decGoukei As Decimal = 0
        '営業
        If strEigyou.Trim <> String.Empty Then
            decGoukei = decGoukei + Convert.ToDecimal(strEigyou)
        End If
        '特販
        If strTokuhan.Trim <> String.Empty Then
            decGoukei = decGoukei + Convert.ToDecimal(strTokuhan)
        End If
        'ＦＣ
        If strFC.Trim <> String.Empty Then
            decGoukei = decGoukei + Convert.ToDecimal(strFC)
        End If

        Return FormatNumber(decGoukei, 0)

    End Function

    ''' <summary>
    ''' 金額をカンマ区切り文字列で表示する(小数点考慮版)
    ''' </summary>
    ''' <param name="money">金額</param>
    ''' <returns>フォーマット後文字列</returns>
    ''' <remarks></remarks>
    Public Function FormatComma(ByVal money As String) As String

        If String.IsNullOrEmpty(money) = True Then

            Return String.Empty
        Else

            Return FormatNumber(money, 0)
        End If

    End Function

#End Region

End Class
