
Partial Public Class TeibetuSyouhinHeaderCtrl
    Inherits System.Web.UI.UserControl

#Region "列挙"
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' 商品１
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN1 = 1
        ''' <summary>
        ''' 商品２
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN2 = 2
        ''' <summary>
        ''' 商品３
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN3 = 3
        ''' <summary>
        ''' 報告書
        ''' </summary>
        ''' <remarks></remarks>
        HOUKOKUSYO = 4
        ''' <summary>
        ''' 保証
        ''' </summary>
        ''' <remarks></remarks>
        HOSYOU = 5
        ''' <summary>
        ''' 保証(解約払戻)
        ''' </summary>
        ''' <remarks></remarks>
        KAIYAKU = 6
    End Enum
#End Region

#Region "プロパティ"
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Private mode As DisplayMode
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>コントロールの表示モード</returns>
    ''' <remarks>商品の種類により画面の表示を変更します</remarks>
    Public Property DispMode() As DisplayMode
        Get
            Return mode
        End Get
        Set(ByVal value As DisplayMode)
            mode = value
        End Set
    End Property
#End Region

#Region "イベント"
    ''' <summary>
    ''' ページロード時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ' 画面表示設定
            Select Case Me.DispMode
                Case DisplayMode.SYOUHIN1
                    ' 商品１の場合
                    TdSyouhinCdTitle.InnerText = TdSyouhinCdTitle.InnerText & "1"
                Case DisplayMode.SYOUHIN2
                    ' 商品２の場合
                    TdSyouhinCdTitle.InnerText = TdSyouhinCdTitle.InnerText & "2"
                Case DisplayMode.SYOUHIN3
                    ' 商品３の場合
                    TdSyouhinCdTitle.InnerText = TdSyouhinCdTitle.InnerText & "3"
                Case DisplayMode.HOUKOKUSYO, DisplayMode.HOSYOU
                    ' 報告書,保証の場合
                    ' 承諾書金額、仕入消費税額、伝票仕入年月日を非表示()
                    TdSyoudakusyoTitle.Style("display") = "none"
                    TdSiireSyouhizeiGakuTitle.Style("display") = "none"
                    TdDenpyouSiireNengappiTitleDisplay.Style("display") = "none"
                    TdDenpyouSiireNengappiTitle.Style("display") = "none"
                    TdSpacer.Style("display") = "inline"                    ' 右端スペーサー

                Case DisplayMode.KAIYAKU
                    ' 保証(解約払戻)の場合
                    TdSyouhinCdTitle.Attributes("rowspan") = 2
                    TdSyouhinNmTitle.Style("display") = "none"
                    SpanSeikyuuUmu.InnerHtml = EarthConst.KAIYAKU_UMU
                    SpanSeikyuuUmu.Style("font-size") = "10px;"

                    ' 承諾書金額、仕入消費税額、伝票仕入年月日を非表示()
                    TdSyoudakusyoTitle.Style("display") = "none"
                    TdSiireSyouhizeiGakuTitle.Style("display") = "none"
                    TdDenpyouSiireNengappiTitleDisplay.Style("display") = "none"
                    TdDenpyouSiireNengappiTitle.Style("display") = "none"

                    TdSpacer.Style("display") = "inline"                    ' 右端スペーサー

                Case Else
            End Select
        End If
    End Sub
#End Region

End Class