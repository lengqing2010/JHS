Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Data
Imports System.Collections.Generic
Partial Class UriageYojituKanriMenu
    Inherits System.Web.UI.Page

#Region "イベント"
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC

    ''' <summary>
    ''' 初期表示
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>																
    ''' <para>2012/11/16 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        Dim CommonCheck As New CommonCheck
        CommonCheck.CommonNinsyou(String.Empty, Master.loginUserInfo, kegen.UserIdOnly)
        '全社 集計ボタンの字体を太字に設定
        btnZensya.Button.Style("font-weight") = "bold"
        '各種 集計ボタンの字体を太字に設定
        btnKakusyu.Button.Style("font-weight") = "bold"

        Call SetJsEvent()

    End Sub

    ''' <summary>
    ''' 全社 集計ボタン
    ''' </summary>
    ''' <history>																
    ''' <para>2012/11/16 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnZensya_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        '全社 集計画面へ遷移する
        Server.Transfer("ZensyaSyukeiInquiry.aspx")

    End Sub

    ''' <summary>
    ''' 各種 集計ボタン
    ''' </summary>
    ''' <history>																
    ''' <para>2012/11/16 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnKakusyu_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        '全社 集計画面へ遷移する
        Server.Transfer("KakusyuSyukeiInquiry.aspx")

    End Sub

#End Region

#Region "メンッド"
    ''' <summary>
    ''' OnClick事件を追加
    ''' </summary>
    ''' <history>																
    ''' <para>2012/11/16 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub SetJsEvent()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        '全社 集計ボタンのClick事件
        Me.btnZensya.OnClick = "btnZensya_Click()"
        '各種 集計ボタンのClick事件
        Me.btnKakusyu.OnClick = "btnKakusyu_Click()"
    End Sub
#End Region

End Class
